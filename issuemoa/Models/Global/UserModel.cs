using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using issuemoa.Models.Database;
using issuemoa.DAL;

namespace issuemoa.Models.Global
{
    using Models.Database;
    public class UserModel
    {
        public List<User> users { get; set; }

        public UserModel()
        {
            using (var db = new IssueMoaDB())
            {
                users = db.Users.ToList();
            }   
        }

        public string getPasswordHash(string password)
        {
            return PasswordHash.CreateHash(password);
        }

        /*
         * check if password is right for the user
         */
        public bool isPasswordMatching(User user, string password)
        {            
            if (user == null)
                return false;

            return PasswordHash.ValidatePassword(password, user.Password);
        }
        public User getUserbyUsername(string username)
        {            
            foreach (User user in users)
            {
                if (user.UserName == username)
                    return user;
            }
            return null;
        }
        public bool saveUserToDB(User user)
        {
            try
            {
                using (var db = new IssueMoaDB())
                {
                    db.Users.Add(user);
                    UserRole ThisUserRole = new UserRole();
                    ThisUserRole.UserId = user.UserId;
                    ThisUserRole.RoleId = 3;
                    db.UserRoles.Add(ThisUserRole);
                    db.PointHistories.Add(new PointHistory { ChangeAmount = 1000, PointTypeId = 6, UserId = user.UserId });
                    db.SaveChanges();
                }
                return true;
                
            }
            catch(System.Data.SqlClient.SqlException exp)
            {
                throw new InvalidOperationException("Data could not be read", exp);
            }
        }
        public void userToSession(User user)
        {
            HttpContext.Current.Session["UserId"] = user.UserId;
            HttpContext.Current.Session["Name"] = user.Name;
            HttpContext.Current.Session["UserName"] = user.UserName;
            HttpContext.Current.Session["PointsGained"] = user.PointsGained;
            HttpContext.Current.Session["PointsDonated"] = user.PointsDonated;
            using (var db = new IssueMoaDB())
            {
                var Roles = (from u in db.UserRoles
                             where u.UserId == user.UserId
                             select u.Role.Description).ToList();
                HttpContext.Current.Session["Roles"] = Roles;
            }
        }

        public static bool IsInRole(string role)
        {
            if ((List<string>)HttpContext.Current.Session["Roles"] == null)
                return false;
            List<string> Roles = (List<string>)HttpContext.Current.Session["Roles"];
            foreach(string Role in Roles)
            {
                if (Role == role)
                {
                    return true;
                }
            }
            return false;
        }


    }
}