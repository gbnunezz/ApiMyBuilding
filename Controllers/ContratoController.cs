using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContratoController : ControllerBase
    {
        private readonly MyBuildingContext _context;

        public ContratoController(MyBuildingContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Contratos contrato, [FromQuery] Guid idPredio)
        {
            if (contrato == null)
                return BadRequest("Dados inválidos.");

            // 1) Buscar prédio + financeiro já criado automaticamente
            var predio = _context.Building
                .Include(b => b.Financeiro)
                .FirstOrDefault(b => b.Id == idPredio);

            if (predio == null)
                return NotFound("Prédio não encontrado.");

            // 2) Garantir que o Financeiro esteja persistido e com Id válido
            var financeiro = predio.Financeiro;
            if (financeiro == null)
            {
                // Por segurança, se a inicialização não aconteceu
                financeiro = new Financeiro { Id = Guid.NewGuid() };
                predio.Financeiro = financeiro;
                _context.Financeiro.Add(financeiro);
                _context.Building.Update(predio);
                _context.SaveChanges();
            }
            else if (financeiro.Id == Guid.Empty)
            {
                // Se veio instanciado sem Id, atribui e persiste
                financeiro.Id = Guid.NewGuid();
                _context.Financeiro.Add(financeiro);
                _context.Building.Update(predio);
                _context.SaveChanges();
            }

            // 3) Criar contrato
            contrato.Id = Guid.NewGuid();
            contrato.Created = DateTime.Now;

            _context.Contratos.Add(contrato);
            _context.SaveChanges();

            // 4) Criar registro financeiro (Entrada) referente à criação do contrato
            var registro = new RegistroFinanceiro
            {
                Id = Guid.NewGuid(),
                FinanceiroId = financeiro.Id,
                Tipo = "Entrada",
                Valor = contrato.Valor,
                Data = DateTime.Now,
                Descricao = $"Criação do contrato entre Morador {contrato.Morador} e Proprietário {contrato.Proprietario}"
            };

            _context.RegistroFinanceiro.Add(registro);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = contrato.Id }, contrato);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var contratos = _context.Contratos
                .Include(c => c.Atrasos)
                .ToList();

            return Ok(contratos);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var contrato = _context.Contratos
                .Include(c => c.Atrasos)
                .FirstOrDefault(c => c.Id == id);

            if (contrato == null)
                return NotFound("Contrato não encontrado.");

            return Ok(contrato);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] Contratos atualizado)
        {
            var contrato = _context.Contratos.Find(id);

            if (contrato == null)
                return NotFound("Contrato não encontrado.");

            contrato.Morador = atualizado.Morador;
            contrato.Proprietario = atualizado.Proprietario;
            contrato.Valor = atualizado.Valor;
            contrato.Expiracao = atualizado.Expiracao;

            _context.Contratos.Update(contrato);
            _context.SaveChanges();

            return Ok(contrato);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var contrato = _context.Contratos.Find(id);

            if (contrato == null)
                return NotFound("Contrato não encontrado.");

            _context.Contratos.Remove(contrato);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
