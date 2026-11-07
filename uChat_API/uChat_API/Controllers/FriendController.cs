using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using uChat_API.DbModel;

namespace uChat_API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase {
        private readonly UChatDbContext _db;

        public FriendController(UChatDbContext db) {
            _db = db;
        }

        [HttpPost("get")]
        public async Task<IActionResult> GetFriends([FromBody] GetFriendData data) {
            var _Self = await _db.Account.FirstOrDefaultAsync(x => x.AccountId == data.UserId);

            if (_Self == null)
                return BadRequest(new { success = false, message = AccountMessage.AccountNotFound });

            switch (data.GetType) {
                case 0: {
                        var _Result = await (
                            from me in _db.Friend
                            where me.AccountId == _Self.AccountId
                               && me.Type == 1
                               && me.Status == 1

                            join f in _db.Friend
                                on me.FriendAccountId equals f.AccountId

                            where f.Type == 1
                               && f.Status == 1

                            select new FriendDTO() {
                                AccountNo = f.Account.AccountNo,
                                Username = f.Account.Username,
                                Displayname = f.Account.Nickname
                            }
                        ).ToListAsync();

                        return Ok(new { success = true, message = "OK", friends = _Result });
                    }
                    
                case 1: {
                        var _Result = await (
                            from me in _db.Friend
                            where me.AccountId == _Self.AccountId
                               && me.Type == 1
                               && me.Status == 1

                            join f in _db.Friend
                                on me.FriendAccountId equals f.AccountId

                            where f.Type == 0
                               && f.Status == 0

                            select new FriendDTO() {
                                AccountNo = f.Account.AccountNo,
                                Username = f.Account.Username,
                                Displayname = f.Account.Nickname
                            }
                        ).ToListAsync();

                        return Ok(new { success = true, message = "OK", friends = _Result });
                    }
                    
                default: {
                        var _Result = await (
                            from me in _db.Friend
                            where me.AccountId == _Self.AccountId
                               && me.Type == 0
                               && me.Status == 0

                            join f in _db.Friend
                                on me.FriendAccountId equals f.AccountId

                            where f.Type == 1
                               && f.Status == 1

                            select new FriendDTO() {
                                AccountNo = f.Account.AccountNo,
                                Username = f.Account.Username,
                                Displayname = f.Account.Nickname
                            }
                        ).ToListAsync();

                        return Ok(new { success = true, message = "OK", friends = _Result });
                    }
            }
        }
    }

    public class GetFriendData {
        public Guid UserId { get; set; }
        public int GetType { get; set; }
    }

    public class FriendDTO {
        public long AccountNo { get; set; }
        public string Username { get; set; }
        public string? Displayname { get; set; }
    }
}
