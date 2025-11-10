using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Backend.DTO
{
    public class BuildingDTO
    {
        public string Nome { get; set; }
        public string Descricao { get; set; } 
        public Guid IdDono { get; set; }

        public BuildingDTO() { }


    }
}