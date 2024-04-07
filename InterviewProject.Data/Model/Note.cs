using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InterviewProject.Data.Interfaces;

namespace InterviewProject.Data.Model
{
    public class Note: IEntity<int>
    {
        public int Id { get; set; }

        [MaxLength(5000)]
        public string Text { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}

