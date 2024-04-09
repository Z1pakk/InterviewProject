using InterviewProject.Data.DTO;
using InterviewProject.Data.Filters;

namespace InterviewProject.Services.Interfaces
{
    public interface IUserService
    {
        Task<PageResult<UserDTO>> GetPage(FilterCommand? command, CancellationToken cancellationToken);
        
        Task<UserDTO> Get(string id, CancellationToken cancellationToken);
        
        Task<UserDTO> Update(string? id, UserDTO userDTO, CancellationToken cancellationToken);
        
        Task<bool> Remove(string id, CancellationToken cancellationToken);
    }
}
