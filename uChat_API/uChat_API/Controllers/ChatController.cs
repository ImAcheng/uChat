using Microsoft.AspNetCore.Http;
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

        [HttpPost("open-by-user")]
        public async Task<IActionResult> OpenChatByUser([FromBody] GeneralChatRequest data) {
            // check if they have chat
            var ctx = await (from self in _db.ChatRoomMember.AsNoTracking()
                        where self.AccountId == data.Requester
                        join r in _db.ChatRoom on self.ChatRoomId equals r.ChatRoomId
                        join they in _db.ChatRoomMember on r.ChatRoomId equals they.ChatRoomId
                        where they.AccountId == data.Target
                        select new OneToOneChat(r, self, they)).FirstOrDefaultAsync();

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

                ctx = new OneToOneChat(chat, self, they);
            }

            return Ok(new {
                success = true,
                message = "OK",
                chat = new ChatDTO() {

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

    public class ChatDTO {
        public Guid ChatId { get; set; }
        public string? ChatName { get; set; }
        public List<Guid> Members { get; set; }
        public List<string> Names { get; set; }
    }
}