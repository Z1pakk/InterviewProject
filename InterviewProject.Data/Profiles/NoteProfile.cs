using AutoMapper;
using InterviewProject.Data.DTO;
using InterviewProject.Data.Model;

namespace InterviewProject.Data.Profiles
{
    /// <summary>
    /// A profile for mapper
    /// </summary>
    public class NoteProfile: Profile
    {
        public NoteProfile()
        {
            CreateMap<Note, NoteDTO>()
                .ReverseMap();
        }
    }
}
