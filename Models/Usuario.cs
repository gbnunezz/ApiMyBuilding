using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Backend.Service;
using Backend.Service.Interface;

namespace Backend.Models
{
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public TipoUsuario TipoUsuario { get; set; }

        public Usuario(string nome,string email,string password,TipoUsuario tipoUsuario)
        {
            Id = Guid.NewGuid();
            Nome = nome.ToLower();
            Email = email;
            Password = password;
            TipoUsuario = tipoUsuario;
        }

    }
}