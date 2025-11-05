using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Service.Interface
{
    public interface ItransformeFileEmBytes 
    {
        public byte[] Convert(IFormFile file);
    }
}