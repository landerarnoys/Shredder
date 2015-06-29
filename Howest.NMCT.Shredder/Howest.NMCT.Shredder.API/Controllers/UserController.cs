using Howest.NMCT.Shredder.API.DAL;
using Howest.NMCT.Shredder.API.DAL.UnitOfWork;
using Howest.NMCT.Shredder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Howest.NMCT.Shredder.API.Controllers
{
    public class UserController : ApiController
    {
        private UnitOfWork uow = new UnitOfWork(new ShredderContext());

        [HttpGet]
        public List<User> GetUsers()
        {
            return uow.UserRepository.GetUsers();
        }

        [HttpGet]
        public User GetUserById(int id)
        {
            return uow.UserRepository.GetUserById(id);
        }

        [HttpGet]
        public User GetUserByEmail(String email)
        {
            return uow.UserRepository.GetUserByEmail(email);
        }

        [HttpPost]
        public User AddUser(User user)
        {
            user = uow.UserRepository.AddUser(user);
            uow.Save();
            return user;
        }

    }
}
