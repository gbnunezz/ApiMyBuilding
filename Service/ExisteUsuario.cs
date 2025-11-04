using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Models;
using Backend.Service.Interface;

namespace Backend.Service
{
    public class ExisteUsuario : IExisteUsuario
    {
        private readonly MyBuildingContext _context;

        public ExisteUsuario(MyBuildingContext context)
        {
            _context = context;
        }

        public bool Existe(Usuario Usuario)
        {
            Usuario? usuario = _context.Usuarios.FirstOrDefault(x =>
                x.Id == Usuario.Id &&
                x.Email == Usuario.Email
            );

            if (usuario != null)
            {
                return true;
            }
            
            return false;
        }
    }
}