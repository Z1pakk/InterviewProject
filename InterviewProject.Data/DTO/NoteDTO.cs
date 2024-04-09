using InterviewProject.Data.Interfaces;

namespace InterviewProject.Data.DTO
{
    public class NoteDTO: IDTO
    {
        public int Id { get; set; }

        public string Text { get; set; }
        
        public string? UserId { get; set; }
    }
}

