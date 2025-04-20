using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TestApiSep.DTOs;
using TestApiSep.Repository;

namespace TestApiSep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroController : ControllerBase
    {
        private readonly IValidator<RegistroInsertDto> _registroInsertValidator;
        private readonly IRepository<RegistroDto> _repository;


        public RegistroController(IValidator<RegistroInsertDto> registroInsertValidator, IRepository<RegistroDto> repository)
        {
            _registroInsertValidator = registroInsertValidator;
            _repository = repository;
        }

        // POST api/registro
        [HttpPost]
        public async Task<ActionResult<RegistroDto>> Post(RegistroInsertDto registro)
        {
            // Validar los datos recibidos
            var validationResult = await _registroInsertValidator.ValidateAsync(registro);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            // Guardar el registro
            await _repository.Add(registro);

            return Ok(registro);
        }

        // GET api/registro
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegistroDto>>> Get()
        {
            var resgistros = await _repository.Get();
            return Ok(resgistros);
        }
    }
}
