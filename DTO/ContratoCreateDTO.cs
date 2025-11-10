using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTO
{
    public class ContratoCreateDTO
    {
        public Guid Morador { get; set; }
        public Guid Proprietario { get; set; }
        public decimal Valor { get; set; }
        public DateTime Expiracao { get; set; }
    }
}