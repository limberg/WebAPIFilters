using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIFilters.BasicAuthentication
{
    public class LoginSecurity
    {

        public static bool Login(string username, string password)
        {
            //Case when Employye Tabelle existiert
            //using(EmployeeDBEntities enitities = new EmployeeDBEntities())
            //{
            //    return entities.Users.Any(username => 
            //        user.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
            //        && user.Pasword == password)
            //    );
            //}

            return username.Equals("male", StringComparison.OrdinalIgnoreCase) && password == "male";
        }
    }
}