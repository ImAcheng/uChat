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
                                 join ms in _db.ChatRoomMember on r.ChatRoomId equals ms.ChatRoomId
                                 select new ChatDisplayDTO() {
                                     ChatId = r.ChatRoomId,
                                     ChatName = r.ChatName ?? ms.Account.Username
                                 }).ToListAsync();

            return Ok(new { success = true, message = "", chat = _result });
        }

        [HttpPost("open-by-user")]
        public async Task<IActionResult> OpenChatByUser([FromBody] GeneralChatRequest data) {
            // this is called when users try to open a chat by clicking one's profile
            // check if they have chat
            var ctx = await (from self in _db.ChatRoomMember.AsNoTracking()
                             where self.AccountId == data.Requester
                             join r in _db.ChatRoom on self.ChatRoomId equals r.ChatRoomId
                             join they in _db.ChatRoomMember on r.ChatRoomId equals they.ChatRoomId
                             where they.AccountId == data.Target && r.ChatRoomType == 0
                             select new OneToOneChat(r, self.Account, they.Account)).FirstOrDefaultAsync();

            if (ctx == null) {
                var chat = new ChatRoom() {
                    ChatRoomId = Guid.NewGuid(),
                    ChatRoomType = 0,
                    CreateDateTime = DateTimeOffset.Now
                };

                var self = new ChatRoomMember() {
                    ChatRoomMemberId = Guid.NewGuid(),
                    AccountId = data.Requester,
                    ChatRoomId = chat.ChatRoomId,
                    CreateDateTime = chat.CreateDateTime,
                };

                var they = new ChatRoomMember() {
                    ChatRoomMemberId = Guid.NewGuid(),
                    AccountId = data.Target,
                    ChatRoomId = chat.ChatRoomId,
                    CreateDateTime = chat.CreateDateTime,
                };

                try {
                    await _db.Database.BeginTransactionAsync();
                    await _db.ChatRoom.AddAsync(chat);
                    await _db.ChatRoomMember.AddAsync(self);
                    await _db.ChatRoomMember.AddAsync(they);
                    await _db.SaveChangesAsync();
                    await _db.Database.CommitTransactionAsync();
                }
                catch {
                    await _db.Database.RollbackTransactionAsync();
                    return Ok(new { success = false, message = "Sorry! Please try again later." });
                }

                ctx = new OneToOneChat(chat, self.Account, they.Account);
            }

            return Ok(new {
                success = true,
                message = "OK",
                chat = new ChatDTO() {
                    ChatId = ctx.Chat!.ChatRoomId,
                    ChatType = 0,
                    Members = new List<AccountDTO>() {
                        new AccountDTO(ctx.Self!),
                        new AccountDTO(ctx.They!)
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
        public Account? Self, They;

        public OneToOneChat(ChatRoom? chat, Account? self, Account? they) {
            Chat = chat;
            Self = self;
            They = they;
        }
    }
}