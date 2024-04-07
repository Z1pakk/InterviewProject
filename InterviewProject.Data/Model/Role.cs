using InterviewProject.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace InterviewProject.Data.Model
{
    public class Role : IdentityRole<string>, IEntity<string>
    {
        public Role()
        {
        }

        public Role(string roleName) : base(roleName)
        {
        }
        
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public const string Admin = "Admin";

        public const string User = "User";
    }
}

