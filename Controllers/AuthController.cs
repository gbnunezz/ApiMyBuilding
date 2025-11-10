using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTO;
using Backend.Models;
using Backend.Repository;
using Backend.Service;
using Backend.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Auth")]
    public class AuthController : ControllerBase
    {

        [HttpPost("cadastro")]
        public IActionResult Cadastro(
            [FromBody] UsuarioCadastroDTO usuario,
            [FromServices] IAuthCadastro _service)
        {
            bool sucess = _service.Cadastro(usuario);

            if (sucess == false)
            {
                return BadRequest();
            }

            return Created();
        }

        [HttpPost("login")]
        public IActionResult Login(
            [FromBody] UsuarioLoginDTO usuario,
            [FromServices] IJwtService jwtService,
            [FromServices] IAuthLogin service,
            [FromServices] UserRepository repository
        )
        {
            if (service.Login(usuario) == false)
            {
                return BadRequest("Erro ao fazer login");
            }
            
            Usuario user = repository.GetbyEmail(usuario.Email);

            string jwt = jwtService.GenerateToken(user);
            return Ok(user.Id);
        }
    }
}