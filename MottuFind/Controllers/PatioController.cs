using Microsoft.AspNetCore.Mvc;
using MottuFind_C_.Domain.Entities;
using Sprint1_C_.Application.DTOs.Requests;
using Sprint1_C_.Application.DTOs.Response;
using Sprint1_C_.Application.Services;
using Sprint1_C_.Domain.Entities;

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
        [ProducesResponseType(typeof(Resource<PatioResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var patio = await _patioService.ObterPorId(id);
            if (patio == null) return NotFound();


            var resource = new Resource<PatioResponse>
            {
                Data = patio,
                Links =
        {
                    new Link { Href = Url.Action(nameof(GetById), new { id }), Rel = "self", Method = "GET" },
                    new Link { Href = Url.Action(nameof(Update), new { id }), Rel = "update", Method = "PUT" },
                    new Link { Href = Url.Action(nameof(Delete), new { id }), Rel = "delete", Method = "DELETE" },
                    new Link { Href = Url.Action(nameof(GetAll)), Rel = "all", Method = "GET" }
        }
            };

            return Ok(resource);
        }

        [HttpGet("pagina")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Resource<PagedResult<PatioResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Resource<PagedResult<PatioResponse>>>> GetPaged(int numeroPag = 1, int tamanhoPag = 10)
        {
            var result = await _patioService.ObterPorPagina(numeroPag, tamanhoPag);
            if (result == null || !result.Itens.Any()) return NoContent();


            var resource = new Resource<PagedResult<PatioResponse>>
            {
                Data = result,
                Links =
        {
            new Link { Href = Url.Action(nameof(GetPaged), new { numeroPag, tamanhoPag }), Rel = "self", Method = "GET" }
        }
            };

            // adiciona links de próxima e anterior se aplicável
            if ((numeroPag * tamanhoPag) < result.Total)
                resource.Links.Add(new Link
                {
                    Href = Url.Action(nameof(GetPaged), new { numeroPag = numeroPag + 1, tamanhoPag }),
                    Rel = "next",
                    Method = "GET"
                });

            if (numeroPag > 1)
                resource.Links.Add(new Link
                {
                    Href = Url.Action(nameof(GetPaged), new { numeroPag = numeroPag - 1, tamanhoPag }),
                    Rel = "prev",
                    Method = "GET"
                });

            return Ok(resource);
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
