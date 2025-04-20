using Microsoft.AspNetCore.Mvc;
using TestApiSep.Service;

namespace TestApiSep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SepController : ControllerBase
    {
        private readonly ICedulaService _cedulaService;

        public SepController(ICedulaService cedulaService)
        {
            _cedulaService = cedulaService;
        }
        [HttpGet]
        public async Task<ActionResult<string>> Get(string idCedula) => await _cedulaService.BuscarCedulaAsync(idCedula);
    }
}
