using InterviewProject.Data.DTO;
using InterviewProject.Data.Model;

namespace InterviewProject.InitialData.Data
{
    public static class Roles
    {
        public static RoleDTO Admin = new RoleDTO
        {
            Name = Role.Admin
        };
        
        public static RoleDTO User = new RoleDTO
        {
            Name = Role.User
        };
    }
}

