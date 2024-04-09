using InterviewProject.Data.DTO;
using InterviewProject.Data.Filters;

namespace InterviewProject.Services.Interfaces
{
    public interface INoteService
    {
        Task<PageResult<NoteDTO>> GetPage(FilterCommand? command, CancellationToken cancellationToken);
        
        Task<NoteDTO> Get(int id, CancellationToken cancellationToken);
        
        Task<NoteDTO> Update(int? id, NoteDTO noteDTO, CancellationToken cancellationToken);
        
        Task<bool> Remove(int id, CancellationToken cancellationToken);
    }
}

