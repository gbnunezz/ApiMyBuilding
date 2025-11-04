using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Models;
using Backend.Repository.Interface;
using Backend.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Repository
{
    public class UserRepository : ICreateUser,IGetbyEmail
    {
        private readonly MyBuildingContext _context;
        private readonly IExisteUsuario _existe;


        public UserRepository(MyBuildingContext context,IExisteUsuario existe)
        {
            _context = context;
            _existe = existe;
        }

        public bool CreateUser(Usuario usuario)
        {
            if (_existe.Existe(usuario) == true)
            {
                return false;
            }

            try
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public Usuario GetbyEmail(string email)
        {
            Usuario usuario = _context.Usuarios.FirstOrDefault(x => x.Email == email) ?? throw new Exception("Erro ao procurar usuario");
            return usuario;
        }
    }
}