using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Codejourney.Authorization;
using API.Codejourney.DTO;
using API.Codejourney.Models;
using API.Codejourney.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Codejourney.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private IUsuariosRepository _repository;
        private IConfiguration _configuration;

        public UsuariosController(IUsuariosRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return Ok(_repository.GetUsuarios());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var usuario = _repository.Get(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);

        }

        [HttpPost("cadastro")]
        [AllowAnonymous]
        public IActionResult Insert([FromBody] Usuario usuario)
        {
            var usuarioExistente = _repository
                .GetUsuarios()
                .FirstOrDefault(u => u.UserName == usuario.UserName || u.Email == usuario.Email);

            if (usuarioExistente != null)
            {
                return BadRequest("Já existe um usuário com esse nome de usuário.");
            }

            usuario.DataCadastro = DateTime.Now;

            _repository.Insert(usuario);
            return Ok(usuario);
        }

        [HttpPut("atualizar/{id}")]
        public IActionResult Update(int id, [FromBody] Usuario usuario)
        {
            if (usuario == null)
                return BadRequest("Usuário inválido.");

            var usuarioExistente = _repository.Get(id);
            if (usuarioExistente == null)
                return NotFound("Usuário não encontrado.");

            var todosUsuarios = _repository.GetUsuarios();

            if (!string.IsNullOrEmpty(usuario.UserName) &&
                todosUsuarios.Any(u => u.Id != id && u.UserName == usuario.UserName))
            {
                return Conflict("UserName já está em uso por outro usuário.");
            }

            if (!string.IsNullOrEmpty(usuario.Email) &&
                todosUsuarios.Any(u => u.Id != id && u.Email == usuario.Email))
            {
                return Conflict("Email já está em uso por outro usuário.");
            }

            if (!string.IsNullOrEmpty(usuario.Nome))
                usuarioExistente.Nome = usuario.Nome;

            if (!string.IsNullOrEmpty(usuario.UserName))
                usuarioExistente.UserName = usuario.UserName;

            if (!string.IsNullOrEmpty(usuario.Email))
                usuarioExistente.Email = usuario.Email;

            if (usuario.DataNascimento.HasValue && usuario.DataNascimento.Value != DateTime.MinValue)
            {
                usuarioExistente.DataNascimento = usuario.DataNascimento.Value;
            }

            if (!string.IsNullOrEmpty(usuario.Senha))
                usuarioExistente.Senha = usuario.Senha;

            _repository.Update(usuarioExistente);

            return Ok(usuarioExistente);
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return Ok();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginDto login)
        {
            var usuario = _repository
                .GetUsuarios()
                .FirstOrDefault(u => u.UserName == login.UserName && u.Senha == login.Senha);

            if (usuario == null)
                return Unauthorized("Credenciais inválidas");

            var token = GerarToken(usuario, out DateTime expiration);

            var response = new LoginResponse
            {
                TokenName = token,
                id = usuario.Id
            };

            return Ok(response);
        }

        private string GerarToken(Usuario usuario, out DateTime expiration)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.UserName),
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured")));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            expiration = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireTimeInMinutes"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPut("{id}/nivel-progresso")]
        public IActionResult UpdateNivelEProgresso(int id, [FromBody] ProgressoDTO dto)
        {
            var usuario = _repository.Get(id);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            _repository.UpdateNivelEProgresso(id, dto.Nivel, dto.Progresso);
            return Ok();
        }

        [HttpGet("{id}/nivel-progresso")]
        public IActionResult GetNivelEProgresso(int id)
        {
            var progresso = _repository.GetNivelEProgresso(id);

            if (progresso == null)
                return NotFound(new { message = "Usuário não encontrado." });

            return Ok(progresso);
        }

    }
}