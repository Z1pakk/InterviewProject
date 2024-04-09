using InterviewProject.Data.Model;
using InterviewProject.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace InterviewProject.Services.Classes
{
    /// <summary>
    /// Main service for working with current logged user
    /// </summary>
    public class CurrentUserService: ICurrentUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUserId()
        {
            return _userManager.GetUserId(_httpContextAccessor?.HttpContext?.User);
        }

        public async Task<string> GetCurrentUserEmail()
        {
            return (await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User))?.Email;
        }

        public async Task<List<string>> GetCurrentUserRoles()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User);
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }
    }
}
