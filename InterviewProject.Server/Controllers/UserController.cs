using InterviewProject.Data.DTO;
using InterviewProject.Data.Filters;
using InterviewProject.Data.Model;
using InterviewProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterviewProject.Server.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController: Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        
        public UserController(
            ILogger<UserController> logger,
            IUserService userService
            )
        {
            this._logger = logger;
            this._userService = userService;
        }

        [HttpGet("page")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetPage([FromQuery] FilterCommand? command, CancellationToken cancellationToken)
        {
            return Ok(await this._userService.GetPage(command, cancellationToken));
        }
        
        [HttpGet]
        public async Task<ActionResult<UserDTO>> Get(string id, CancellationToken cancellationToken)
        {
            return Ok(await this._userService.Get(id, cancellationToken));
        }
        
        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<UserDTO>> Create([FromBody] UserDTO user, CancellationToken cancellationToken)
        {
            return Ok(await this._userService.Update(user.Id, user, cancellationToken));
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<UserDTO>> Update(string id, [FromBody] UserDTO user, CancellationToken cancellationToken)
        {
            return Ok(await this._userService.Update(id, user, cancellationToken));
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<bool>> Delete(string id, CancellationToken cancellationToken)
        {
            return Ok(await this._userService.Remove(id, cancellationToken));
        }
    }
}

