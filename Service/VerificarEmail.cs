using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Models;
using Backend.Service.Interface;

namespace Backend.Service
{
    public class VerificarEmail : IVerificarEmail
    {
        private readonly MyBuildingContext _context;
        public VerificarEmail(MyBuildingContext context)
        {
            _context = context;
        } 
        public bool Verificar(string email)
        {
            if (!email.Contains("@"))
            {
                return false;
            }

            if (email.Length > 320)
            {
                return false;
            }

            Usuario? user = _context.Usuarios.FirstOrDefault(x => x.Email == email);

            if (user != null)
            {
                return false;
            }

            return true;
        }
    }
}