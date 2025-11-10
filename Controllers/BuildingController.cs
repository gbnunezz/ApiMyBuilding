using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.DTO;
using Backend.Models;
using Backend.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Build")]
    public class BuildingController : ControllerBase
    {

        public readonly MyBuildingContext _context;

        public BuildingController(MyBuildingContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult Create(
            [FromBody] BuildingDTO building,
            [FromServices] IBuildCreate _buildCreate
        )
        {
            bool result = _buildCreate.Create(building);

            if (result == false)
            {
                return BadRequest();
            }

            return Created();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] Guid id)
        {
            var item = _context.Building.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Building.Remove(item);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult GetById([FromRoute] Guid id)
        {
            var builds = _context.Building
                .Where(x => x.IdDono == id)
                .ToList();

            return Ok(builds);
        }

        [HttpGet("fincanceiro/{id}")]
        public ActionResult fianceio([FromRoute] Guid id)
        {
            var builds = _context.Building
                .FirstOrDefault(x => x.Id == id);

            return Ok(builds.Financeiro.Id);
        }

        [HttpGet("Moradores/{Id}")]
        public IActionResult BuscarMoradores(Guid id)
        {
            // 1. Verifica se o prédio existe
            var building = _context.Building.FirstOrDefault(x => x.Id == id);
            if (building == null)
            {
                return BadRequest("O ID do prédio é inválido.");
            }

            // 2. Pega a lista de GUIDs dos moradores do prédio
            var listaGuids = building.Moradores; // já é List<Guid>

            if (listaGuids == null || !listaGuids.Any())
            {
                return NotFound("Nenhum morador encontrado para esse prédio.");
            }

            // 3. Busca os usuários cujos IDs estão na lista de moradores
            var listMoradores = _context.Usuarios
                .Where(u => listaGuids.Contains(u.Id))
                .ToList();

            // 4. Retorna a lista
            return Ok(listMoradores);
        }

    }
}