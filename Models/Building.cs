using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Building
    {
        [Key]
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Guid IdDono { get; set; }
        public List<Guid> Moradores { get; set; } = new List<Guid>();
        public Financeiro Financeiro { get; set; } = new Financeiro();

        public Building() { }


        public Building(string nome, string descricao, Guid IdDono)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
            this.IdDono = IdDono;
        }
    }
}
