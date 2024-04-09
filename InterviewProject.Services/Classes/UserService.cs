using AutoMapper;
using InterviewProject.Data;
using InterviewProject.Data.DTO;
using InterviewProject.Data.Exceptions;
using InterviewProject.Data.Filters;
using InterviewProject.Data.Model;
using InterviewProject.Services.Interfaces;
using InterviewProject.Services.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InterviewProject.Services.Classes
{
    /// <summary>
    /// Main service for user
    /// </summary>
    public class UserService: IUserService
    {
        private readonly IDataContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        
        public UserService(
            IDataContext context,
            IMapper mapper,
            UserManager<User> userManager
            )
        {
            this._context = context;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        private IQueryable<User> GetQueryable()
        {
            return this._context.Set<User>()
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .AsQueryable();
        }
        
        /// <summary>
        /// Main method to work with grids
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PageResult<UserDTO>> GetPage(FilterCommand? command, CancellationToken cancellationToken)
        {
            var query = this.GetQueryable().AsNoTracking();
            int total;

            if (command != null)
            {
                query = SortService<User, string>.ApplySorting(query, command);

                if (!string.IsNullOrEmpty(command.SearchQuery))
                {
                    query = query.Where(u => (u.FirstName + " " + u.LastName).ToLower().Contains(command.SearchQuery) || u.Email!.Contains(command.SearchQuery));
                }
                
                total = await query.CountAsync(cancellationToken);
                
                var maxSkip = (total / command.Take) * command.Take;
                if (total % command.Take == 0 && total != 0)
                {
                    maxSkip -= command.Take;
                }
                query = query.Skip(Math.Min(command.Skip, maxSkip)).Take(command.Take);
            }
            else
            {
                total = await query.CountAsync(cancellationToken);
            }
            
            return new PageResult<UserDTO>()
            {
                Items = this._mapper.Map<IEnumerable<UserDTO>>(await query.ToListAsync(cancellationToken)),
                Total = total
            };
        }

        public async Task<UserDTO> Get(string id, CancellationToken cancellationToken)
        {
            var query = this.GetQueryable().AsNoTracking();
            var user = await query.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            return _mapper.Map<UserDTO>(user);
        }
        
        public async Task<UserDTO> Update(string? id, UserDTO userDTO, CancellationToken cancellationToken)
        {
            User user;
            if (string.IsNullOrEmpty(id))
            {
                user = _mapper.Map<User>(userDTO);
            }
            else
            {
                user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    throw new UserNotFoundException();
                }
            }
            
            // Check if an user with the same email exists
            var existingUserByEmail = (await _userManager.FindByEmailAsync(userDTO.Email));
            if (existingUserByEmail != null && (existingUserByEmail?.Id != user.Id || user.Id == null))
            {
                throw new Exception("User with this email exists");
            }

            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;
            user.Email = userDTO.Email;
            user.UserName = userDTO.Email;

            if (string.IsNullOrEmpty(user.Id))
            {
                await _userManager.CreateAsync(user);
                id = user.Id;
            }
            else
            {
                await _userManager.UpdateAsync(user);
            }
            
            // Assign a role
            if (!string.IsNullOrEmpty(userDTO.RoleName))
            {
                var roleToAssign = await _context.Set<Role>().FirstOrDefaultAsync(r => r.Name.ToLower() == userDTO.RoleName, cancellationToken);
                if (roleToAssign == null)
                {
                    throw new Exception("Role is not found");
                }
                
                var userRoles = _context.Set<UserRole>().Where(ur => ur.UserId == user.Id);
                _context.Set<UserRole>().RemoveRange(userRoles);
                
                await _context.Set<UserRole>().AddAsync(new UserRole()
                {
                    UserId = user.Id,
                    RoleId = roleToAssign.Id
                }, cancellationToken);
            }

            // Update password
            if (!string.IsNullOrEmpty(userDTO.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var result = await _userManager.ResetPasswordAsync(user, token, userDTO.Password);
                if (!result.Succeeded)
                {
                    throw new ObjectUpdatingException($"Password updating exception: {string.Join(";", result.Errors.Select(er => er.Description))}");
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UserDTO>(await this.GetQueryable().FirstOrDefaultAsync(u => u.Id == id, cancellationToken));
        }

        public async Task<bool> Remove(string id, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            // Use transaction to ensure that both related notes and a user are deleted
            var executionStrategy = this._context.Database.CreateExecutionStrategy();
            return await executionStrategy.ExecuteAsync(async () =>
            {
                using (var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable, cancellationToken))
                {
                    try
                    {
                        var notes = _context.Set<Note>().Where(u => u.UserId == user.Id);
                        _context.Set<Note>().RemoveRange(notes);

                        await _userManager.DeleteAsync(user);

                        await _context.SaveChangesAsync(cancellationToken);

                        await transaction.CommitAsync(cancellationToken);

                        return true;
                    }
                    catch (Exception e)
                    {
                        await transaction.RollbackAsync(cancellationToken);
                    }
                    
                    return false;
                }
            });
        }
    }
}

