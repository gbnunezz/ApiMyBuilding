using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Backend.DTO
{
    public class UsuarioLoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public UsuarioLoginDTO(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}