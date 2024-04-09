using InterviewProject.Data.DTO;
using InterviewProject.Data.Filters;
using InterviewProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterviewProject.Server.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class NoteController: Controller
    {
        private readonly ILogger<NoteController> _logger;
        private readonly INoteService _noteService;
        
        public NoteController(
            ILogger<NoteController> logger,
            INoteService noteService
        )
        {
            this._logger = logger;
            this._noteService = noteService;
        }

        [HttpGet("page")]
        public async Task<ActionResult<IEnumerable<NoteDTO>>> GetPage([FromQuery] FilterCommand? command, CancellationToken cancellationToken)
        {
            return Ok(await this._noteService.GetPage(command, cancellationToken));
        }
        
        [HttpGet]
        public async Task<ActionResult<NoteDTO>> Get(int id, CancellationToken cancellationToken)
        {
            return Ok(await this._noteService.Get(id, cancellationToken));
        }
        
        [HttpPost]
        public async Task<ActionResult<NoteDTO>> Create([FromBody] NoteDTO user, CancellationToken cancellationToken)
        {
            return Ok(await this._noteService.Update(null, user, cancellationToken));
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<NoteDTO>> Update(int id, [FromBody] NoteDTO user, CancellationToken cancellationToken)
        {
            return Ok(await this._noteService.Update(id, user, cancellationToken));
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id, CancellationToken cancellationToken)
        {
            return Ok(await this._noteService.Remove(id, cancellationToken));
        }
    }
}

