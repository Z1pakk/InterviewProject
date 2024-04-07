using InterviewProject.Data.DTO;

namespace InterviewProject.InitialData.Data
{
    public static class Users
    {
        public static UserDTO Admin = new UserDTO
        {
            Email = "admin@project.com",
            Password = "Qwerty-1",
            FirstName = "User",
            LastName = "Admin"
        };
        
        public static UserDTO User = new UserDTO
        {
            Email = "user@project.com",
            Password = "Qwerty-1",
            FirstName = "User",
            LastName = "User"
        };
    }
}

