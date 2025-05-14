using Microsoft.AspNetCore.Mvc;
using Sprint1_C_.Application.DTOs.Requests;
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
        public IActionResult GetAll() => Ok(_patioService.ObterTodos());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var patio = _patioService.ObterPorId(id);
            if (patio == null) return NotFound();
            return Ok(patio);
        }

        [HttpPost]
        public IActionResult Create([FromBody] PatioRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = _patioService.Criar(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] PatioRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = _patioService.Atualizar(id, request);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _patioService.Remover(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
