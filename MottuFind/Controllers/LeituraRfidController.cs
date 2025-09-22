using Microsoft.AspNetCore.Mvc;
using MottuFind_C_.Application.Services;
using Sprint1_C_.Application.DTOs.Requests;
using Sprint1_C_.Application.DTOs.Response;
using Sprint1_C_.Application.Services;

namespace MottuFind.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeituraRfidController : ControllerBase
    {
        private readonly LeituraRfidService _leituraService;

        public LeituraRfidController(LeituraRfidService service)
        {
            _leituraService = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LeituraRfidResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll()
        {
            var leituras = await _leituraService.ObterTodos();
            if (leituras == null || !leituras.Any()) return NoContent();
            return Ok(leituras);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LeituraRfidResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var leitura = await _leituraService.ObterPorId(id);
            if (leitura == null) return NotFound();
            return Ok(leitura);
        }

        [HttpGet("pagina")]
        [ProducesResponseType(typeof(PagedResult<LeituraRfidResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<PagedResult<LeituraRfidResponse>>> GetPaged(int numeroPag = 1, int tamanhoPag = 10)
        {
            var result = await _leituraService.ObterPorPagina(numeroPag, tamanhoPag);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(LeituraRfidResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] LeituraRfidRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _leituraService.Criar(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] LeituraRfidRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _leituraService.Atualizar(id, request);
            if (!updated) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _leituraService.Remover(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
