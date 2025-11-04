using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTO;

namespace Backend.Service.Interface
{
    public interface IAuthLogin
    {
        public bool Login(UsuarioLoginDTO usuario);
    }
}