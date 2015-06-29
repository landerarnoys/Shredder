using Howest.NMCT.Shredder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Howest.NMCT.Shredder.API.DAL.Repositories
{
    public class UserRepository
    {
        private ShredderContext context = null;

        public UserRepository(ShredderContext context)
        {
            this.context = context;
        }

        public User AddUser(User user)
        {
            var query = (from u in this.context.Users where u.Email == user.Email select u);
            if (query.SingleOrDefault() == null)
            {
                return this.context.Users.Add(user);
            }
            else
            {
                return null;
            }  
        }

        public List<User> GetUsers()
        {
            return this.context.Users.ToList<User>();
        }

        public User GetUserById(int id)
        {
            var user = (from u in this.context.Users where u.UserId == id select u);
            return user.SingleOrDefault();
        }

        public User GetUserByEmail(String email)
        {
            var user = (from u in this.context.Users where u.Email == email select u);
            return user.SingleOrDefault();
        }
    }
}