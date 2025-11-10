using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinanceiroController : ControllerBase
    {
        private readonly MyBuildingContext _context;

        public FinanceiroController(MyBuildingContext context)
        {
            _context = context;
        }

        // ================== CRUD ==================

        [HttpGet]
        public ActionResult<List<Financeiro>> GetAll()
        {
            var lista = _context.Financeiro
                .Include(f => f.Registros)
                .Include(f => f.Contratos)
                .ToList();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public ActionResult<Financeiro> GetById(Guid id)
        {
            var financeiro = _context.Financeiro
                .Include(f => f.Registros)
                .Include(f => f.Contratos)
                .FirstOrDefault(f => f.Id == id);

            if (financeiro == null)
                return NotFound("Financeiro não encontrado.");

            return Ok(financeiro);
        }


        [HttpGet("{id}/saldo")]
        public ActionResult<object> GetSaldo(Guid id)
        {
            var financeiro = _context.Financeiro
                .Include(f => f.Registros)
                .FirstOrDefault(f => f.Id == id);

            if (financeiro == null)
                return NotFound();

            return Ok(new
            {
                SaldoAtual = financeiro.Saldo(),
                Entradas = financeiro.TotalEntradas(),
                Saidas = financeiro.TotalSaidas(),
                UltimaAtualizacao = financeiro.UltimaAtualizacao
            });
        }

        [HttpGet("{id}/resumo-mensal")]
        public ActionResult<List<ResumoMensal>> GetResumoMensal(Guid id)
        {
            var financeiro = _context.Financeiro
                .Include(f => f.Registros)
                .FirstOrDefault(f => f.Id == id);

            if (financeiro == null)
                return NotFound();

            return Ok(financeiro.ResumoMensal());
        }

        [HttpGet("{id}/inadimplencia")]
        public ActionResult<object> GetInadimplencia(Guid id)
        {
            var financeiro = _context.Financeiro
                .Include(f => f.Contratos)
                .FirstOrDefault(f => f.Id == id);

            if (financeiro == null)
                return NotFound();

            return Ok(new
            {
                TotalContratos = financeiro.Contratos.Count,
                Inadimplentes = financeiro.Contratos.Count(c => c.Status == "Inadimplente"),
                Taxa = financeiro.TaxaInadimplencia()
            });
        }

        [HttpGet("{id}/contratos")]
        public ActionResult<object> GetContratosResumo(Guid id)
        {
            var financeiro = _context.Financeiro
                .Include(f => f.Contratos)
                .FirstOrDefault(f => f.Id == id);

            if (financeiro == null)
                return NotFound();

            return Ok(new
            {
                Ativos = financeiro.TotalContratosAtivos(),
                TotalRecebido = financeiro.TotalAlugueisRecebidos(),
                TopLocatarios = financeiro.TopLocatarios()
            });
        }

        [HttpGet("{id}/dashboard")]
        public ActionResult<object> GetDashboard(Guid id)
        {
            var financeiro = _context.Financeiro
                .Include(f => f.Registros)
                .Include(f => f.Contratos)
                .FirstOrDefault(f => f.Id == id);

            if (financeiro == null)
                return NotFound();

            return Ok(new
            {
                Saldo = financeiro.Saldo(),
                Entradas = financeiro.TotalEntradas(),
                Saidas = financeiro.TotalSaidas(),
                TaxaInadimplencia = financeiro.TaxaInadimplencia(),
                TotalContratosAtivos = financeiro.TotalContratosAtivos(),
                TopLocatarios = financeiro.TopLocatarios()
            });
        }
    }
}
