using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Backend.DTO;
using Backend.Models;

namespace Backend.Service.Interface
{
    public interface IBuildCreate
    {
        bool Create(BuildingDTO build);
    }

}