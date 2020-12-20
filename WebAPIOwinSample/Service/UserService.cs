using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPIOwinSample.Models;

namespace WebAPIOwinSample.Service
{
    public class UserService
    {
        public User GetUserByCredentials(string email, string paswword)
        {
            //Simulation um DB Credentials zu erreichen
            string emailDB = "email@domain.com";
            string passwordDB = "password";

            if (email != emailDB || paswword != passwordDB)
                return null;

            User user = new User() { Id = 1, Name = "Julio Limberg Rivera Ricaldes", Password = "password", Email = "limberg.rivera@gmail.com" };

            if (user != null)
                user.Password = string.Empty;

            return user;
        }
    }
}