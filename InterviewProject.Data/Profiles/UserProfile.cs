using AutoMapper;
using InterviewProject.Data.DTO;
using InterviewProject.Data.Model;

namespace InterviewProject.Data.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>()
                .ReverseMap();
        }
    }
}

