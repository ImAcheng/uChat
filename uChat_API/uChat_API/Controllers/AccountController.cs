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
            Console.WriteLine($"[Debug] a client fetched account/create ({data.username})");

            string username = data.username.Trim().ToLower();
            string password = data.password;
            string? nickname = string.IsNullOrWhiteSpace(data.nickname) ? null : data.nickname;
            byte gender = data.genderCode;
            string usernamePattern = @"^[a-zA-Z0-9._]{2,}$";

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return BadRequest(new { success = false, message = AccountMessage.EmptyData });

            bool isExisted = await _db.Account
                .AsNoTracking()
                .AnyAsync(x => x.Username == username);

            if (isExisted)
                return BadRequest(new { success = false, message = AccountMessage.ExistedData });

            bool isUsernameValid = Regex.IsMatch(username, usernamePattern);
            if (!isUsernameValid)
                return BadRequest(new { success = false, message = AccountMessage.InvalidUsername });

            var newAcc = new Account() {
                AccountId = Guid.NewGuid(),
                Email = $"{username}@noemail.now",
                Username = username,
                Password = password,
                Nickname = nickname,
                AccountBio = null,
                GenderCode = gender,
                CreateDateTime = DateTimeOffset.Now
            };

            try {
                await _db.Database.BeginTransactionAsync();
                await _db.Account.AddAsync(newAcc);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex) {
                await _db.Database.RollbackTransactionAsync();
                return BadRequest(new { success = false, message = $"Unexpected error occurred: {ex.Message}" });
            }

            await _db.Database.CommitTransactionAsync();

            return Ok(new { success = true, message = AccountMessage.Success, account = newAcc });
        }

        /// <summary>
        /// An endpoint to check if one username is already existsed.
        /// </summary>
        [HttpPost("is-exists")]
        public async Task<IActionResult> CheckUsernameExisted([FromBody] string username) {
            bool isExisted = await _db.Account
                .AsNoTracking()
                .AnyAsync(x => x.Username == username);

            return Ok(isExisted);
        }

        [HttpGet("find/{keyword}")]
        public async Task<IActionResult> FindUsers([FromRoute] string keyword) {
            var _Result = await _db.Account
                .AsNoTracking()
                .Where(x => x.Username.Contains(keyword.ToLowerInvariant()))
                .Select(x => new AccountDTO(x))
                .ToListAsync();

            return Ok(_Result);
        }
    }

    public class AccountMessage {
        public static string EmptyData = "EMPTY_DATA";
        public static string AccountNotFound = "ACC_NOT_FOUND";
        public static string IncorrectPassword = "PWD_FAIL";

        public static string InvalidEmail = "BAD_EMAIL";
        public static string ExistedData = "EXISTED_DATA";

        public static string Success = "OK";
        public static string InvalidUsername = "BAD_USERNAME";
    }

    public class LoginRequest {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class CreateAccountRequest {
        public string username { get; set; }
        public string password { get; set; }
        public string? nickname { get; set; }
        public byte genderCode { get; set; }
    }

    public class DirectLoginRequest {
        public Guid accountId { get; set; }
        public string password { get; set; }
    }
}
