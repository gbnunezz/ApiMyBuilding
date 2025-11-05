using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Service.Interface;

namespace Backend.Service
{
    public class TransformeFileEmBytes : ItransformeFileEmBytes
    {
        public byte[] Convert(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}