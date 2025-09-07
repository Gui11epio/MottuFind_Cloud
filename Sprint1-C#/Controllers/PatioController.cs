using Microsoft.AspNetCore.Mvc;
using Sprint1_C_.Application.DTOs.Requests;
using Sprint1_C_.Application.DTOs.Response;
using Sprint1_C_.Application.Services;

namespace Sprint1_C_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatioController : ControllerBase
    {
        private readonly PatioService _patioService;

        public PatioController(PatioService service)
        {
            _patioService = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PatioResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll()
        {
            var patios = await _patioService.ObterTodos();
            if (patios == null || !patios.Any()) return NoContent();
            return Ok(patios);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PatioResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var patio = await _patioService.ObterPorId(id);
            if (patio == null) return NotFound();
            return Ok(patio);
        }

        [HttpGet("pagina")]
        [ProducesResponseType(typeof(PagedResult<PatioResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<PagedResult<PatioResponse>>> GetPaged(int numeroPag = 1, int tamanhoPag = 10)
        {
            var result = await _patioService.ObterPorPagina(numeroPag, tamanhoPag);
            if (result == null || !result.Itens.Any()) return NoContent();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PatioResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] PatioRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _patioService.Criar(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] PatioRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _patioService.Atualizar(id, request);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _patioService.Remover(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
