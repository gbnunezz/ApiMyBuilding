using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Backend.Models
{
    public class Financeiro
    {
        [Key]
        public Guid Id { get; set; }
        public List<RegistroFinanceiro> Registros { get; set; } = new();
        public List<ContratoResumo> Contratos { get; set; } = new();

        // 🔹 Campos auxiliares
        public DateTime UltimaAtualizacao { get; set; } = DateTime.Now;

        public Financeiro()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Adiciona um novo registro financeiro.
        /// </summary>
        public void AdicionarRegistro(decimal valor, string tipo, string descricao = "")
        {
            Registros.Add(new RegistroFinanceiro
            {
                Id = Guid.NewGuid(),
                Tipo = tipo,
                Valor = valor,
                Data = DateTime.Now,
                Descricao = descricao
            });
            UltimaAtualizacao = DateTime.Now;
        }

        /// <summary>
        /// Retorna o total de entradas no período.
        /// </summary>
        public decimal TotalEntradas(DateTime? inicio = null, DateTime? fim = null)
        {
            return FiltrarPorPeriodo(inicio, fim)
                .Where(r => r.Tipo.Equals("entrada", StringComparison.OrdinalIgnoreCase))
                .Sum(r => r.Valor);
        }

        /// <summary>
        /// Retorna o total de saídas no período.
        /// </summary>
        public decimal TotalSaidas(DateTime? inicio = null, DateTime? fim = null)
        {
            return FiltrarPorPeriodo(inicio, fim)
                .Where(r => r.Tipo.Equals("saida", StringComparison.OrdinalIgnoreCase))
                .Sum(r => r.Valor);
        }

        /// <summary>
        /// Retorna o saldo (entradas - saídas).
        /// </summary>
        public decimal Saldo(DateTime? inicio = null, DateTime? fim = null)
        {
            return TotalEntradas(inicio, fim) - TotalSaidas(inicio, fim);
        }

        /// <summary>
        /// Retorna os registros financeiros filtrados por período.
        /// </summary>
        public IEnumerable<RegistroFinanceiro> FiltrarPorPeriodo(DateTime? inicio, DateTime? fim)
        {
            return Registros.Where(r =>
                (!inicio.HasValue || r.Data >= inicio.Value) &&
                (!fim.HasValue || r.Data <= fim.Value));
        }

        /// <summary>
        /// Retorna um resumo mensal (para gráficos de barra/linha).
        /// </summary>
        public List<ResumoMensal> ResumoMensal()
        {
            return Registros
                .GroupBy(r => new { r.Data.Year, r.Data.Month })
                .Select(g => new ResumoMensal
                {
                    Ano = g.Key.Year,
                    Mes = g.Key.Month,
                    Entradas = g.Where(x => x.Tipo == "entrada").Sum(x => x.Valor),
                    Saidas = g.Where(x => x.Tipo == "saida").Sum(x => x.Valor),
                    Saldo = g.Where(x => x.Tipo == "entrada").Sum(x => x.Valor)
                           - g.Where(x => x.Tipo == "saida").Sum(x => x.Valor)
                })
                .OrderBy(x => x.Ano).ThenBy(x => x.Mes)
                .ToList();
        }

        /// <summary>
        /// Calcula taxa de inadimplência (% de contratos inadimplentes / totais)
        /// </summary>
        public double TaxaInadimplencia()
        {
            if (Contratos.Count == 0) return 0;
            int inadimplentes = Contratos.Count(c => c.Status == "Inadimplente");
            return Math.Round((double)inadimplentes / Contratos.Count * 100, 2);
        }

        /// <summary>
        /// Total de contratos ativos atualmente.
        /// </summary>
        public int TotalContratosAtivos()
        {
            return Contratos.Count(c => c.Status == "Ativo");
        }

        /// <summary>
        /// Valor total de aluguéis recebidos.
        /// </summary>
        public decimal TotalAlugueisRecebidos()
        {
            return Contratos.Sum(c => c.TotalRecebido);
        }

        /// <summary>
        /// Retorna um ranking dos 5 maiores locatários por pagamento total.
        /// </summary>
        public List<RankingLocatario> TopLocatarios(int top = 5)
        {
            return Contratos
                .GroupBy(c => c.MoradorId)
                .Select(g => new RankingLocatario
                {
                    MoradorId = g.Key,
                    TotalPago = g.Sum(c => c.TotalRecebido)
                })
                .OrderByDescending(x => x.TotalPago)
                .Take(top)
                .ToList();
        }
    }


    public class RegistroFinanceiro
    {
        [Key]
        public Guid Id { get; set; }

        public string Tipo { get; set; } // "entrada" ou "saida"
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }

        [ForeignKey("FinanceiroId")]
        public Guid FinanceiroId { get; set; }
        public Financeiro Financeiro { get; set; }
    }

    public class ContratoResumo
    {
        [Key]
        public Guid Id { get; set; }

        public Guid MoradorId { get; set; }
        public Guid ProprietarioId { get; set; }

        public decimal ValorMensal { get; set; }
        public decimal TotalRecebido { get; set; }

        public string Status { get; set; } = "Ativo"; // "Ativo", "Encerrado", "Inadimplente"
        public DateTime Inicio { get; set; }
        public DateTime? Fim { get; set; }

        [ForeignKey("FinanceiroId")]
        public Guid FinanceiroId { get; set; }
        public Financeiro Financeiro { get; set; }

        // Métodos auxiliares
        public int MesesAtivos()
        {
            DateTime dataFim = Fim ?? DateTime.Now;
            return ((dataFim.Year - Inicio.Year) * 12) + dataFim.Month - Inicio.Month;
        }

        public decimal MediaMensalRecebida()
        {
            int meses = MesesAtivos();
            return meses > 0 ? TotalRecebido / meses : TotalRecebido;
        }
    }

    public class ResumoMensal
    {
        public int Ano { get; set; }
        public int Mes { get; set; }
        public decimal Entradas { get; set; }
        public decimal Saidas { get; set; }
        public decimal Saldo { get; set; }

        [NotMapped]
        public string NomeMes => new DateTime(Ano, Mes, 1).ToString("MMMM");
    }

    public class RankingLocatario
    {
        public Guid MoradorId { get; set; }
        public decimal TotalPago { get; set; }
    }
}
