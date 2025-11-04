using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.DTO;
using Backend.Models;
using Backend.Repository.Interface;
using Backend.Service.Interface;

namespace Backend.Service
{
    public class AuthService : IAuthCadastro,IAuthLogin
    {
        private readonly ICreateUser _cadastrar;
        private readonly IPasswordHasher _criptografarSenha;
        private readonly MyBuildingContext _context;
        private readonly IJwtService _jwtService;

        public AuthService(ICreateUser cadastrar,IPasswordHasher passwordHasher,MyBuildingContext context,IJwtService jwtService)
        {
            _jwtService = jwtService;
            _context = context;
            _cadastrar = cadastrar;
            _criptografarSenha = passwordHasher;
        }
        public bool Cadastro(UsuarioCadastroDTO usuario)
        {

            var hashSenha = _criptografarSenha.HashPassword(usuario.Password);

            Usuario usuario1 = new Usuario(
                usuario.Nome, usuario.Email, 
                hashSenha, usuario.TipoUsuario
            );
            
            bool sucess = _cadastrar.CreateUser(usuario1);

            if (sucess == false)
            {
                return false;
            }

            return true;
        }

        public bool Login(UsuarioLoginDTO usuario)
        {
            var user = _context.Usuarios.FirstOrDefault(x => x.Email == usuario.Email);
            if (user == null)
            {
                return false;
            }

            if (_criptografarSenha.VerifyPassword(usuario.Password, user.Password) == false)
            {
                return false;
            }

            return true;
        }
    }
}