using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Contratos
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Morador { get; set; }
        public Guid Proprietario { get; set; }
        public decimal Valor { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expiracao { get; set; }
        public List<Atraso> Atrasos { get; set; } = new();

        public Contratos() { }

        public Contratos(Guid morador, Guid proprietario, decimal valor, DateTime expiration)
        {
            Id = Guid.NewGuid();
            Morador = morador;
            Proprietario = proprietario;
            Valor = valor;
            Created = DateTime.Now;
            Expiracao = expiration;
        }


        public void AdicionarAtraso(DateTime data, decimal valor)
        {
            Atrasos.Add(new Atraso(data, valor) { ContratoId = Id });
        }

        public decimal TotalAtrasos()
        {
            decimal total = 0;
            foreach (var atraso in Atrasos)
                total += atraso.Value;
            return total;
        }

        public bool TemAtrasoEm(DateTime data)
        {
            return Atrasos.Exists(a => a.Date.Date == data.Date);
        }

        public bool RemoverAtraso(DateTime data)
        {
            var atraso = Atrasos.Find(a => a.Date.Date == data.Date);
            if (atraso == null) return false;
            Atrasos.Remove(atraso);
            return true;
        }
    }
}
