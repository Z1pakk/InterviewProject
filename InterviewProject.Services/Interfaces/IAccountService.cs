using InterviewProject.Data.DTO;

namespace InterviewProject.Services.Interfaces
{
    public interface IAccountService
    {
        Task<UserDTO> SignIn(SignInDTO dto, CancellationToken cancellationToken);
        bool HasLoggedInUser();
        Task Logout(CancellationToken cancellationToken);
    }
}

