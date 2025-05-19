using Microsoft.AspNetCore.Mvc;
using Sprint1_C_.Application.DTOs.Requests;
using Sprint1_C_.Application.DTOs.Response;
using Sprint1_C_.Application.Services;

namespace Sprint1_C_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilialController : ControllerBase
    {
        private readonly FilialService _filialService;

        public FilialController(FilialService service)
        {
            _filialService = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FilialResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAll() => Ok(_filialService.ObterTodos());

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FilialResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var filial = _filialService.ObterPorId(id);
            if (filial == null) return NotFound();
            return Ok(filial);
        }

        [HttpGet("pagina")]
        [ProducesResponseType(typeof(PagedResult<FilialResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<PagedResult<FilialResponse>>> GetPaged(int numeroPag = 1, int tamanhoPag = 10)
        {
            var result = await _filialService.ObterPorPagina(numeroPag, tamanhoPag);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(FilialResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] FilialRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = _filialService.Criar(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, [FromBody] FilialRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = _filialService.Atualizar(id, request);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var deleted = _filialService.Remover(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
