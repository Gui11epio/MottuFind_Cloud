using Microsoft.AspNetCore.Mvc;
using MottuFind_C_.Domain.Entities;
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
        public async Task<IActionResult> GetAll()
        {
            var filiais = await _filialService.ObterTodos();
            if (filiais == null || !filiais.Any()) return NoContent();
            return Ok(filiais);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Resource<FilialResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var filial = await _filialService.ObterPorId(id);
            if (filial == null) return NotFound();


            var resource = new Resource<FilialResponse>
            {
                Data = filial,
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
        [ProducesResponseType(typeof(Resource<PagedResult<FilialResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Resource<PagedResult<FilialResponse>>>> GetPaged(int numeroPag = 1, int tamanhoPag = 10)
        {
            var result = await _filialService.ObterPorPagina(numeroPag, tamanhoPag);
            if (result.Itens == null || !result.Itens.Any()) return NoContent();

            var resource = new Resource<PagedResult<FilialResponse>>
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
        [ProducesResponseType(typeof(FilialResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] FilialRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _filialService.Criar(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] FilialRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _filialService.Atualizar(id, request);
            if (!updated) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _filialService.Remover(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
