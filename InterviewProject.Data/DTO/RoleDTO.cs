using InterviewProject.Data.Interfaces;

namespace InterviewProject.Data.DTO
{
    public class RoleDTO: IDTO<string>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}

