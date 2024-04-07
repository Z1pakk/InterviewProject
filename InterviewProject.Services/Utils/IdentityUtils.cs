using Microsoft.AspNetCore.Identity;

namespace InterviewProject.Services.Utils
{
    public static class IdentityUtils
    {
        public static string GetOneStringErrors(IEnumerable<IdentityError> errors)
        {
            return string.Join("; ", errors?.Select(err => err.Code + ": " + err.Description) ?? Array.Empty<string>());
        }
    }
}

