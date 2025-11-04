using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Service.Interface
{
    public interface IVerificarEmail
    {
        public bool Verificar(string email);
    }
}