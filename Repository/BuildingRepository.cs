using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.DTO;
using Backend.Models;
using Backend.Repository.Interface;
using Backend.Service.Interface;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Backend.Repository
{
    public class BuildingRepository : IGetById,ICreateBuilding,IDeleteBuilding,IPatchMoradores
    {
        public readonly ItransformeFileEmBytes _transfomeByte;
        private readonly MyBuildingContext _context;

        public BuildingRepository(ItransformeFileEmBytes transfomeByte, MyBuildingContext context)
        {
            _transfomeByte = transfomeByte;
            _context = context;
        }
        public bool Create(BuildingDTO build)
        {
            if (build == null)
            {
                return false;
            }
            Building building = new Building(
                build.Nome, build.Descricao, build.IdDono
            );

            try
            {
                _context.Building.Add(building);
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
            
            
        }

        public bool DeleteBuilding(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Building> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool PatchMoradores(List<Guid> Morasdores, Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}