using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.DTO;
using Backend.Repository.Interface;
using Backend.Service.Interface;

namespace Backend.Service.LogicaController
{
    public class BuildingService : IBuildCreate
    {
        private readonly MyBuildingContext _context;
        private readonly ICreateBuilding _serviceCreate;

        public BuildingService(MyBuildingContext context, ICreateBuilding serviceCreate)
        {
            _context = context;
            _serviceCreate = serviceCreate;
        }
        public bool Create(BuildingDTO build)
        {
            if (build == null)
            {
                return false;
            }
            var building = _context.Building.FirstOrDefault(x =>
                x.Nome == build.Nome && x.IdDono == build.IdDono
            );

            if (building != null)
            {
                return false;
            }

            bool create = _serviceCreate.Create(build);

            if (create == false)
            {
                return false;
            }

            return true;
        }
    }
}