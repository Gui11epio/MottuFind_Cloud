using Microsoft.AspNetCore.Mvc;
using Sprint1_C_.Application.DTOs.Requests;
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
        public IActionResult GetAll()
        {
            var motos = _motoService.ObterTodos();
            return Ok(motos);
        }

        

        [HttpGet("placa")]
        public IActionResult GetByPlaca([FromQuery] string valor)
        {
            var moto = _motoService.ObterPorPlaca(valor);
            if (moto == null)
                return NotFound();
            return Ok(moto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] MotoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var motoCriada = _motoService.Criar(request);
            return CreatedAtAction(nameof(GetByPlaca), new { placa = motoCriada.Placa }, motoCriada);
        }

        [HttpPut("placa")]
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
        public IActionResult Delete(string placa)
        {
            var removida = _motoService.Remover(placa);
            if (!removida)
                return NotFound();

            return NoContent();
        }
    }
}
