using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestApiSep.DTOs;
using TestApiSep.Models;

namespace TestApiSep.Repository
{
    public class ParticipanteRepository : IRepository<RegistroDto>
    {
        private readonly DBTestSepContext _context;
        private readonly IMapper _mapper;

        public ParticipanteRepository(DBTestSepContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(RegistroInsertDto registro)
        {
            var entity = _mapper.Map<Participante>(registro);
            await _context.Participantes.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RegistroDto>> Get()
        {
            var participantes = await _context.Participantes.ToListAsync();

            return participantes.Select(b => _mapper.Map<RegistroDto>(b));
        }

        public async Task<RegistroDto> GetById(int id)
        {
            var participante = await _context.Participantes.FindAsync(id);

            if (participante != null)
            {
                var registroDto = _mapper.Map<RegistroDto>(participante);

                return registroDto;
            }

            return null;
        }
    }
}
