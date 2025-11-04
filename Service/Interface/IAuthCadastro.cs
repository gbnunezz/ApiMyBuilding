using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTO;
using Backend.Models;

namespace Backend.Service.Interface
{
    public interface IAuthCadastro
    {
        public bool Cadastro(UsuarioCadastroDTO usuario);
    }
}