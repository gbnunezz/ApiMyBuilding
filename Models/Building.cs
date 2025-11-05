using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Building
    {
        [Key]
        public Guid Id { get; set; }
        public byte[] Foto { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Guid IdDono { get; set; }
        public List<Guid> Moradores { get; set; } = new List<Guid>();

        public Building(byte[] foto, string nome, string descricao)
        {
            Id = Guid.NewGuid();
            Foto = foto;
            Nome = nome;
            Descricao = descricao;
        }
    }
}