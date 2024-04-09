using AutoMapper;
using InterviewProject.Data.DTO;
using InterviewProject.Data.Exceptions;
using InterviewProject.Data.Model;
using InterviewProject.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace InterviewProject.Services.Classes
{
    /// <summary>
    /// Main service to work with authentication
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ICurrentUserService _currentUserService;
        
        public AccountService(
            IMapper mapper,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            ICurrentUserService currentUserService
            )
        {
            this._mapper = mapper;
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._currentUserService = currentUserService;
        }

        public async Task<UserDTO> SignIn(SignInDTO dto, CancellationToken cancellationToken)
        {
            var user = await this._userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
            {
                throw new WrongCredentialsException("Invalid username or password");
            }
            
            result = await _signInManager.PasswordSignInAsync(user, dto.Password, false, false);
            if (!result.Succeeded)
            {
                throw new Exception($"Other authentication error");
            }

            var mappedUser = _mapper.Map<UserDTO>(user);
            var roles = await this._currentUserService.GetCurrentUserRoles();

            if (roles != null && roles.Count > 0 && roles.Contains(Role.Admin))
            {
                mappedUser.IsAdmin = true;
            }

            return mappedUser;
        }
        
        public bool HasLoggedInUser()
        {
            string userId = _currentUserService.GetCurrentUserId();

            if (string.IsNullOrWhiteSpace(userId))
                return false;

            return true;
        }

        public async Task Logout(CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();
        }
    }
}

