using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using uChat_API.DbModel;

namespace uChat_API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase {

        private readonly UChatDbContext _db;
        private readonly IWebHostEnvironment _env;

        public AccountController(UChatDbContext db, IWebHostEnvironment env) {
            this._db = db;
            this._env = env;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest data) {
            string username = data.username.Trim().ToLower();
            string password = data.password;

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
                return BadRequest(new { success = false, message = AccountMessage.EmptyData });

            Account? _account;

            try {
                _account = await _db.Account
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Email == username || x.Username == username);
            }
            catch {
                return StatusCode(503, new { success = false, message = "DB_DIED" });
            }

            if (_account == null)
                return NotFound(new { success = false, message = AccountMessage.AccountNotFound });

            if (_account.Password.CompareTo(password) != 0)
                return BadRequest(new { success = false, message = AccountMessage.IncorrectPassword });

            var _accDto = new AccountDTO(_account);

            return Ok(new { success = true, message = AccountMessage.Success, account = _accDto});
        }

        [HttpPost("d-login")]
        public async Task<IActionResult> DirectLogin([FromBody] DirectLoginRequest data) {
            // this doesn't need to provide any message (except for DB_DIED)
            // because this is mostly in background work.
            // (technically)

            if (string.IsNullOrWhiteSpace(data.password))
                return BadRequest(new { success = false, message = AccountMessage.EmptyData });

            Account? _account;

            try {
                _account = await _db.Account
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.AccountId == data.accountId && x.Password == data.password);
            }
            catch {
                return StatusCode(503, new { success = false, message = "DB_DIED" });
            }

            if (_account == null)
                return NotFound(new { success = false, message = AccountMessage.AccountNotFound });

            var _accDto = new AccountDTO(_account);

            return Ok(new { success = true, message = AccountMessage.Success, account = _accDto });
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest data) {
            string email = data.email.Trim().ToLower();
            string username = data.username.Trim().ToLower();
            string? nickname = data.nickname;
            string? accountBio = data.accountBio;
            byte gender = data.genderCode;

            string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            string usernamePattern = @"^[a-zA-Z0-9._-]{2,}$";

            bool isEmailValid = Regex.IsMatch(email, emailPattern);
            bool isUsernameValid = Regex.IsMatch(username, usernamePattern);

            if (!isEmailValid)
                return BadRequest(new { success = false, message = AccountMessage.InvalidEmail });

            bool _existsed = await _db.Account
                .AsNoTracking()
                .AnyAsync(x => x.Username == username || x.Email == email);

            if (_existsed)
                return BadRequest(new { success = false, message = AccountMessage.ExistedData });

            var newAcc = new Account() {
                AccountId = Guid.NewGuid(),
                Email = email,
                Username = username,
                Nickname = nickname,
                AccountBio = accountBio,
                GenderCode = gender,
                CreateDateTime = DateTimeOffset.Now
            };

            await _db.Database.BeginTransactionAsync();
            try {
                await _db.Account.AddAsync(newAcc);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex) {
                await _db.Database.RollbackTransactionAsync();
                return BadRequest(new { success = false, message = ex.Message });
            }

            await _db.Database.CommitTransactionAsync();

            return Ok(new { success = false, message = AccountMessage.Success, account = newAcc });
        }
    }

    public class AccountMessage {
        public static string EmptyData = "EMPTY_DATA";
        public static string AccountNotFound = "ACC_NOT_FOUND";
        public static string IncorrectPassword = "PWD_FAIL";

        public static string InvalidEmail = "BAD_EMAIL";
        public static string ExistedData = "EXISTED_DATA";

        public static string Success = "OK";
    }

    public class LoginRequest {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class CreateAccountRequest {
        public string email { get; set; }
        public string username { get; set; }
        public string? nickname { get; set; }
        public string? accountBio { get; set; }
        public byte genderCode { get; set; }
    }

    public class DirectLoginRequest {
        public Guid accountId { get; set; }
        public string password { get; set; }
    }
}
