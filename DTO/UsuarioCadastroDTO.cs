using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.DTO
{
    public class UsuarioCadastroDTO
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public TipoUsuario TipoUsuario { get; set; }

        public UsuarioCadastroDTO(string nome, string email, string password, TipoUsuario tipoUsuario)
        {
            Nome = nome.ToLower();
            Email = email;
            Password = password;
            TipoUsuario = tipoUsuario;
        }
    }
}