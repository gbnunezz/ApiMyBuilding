using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Repository.Interface
{
    public interface ICreateUser
    {
        public bool CreateUser(Usuario usuario);
    }

    public interface IGetbyEmail
    {
        public Usuario GetbyEmail(string email);
    }
}