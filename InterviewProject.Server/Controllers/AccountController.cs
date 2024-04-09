using InterviewProject.Data.DTO;
using InterviewProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterviewProject.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController: Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;
        
        public AccountController(
            ILogger<AccountController> logger,
            IAccountService accountService,
            IUserService userService,
            ICurrentUserService currentUserService
            )
        {
            this._logger = logger;
            this._accountService = accountService;
            this._userService = userService;
            this._currentUserService = currentUserService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]SignInDTO data, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await this._accountService.SignIn(data, cancellationToken);
                this._logger.LogInformation("User logged in successfully.");
                return Ok(result);

            }
            catch (Exception e)
            {
                this._logger.LogError("SignIn Error: " + e.Message);
                
                return Unauthorized(e.Message);
            }
        } 
        
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMyInfo(CancellationToken cancellationToken = default)
        {
            try
            {
                return Ok(await this._userService.Get(this._currentUserService.GetCurrentUserId(), cancellationToken));

            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
        }
        
        [HttpGet, Route("has-logged-in-user")]
        [Authorize]
        public IActionResult HasLoggedInUser()
        {
            var res = this._accountService.HasLoggedInUser();
            return Ok(res);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("logout")]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken = default)
        {
            await this._accountService.Logout(cancellationToken);
            this._logger.LogInformation("User logged out successfully.");

            return NoContent();
        }
    }
}

