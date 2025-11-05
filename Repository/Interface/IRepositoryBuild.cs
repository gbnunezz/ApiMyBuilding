using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTO;
using Backend.Models;

namespace Backend.Repository.Interface
{
    public interface IGetById
    {
        List<Building> GetById(Guid id);
    }

    public interface ICreateBuilding
    {
        bool Create(BuildingDTO build);
    }

    public interface IDeleteBuilding
    {
        bool DeleteBuilding(Guid id);
    }

    public interface IPatchMoradores
    {
        bool PatchMoradores(List<Guid> Morasdores,Guid Id);
    }
}