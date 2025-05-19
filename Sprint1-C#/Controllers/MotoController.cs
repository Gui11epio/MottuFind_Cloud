using Microsoft.AspNetCore.Mvc;
using Sprint1_C_.Application.DTOs.Requests;
using Sprint1_C_.Application.DTOs.Response;
using Sprint1_C_.Application.Services;

namespace Sprint1_C_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotoController : ControllerBase
    {
        private readonly MotoService _motoService;

        public MotoController(MotoService motoService)
        {
            _motoService = motoService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MotoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAll()
        {
            var motos = _motoService.ObterTodos();
            return Ok(motos);
        }

        

        [HttpGet("placa")]
        [ProducesResponseType(typeof(MotoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByPlaca([FromQuery] string valor)
        {
            var moto = _motoService.ObterPorPlaca(valor);
            if (moto == null)
                return NotFound();
            return Ok(moto);
        }

        [HttpGet("pagina")]
        [ProducesResponseType(typeof(PagedResult<PatioResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<PagedResult<PatioResponse>>> GetPaged(int numeroPag = 1, int tamanhoPag = 10)
        {
            var result = await _motoService.ObterPorPagina(numeroPag, tamanhoPag);
            return Ok(result);
        }


        [HttpPost]
        [ProducesResponseType(typeof(MotoResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] MotoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var motoCriada = _motoService.Criar(request);
            return CreatedAtAction(nameof(GetByPlaca), new { placa = motoCriada.Placa }, motoCriada);
        }

        [HttpPut("placa")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(string placa, [FromBody] MotoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var atualizada = _motoService.Atualizar(placa, request);
            if (!atualizada)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("placa")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(string placa)
        {
            var removida = _motoService.Remover(placa);
            if (!removida)
                return NotFound();

            return NoContent();
        }
    }
}
