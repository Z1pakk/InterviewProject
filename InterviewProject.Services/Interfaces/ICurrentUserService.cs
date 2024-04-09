namespace InterviewProject.Services.Interfaces
{
    public interface ICurrentUserService
    {
        string GetCurrentUserId();
        Task<string> GetCurrentUserEmail();
        Task<List<string>> GetCurrentUserRoles();
    }
}

