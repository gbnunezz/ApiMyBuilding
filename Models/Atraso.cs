using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Atraso
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal Value { get; set; }

        [ForeignKey("Contrato")]
        public Guid ContratoId { get; set; }

        public Contratos Contrato { get; set; } = null!;

        public Atraso() { }

        public Atraso(DateTime date, decimal value)
        {
            Id = Guid.NewGuid();
            Date = date;
            Value = value;
        }
    }
}
