using AutoMapper;
using InterviewProject.Data.DTO;
using InterviewProject.Data.Model;
using Microsoft.AspNetCore.Identity;

namespace InterviewProject.Data.Profiles
{
    /// <summary>
    /// A profile for mapper
    /// </summary>
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(u => u.FullName, opt => opt.MapFrom(u => u.FirstName + " " + u.LastName))
                .ForMember(u => u.RoleName, opt =>
                {
                    opt.PreCondition(u => u.UserRoles != null && u.UserRoles.Count > 0 && u.UserRoles.FirstOrDefault()?.Role != null);
                    opt.MapFrom(u => u.UserRoles.FirstOrDefault().Role.Name);
                })
                .ReverseMap();
        }
    }
}

