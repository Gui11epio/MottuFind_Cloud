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
        public async Task<IActionResult> GetAll()
        {
            var motos = await _motoService.ObterTodos();
            if (motos == null || !motos.Any()) return NoContent();
            return Ok(motos);
        }

        [HttpGet("placa")]
        [ProducesResponseType(typeof(MotoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByPlaca([FromQuery] string valor)
        {
            var moto = await _motoService.ObterPorPlaca(valor);
            if (moto == null)
                return NotFound();
            return Ok(moto);
        }

        [HttpGet("pagina")]
        [ProducesResponseType(typeof(PagedResult<MotoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<PagedResult<MotoResponse>>> GetPaged(int numeroPag = 1, int tamanhoPag = 10)
        {
            var result = await _motoService.ObterPorPagina(numeroPag, tamanhoPag);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(MotoResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] MotoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var motoCriada = await _motoService.Criar(request);
            return CreatedAtAction(nameof(GetByPlaca), new { valor = motoCriada.Placa }, motoCriada);
        }

        [HttpPut("placa")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(string placa, [FromBody] MotoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var atualizada = await _motoService.Atualizar(placa, request);
            if (!atualizada)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("placa")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string placa)
        {
            var removida = await _motoService.Remover(placa);
            if (!removida)
                return NotFound();

            return NoContent();
        }
    }
}
