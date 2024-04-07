using AutoMapper;
using InterviewProject.Data;
using InterviewProject.Data.DTO;
using InterviewProject.Data.Exceptions;
using InterviewProject.Data.Model;
using InterviewProject.InitialData.Data;
using InterviewProject.InitialData.Interfaces;
using InterviewProject.Services.Interfaces;
using InterviewProject.Services.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;

namespace InterviewProject.InitialData.Classes
{
    public class DatabaseInitializerService: IDatabaseInitializerService
    {
        private readonly IDataContext _context;
        private ILogger<DatabaseInitializerService> _logger;
        private IMapper _mapper;
        private RoleManager<Role> _roleManager;
        private UserManager<User> _userManager;
        private ISecurityService _securityService;

        public DatabaseInitializerService(
            IDataContext context,
            ILogger<DatabaseInitializerService> logger,
            IMapper mapper,
            RoleManager<Role> roleManager,
            UserManager<User> userManager,
            ISecurityService securityService
            )
        {
            this._context = context;
            this._logger = logger;
            this._mapper = mapper;
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._securityService = securityService;
        }
        
        public void EnsureInitialData()
        {
            if (!AllMigrationsApplied())
            {
                _logger.LogError("DatabaseInitializerService: not all migrations applied");
                return;
            }

            EnsureInitialRoles();
            EnsureInitialUsers();
        }
        
        private bool AllMigrationsApplied()
        {
            var applied = ((DataContextBase)_context).GetService<IHistoryRepository>().GetAppliedMigrations().Select(m => m.MigrationId);
            var total = ((DataContextBase)_context).GetService<IMigrationsAssembly>().Migrations.Select(m => m.Key);
            return !total.Except(applied).Any();
        }

        private void EnsureInitialRoles()
        {
            var roles = new List<RoleDTO>()
            {
                Roles.Admin,
                Roles.User
            };

            foreach (var role in roles)
            {
                var existingRole = _roleManager.FindByNameAsync(role.Name).Result;
                if (existingRole == null)
                {
                    existingRole = new Role(role.Name) { Id = Guid.NewGuid().ToString() };
                    var result = _roleManager.CreateAsync(existingRole).Result;
                    if (result.Succeeded)
                    {
                        _logger.LogInformation($"Role {role.Name} has been successfully created");
                    }
                    else
                    {
                        _logger.LogError($"Role {role.Name} hasn't been created. Error: {IdentityUtils.GetOneStringErrors(result.Errors)}");
                    }
                }
            }
        }
        
        private void EnsureInitialUsers()
        {
            var users = new List<(UserDTO, RoleDTO)>()
            {
                (Users.Admin, Roles.Admin),
                (Users.User, Roles.User),
            };
            var passwordHasher = new PasswordHasher<User>();

            foreach (var userRole in users)
            {
                var user = userRole.Item1;
                var role = userRole.Item2;
                
                var existingUser = _userManager.FindByEmailAsync(user.Email).Result;
                if (existingUser == null)
                {
                    existingUser = this._mapper.Map<User>(user);
                    existingUser.UserName = user.Email;
                    existingUser.EmailConfirmed = true;
                    
                    var newPassword = _securityService.GetHashedValue(user.Password);
                    var result = _userManager.CreateAsync(existingUser, newPassword.ToLowerInvariant()).Result;

                    if (result.Succeeded)
                    {
                        _logger.LogInformation($"User {user.Email} has been successfully created");
                    }
                    else
                    {
                        throw new ObjectUpdatingException(
                            $"User {user.Email} hasn't been created. Error: {IdentityUtils.GetOneStringErrors(result.Errors)}");
                    }
                    
                    result = _userManager.AddToRoleAsync(existingUser, role.Name).Result;
                    
                    if (result.Succeeded)
                    {
                        _logger.LogInformation($"User {user.Email} has been successfully assigned to the {role.Name} role");
                    }
                    else
                    {
                        throw new ObjectUpdatingException(
                            $"User {user.Email} hasn't been assigned to the {role.Name} role. Error: {IdentityUtils.GetOneStringErrors(result.Errors)}");
                    }
                }
                else
                {
                    var newPassword = _securityService.GetHashedValue(user.Password);

                    var verifyRes = passwordHasher.VerifyHashedPassword(existingUser, existingUser.PasswordHash, newPassword.ToLowerInvariant());

                    if (verifyRes == PasswordVerificationResult.Failed)
                    {
                        // Update existent demo user with a new password
                        existingUser.PasswordHash = passwordHasher.HashPassword(existingUser, newPassword);
                        var result = _userManager.UpdateAsync(existingUser).Result;
                        
                        if (result.Succeeded)
                        {
                            _logger.LogInformation($"User {user.Email} has been successfully updated");
                        }
                        else
                        {
                            _logger.LogError($"User {user.Email} hasn't been updated. Error: {IdentityUtils.GetOneStringErrors(result.Errors)}");
                        }
                    }
                }
            }
        }
    }
}

