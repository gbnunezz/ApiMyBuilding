using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTO;
using Backend.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Build")]
    public class BuildingController : ControllerBase
    {
        [HttpPost]
        public ActionResult Create(
            [FromForm]BuildingDTO building,
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
    
    }
}