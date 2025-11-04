using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Service.Interface
{
    public interface IExisteUsuario
    {
        public bool Existe(Usuario usuario);
    }
}