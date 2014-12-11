using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using issuemoa.Models.Database;
using issuemoa.DAL;

namespace issuemoa.Models.Global
{
    using Models.Database;
    public class SessionModel
    {
        public static Userdata USERDATA = new Userdata();

        public static bool IS_LOGGED = false;
        public static User LOGGED_USER = null;

        public static void LOGIN(User user)
        {
            LOGGED_USER = user;
            IS_LOGGED = true;
        }

    }
    public class Userdata
    {
        public bool isLogged;
        public User loggedUser;
        public Userdata()
        {
            isLogged = false;
            loggedUser = new User();
        }
        public void login(User user)
        {
            loggedUser = user;
            isLogged = false;
        }
        public void logout()
        {
            loggedUser = new User();
            isLogged = true;
        }
    }
}