using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class MyBuildingContext : DbContext
    {
        public MyBuildingContext(DbContextOptions<MyBuildingContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Building> Building { get; set; }
        public DbSet<Contratos> Contratos { get; set; }
        public DbSet<Atraso> Atrasos { get; set; }
        public DbSet<Financeiro> Financeiro { get; set; }
        public DbSet<RegistroFinanceiro> RegistroFinanceiro { get; set; }
        public DbSet<ContratoResumo> ContratoResumo { get; set; }

    }
}