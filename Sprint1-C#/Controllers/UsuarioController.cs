using Microsoft.AspNetCore.Mvc;
using Sprint1_C_.Application.DTOs.Requests;
using Sprint1_C_.Application.DTOs.Response;
using Sprint1_C_.Application.Services;

namespace Sprint1_C_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService service)
        {
            _usuarioService = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UsuarioResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioService.ObterTodos();
            if (usuarios == null || !usuarios.Any()) return NoContent();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _usuarioService.ObterPorId(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        [HttpGet("pagina")]
        [ProducesResponseType(typeof(PagedResult<UsuarioResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<PagedResult<UsuarioResponse>>> GetPaged(int numeroPag = 1, int tamanhoPag = 10)
        {
            var result = await _usuarioService.ObterPorPagina(numeroPag, tamanhoPag);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] UsuarioRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _usuarioService.Criar(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UsuarioRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _usuarioService.Atualizar(id, request);
            if (!updated) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _usuarioService.Remover(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
