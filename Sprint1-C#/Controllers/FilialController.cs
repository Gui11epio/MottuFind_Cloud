using Microsoft.AspNetCore.Mvc;
using Sprint1_C_.Application.DTOs.Requests;
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
        public IActionResult GetAll() => Ok(_filialService.ObterTodos());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var filial = _filialService.ObterPorId(id);
            if (filial == null) return NotFound();
            return Ok(filial);
        }

        [HttpPost]
        public IActionResult Create([FromBody] FilialRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = _filialService.Criar(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] FilialRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = _filialService.Atualizar(id, request);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _filialService.Remover(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
