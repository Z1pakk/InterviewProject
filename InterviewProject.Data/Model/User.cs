using InterviewProject.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace InterviewProject.Data.Model
{
    public class User: IdentityUser<string>, IEntity<string>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}

