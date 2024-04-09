using Microsoft.AspNetCore.Identity;

namespace InterviewProject.Services.Utils
{
    public static class IdentityUtils
    {
        /// <summary>
        /// Convert multiple identity errors into one string
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static string GetOneStringErrors(IEnumerable<IdentityError> errors)
        {
            return string.Join("; ", errors?.Select(err => err.Code + ": " + err.Description) ?? Array.Empty<string>());
        }
    }
}

