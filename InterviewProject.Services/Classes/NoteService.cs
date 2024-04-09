using AutoMapper;
using InterviewProject.Data;
using InterviewProject.Data.DTO;
using InterviewProject.Data.Exceptions;
using InterviewProject.Data.Filters;
using InterviewProject.Data.Model;
using InterviewProject.Services.Interfaces;
using InterviewProject.Services.Utils;
using Microsoft.EntityFrameworkCore;

namespace InterviewProject.Services.Classes
{
    /// <summary>
    /// Main service for notes
    /// </summary>
    public class NoteService: INoteService
    {
        private readonly IDataContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        
        public NoteService(
            IDataContext context,
            IMapper mapper,
            ICurrentUserService currentUserService
            )
        {
            this._context = context;
            this._mapper = mapper;
            this._currentUserService = currentUserService;
        }
        
        private IQueryable<Note> GetQueryable()
        {
            var currentUserId = this._currentUserService.GetCurrentUserId();
            
            return this._context.Set<Note>()
                .Where(n => n.UserId == currentUserId)
                .AsQueryable();
        }

        public async Task<PageResult<NoteDTO>> GetPage(FilterCommand? command, CancellationToken cancellationToken)
        {
            var query = this.GetQueryable().AsNoTracking();
            int total;

            if (command != null)
            {
                query = SortService<Note, int>.ApplySorting(query, command);

                if (!string.IsNullOrEmpty(command.SearchQuery))
                {
                    query = query.Where(u => u.Text.ToLower().Contains(command.SearchQuery));
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
            
            return new PageResult<NoteDTO>()
            {
                Items = this._mapper.Map<IEnumerable<NoteDTO>>(await query.ToListAsync(cancellationToken)),
                Total = total
            };
        }

        public async Task<NoteDTO> Get(int id, CancellationToken cancellationToken)
        {
            var query = this.GetQueryable().AsNoTracking();
            var note = await query.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
            if (note == null)
            {
                throw new ObjectNotFoundException(nameof(Note));
            }

            return _mapper.Map<NoteDTO>(note);
        }

        public async Task<NoteDTO> Update(int? id, NoteDTO noteDTO, CancellationToken cancellationToken)
        {
            Note note;
            if (id == null)
            {
                note = new Note
                {
                    UserId = _currentUserService.GetCurrentUserId()
                };
                await _context.Set<Note>().AddAsync(note, cancellationToken);
            }
            else
            {
                note = await this.GetQueryable().FirstOrDefaultAsync(n => n.Id == id);
                if (note == null)
                {
                    throw new ObjectNotFoundException("Note");
                }
            }

            note.Text = noteDTO.Text;
            
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<NoteDTO>(await this.GetQueryable().FirstOrDefaultAsync(u => u.Id == note.Id, cancellationToken));
        }

        public async Task<bool> Remove(int id, CancellationToken cancellationToken)
        {
            var note = await this.GetQueryable().FirstOrDefaultAsync(n => n.Id == id);
            if (note == null)
            {
                throw new ObjectNotFoundException("Note");
            }

            try
            {
                _context.Set<Note>().Remove(note);

                await _context.SaveChangesAsync(cancellationToken);
            
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}

