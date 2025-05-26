using API.Codejourney.Models;
using API.Codejourney.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Codejourney.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class JornadaController : ControllerBase
    {
        private readonly IJornadaRepository _jornadaRepository;

        public JornadaController(IJornadaRepository jornadaRepository)
        {
            _jornadaRepository = jornadaRepository;
        }

        [HttpPost("inserir")]
        public IActionResult InserirJornada([FromBody] Jornadas jornada, [FromQuery] int usuarioId)
        {
            if (jornada == null)
            {
                return BadRequest("Dados da jornada inválidos.");
            }

            _jornadaRepository.Insert(jornada, usuarioId);
            return Ok("Jornada inserida com sucesso.");
        }

        [HttpGet("pegar")]
        public IActionResult GetJornadasPorNome([FromQuery] string jornNome, [FromQuery] int idUsuario)
        {
            var jornadas = _jornadaRepository.GetJornadas(jornNome, idUsuario);
            if (jornadas == null || !jornadas.Any())
            {
                return NotFound("Nenhuma jornada encontrada com esse nome.");
            }

            return Ok(jornadas);
        }

        [HttpPut("atualizar/{id}")]
        public IActionResult AtualizarJornada(int id, [FromBody] Jornadas jornadaAtualizada)
        {
            if (jornadaAtualizada == null)
            {
                return BadRequest("Dados da jornada inválidos.");
            }

            _jornadaRepository.UpdateJornada(id, jornadaAtualizada);
            return Ok("Jornada atualizada com sucesso.");
        }
    }
}
