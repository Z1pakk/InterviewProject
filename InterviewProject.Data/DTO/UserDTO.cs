using InterviewProject.Data.Interfaces;

namespace InterviewProject.Data.DTO
{
    public class UserDTO: IDTO<string>
    {
        public string? Id { get; set; }

        public string Email { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string? FullName { get; set; }
        
        
        public string RoleName { get; set; }
        
        
        public string? Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}

