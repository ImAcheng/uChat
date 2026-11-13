using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using uChat_API.DbModel;

namespace uChat_API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase {
        private readonly UChatDbContext _db;

        public ChatController(UChatDbContext db) {
            _db = db;
        }

        [HttpGet("get-chats/{userId:guid}")]
        public async Task<IActionResult> GetChats([FromRoute] Guid userId) {
            var _result = await (from a in _db.Account.AsNoTracking()
                                 where a.AccountId == userId
                                 join m in _db.ChatRoomMember.AsNoTracking() on a.AccountId equals m.AccountId
                                 join r in _db.ChatRoom on m.ChatRoomId equals r.ChatRoomId
                                 select new ChatDisplayDTO() {
                                     ChatId = r.ChatRoomId,
                                     ChatMemberId = m.ChatRoomMemberId,
                                     ChatName = r.ChatName ?? 
                                         string.Join(", ",
                                            r.ChatRoomMembers.Where(x => x.AccountId != userId)
                                            .Select(x => x.Nickname ?? x.Account.Username)
                                         ),
                                     CreateDateTime = r.CreateDateTime,
                                     UpdateDateTime = r.UpdateDateTime
                                 })
                                 .OrderBy(x => x.UpdateDateTime)
                                 .ThenBy(x => x.CreateDateTime)
                                 .ToListAsync();

            return Ok(new { success = true, message = "OK", chat = _result });
        }

        [HttpPost("open-by-user")]
        public async Task<IActionResult> OpenChatByUser([FromBody] GeneralChatRequest data) {
            // this is called when users try to open a chat by clicking one's profile
            // check if they have chat
            var ctx = await (from self in _db.ChatRoomMember.Include(x => x.Account).AsNoTracking()
                             where self.AccountId == data.Requester
                             join r in _db.ChatRoom.AsNoTracking() on self.ChatRoomId equals r.ChatRoomId
                             join they in _db.ChatRoomMember.Include(x => x.Account).AsNoTracking() 
                                on r.ChatRoomId equals they.ChatRoomId
                             where they.AccountId == data.Target && r.ChatRoomType == 0
                             select new OneToOneChat(r, self, they)).FirstOrDefaultAsync();

            var accounts = await _db.Account.Where(x => x.AccountId == data.Requester ||
                            x.AccountId == data.Target)
                            .AsNoTracking()
                            .ToDictionaryAsync(x => x.AccountId);

            if (ctx == null) {
                var chat = new ChatRoom() {
                    ChatRoomId = Guid.NewGuid(),
                    ChatRoomType = 0,
                    CreateDateTime = DateTimeOffset.Now
                };

                var self = new ChatRoomMember() {
                    ChatRoomMemberId = Guid.NewGuid(),
                    AccountId = data.Requester,
                    CreateDateTime = chat.CreateDateTime
                };

                var they = new ChatRoomMember() {
                    ChatRoomMemberId = Guid.NewGuid(),
                    AccountId = data.Target,
                    CreateDateTime = chat.CreateDateTime
                };

                chat.ChatRoomMembers.Add(self);
                chat.ChatRoomMembers.Add(they);

                try {
                    await using var transaction =
                        await _db.Database.BeginTransactionAsync();
                    await _db.ChatRoom.AddAsync(chat);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch {
                    return Ok(new {
                        success = false,
                        message = "Sorry! Please try again later."
                    });
                }

                ctx = new OneToOneChat(chat, self, they);
                chat.ChatName = accounts[data.Target].Username;
            }

            return Ok(new {
                success = true,
                message = "OK",
                chat = new ChatDTO() {
                    ChatId = ctx.Chat!.ChatRoomId,
                    ChatMemberId = ctx.Self!.ChatRoomMemberId,
                    ChatType = 0,
                    ChatName = ctx.Chat.ChatName ?? ctx.They?.Account.Username ?? "Data Lost",
                    Members = new List<AccountDTO>() {
                        new AccountDTO(accounts[data.Requester]),
                        new AccountDTO(accounts[data.Target])
                    }
                }
            });
        }
    }

    public class GeneralChatRequest {
        public Guid Requester { get; set; }
        public Guid Target { get; set; }
    }

    public class OneToOneChat {
        public ChatRoom? Chat;
        public ChatRoomMember? Self, They;

        public OneToOneChat(ChatRoom? chat, ChatRoomMember? self, ChatRoomMember? they) {
            Chat = chat;
            Self = self;
            They = they;
        }
    }
}