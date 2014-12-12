using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Globalization;
using issuemoa.Models.Global;
using issuemoa.Models.Database;
using issuemoa.DAL;
using System.Data.Entity.Core.Objects;

namespace issuemoa.Controllers
{

    public class SignController : Controller
    {
        private UserModel userModel = new UserModel();
        
        [HttpPost]
        public ActionResult register(string postAction, FormCollection postedValues)
        {
            User user = new User();
            user.Name = Convert.ToString(postedValues["firstName"]);
            user.UserName = Convert.ToString(postedValues["username"]);
	        user.Password = PasswordHash.CreateHash(Convert.ToString(postedValues["password"]));
            user.Email = Convert.ToString(postedValues["email"]);
            user.PointsGained = 1000;
            userModel.saveUserToDB(user);
            userToSession(new UserModel().users.Last());
            return RedirectToAction("Index", "Home");
        }
        private void userToSession(User user)
        {
            Session["UserId"] = user.UserId;
            Session["Name"] = user.Name;
            Session["UserName"] = user.UserName;
            Session["PointsGained"] = user.PointsGained;
            Session["PointsDonated"] = user.PointsDonated;
            using(var db = new IssueMoaDB())
            {
                var Roles = (from u in db.UserRoles
                             where u.UserId == user.UserId
                             select u.Role.Description).ToList();
                Session["Roles"] = Roles;
                DateTime startDate = DateTime.Today.Date;
                DateTime endDate = startDate.AddDays(1);
                PointHistory TodayLoginPoint = (from p in db.PointHistories
                                                where p.UserId == user.UserId && (p.ChangeDate >= startDate && p.ChangeDate < endDate) && (p.PointTypeId == 1 || p.PointTypeId == 6)
                                                select p).FirstOrDefault();
                if(TodayLoginPoint == null)
                {
                    db.PointHistories.Add(new PointHistory { ChangeAmount = 500, PointTypeId = 1, UserId = user.UserId });
                    User ThisUser = (from u in db.Users
                                     where u.UserId == user.UserId
                                     select u).First();
                    ThisUser.PointsGained += 500;
                    Session["PointsGained"] = ThisUser.PointsGained;
                    db.SaveChanges();
                }
            }
        }

        [HttpPost]
        public string tryLogin(string postAction, FormCollection postedValues)
        {
	        string msg = "The username or password you entered is incorrect.";
	        string unavailableColor = "#B94141";

	        string username = Convert.ToString(postedValues["username_login"]);
	        string password = Convert.ToString(postedValues["password_login"]);

            User user = userModel.getUserbyUsername(username);
	        if(!userModel.isPasswordMatching(user, password))
            {
		        return "<span style=\"color:" + unavailableColor +"; padding-left:2px;font-family: -webkit-pictograph;font-size: 14px;\">"+ msg +"</span>";
	        }
	        else
            {
                userToSession(user);
                Session["user"] = user.ToString();
                return "true";
	        }
        }

        [HttpPost]
        public string checkUsernameNEmail(string postAction, FormCollection postedValues)
        {
         
            string msg = " is unavailable";
            string availableColor = "#41B941;";
            string unavailableColor = "#B94141";
            string data;
            if(Convert.ToString(postedValues["check"]) == "username")
            {
                string username = Convert.ToString(postedValues["username"]);
                if(Regex.IsMatch(username, "/[^0-9a-z]/"))
                {
                    msg = "Only lowercase letters or numbers";
                    data = "<span style=\"color:" + unavailableColor +"; padding-left:6px;\">" + msg + "</span>";
                    data += "<script>uservalid = false;</script>";
                }
                else if(username.Length<5){
                    msg = "username must be at least 5 long";
                    data = "<span style=\"color:"+ unavailableColor +"; padding-left:6px;\">"+msg+"</span>";
                    data += "<script>uservalid = false;</script>";
                }
                else if(userDuplicated(username)){
                    msg = " is already used";	
                    data = "<span style=\"color:"+ unavailableColor +"; padding-left:6px;\">"+username+msg+"</span>";
                    data += "<script>uservalid = false;</script>";
                }
                else {
                    msg = " is available";
                    data = "<span style=\"color:"+ availableColor +"; padding-left:6px;\">"+username+msg+"</span>";
                    data += "<script>uservalid = true;</script>";
                }	
            } //end username checking case
            
            else
            {
                string email = Convert.ToString(postedValues["email"]);
                RegexUtilities r = new RegexUtilities();
                if (!r.IsValidEmail(email))
                {
                    msg = " is not valid email form";
                    data = "<span style=\"color:" + unavailableColor + "; padding-left:6px;\">" + email + msg + "</span>";
                    data += "<script>emailvalid = false;</script>";
                }
                else if(userDuplicated(email)){
                    msg = " is already used";	
                    data = "<span style=\"color:"+ unavailableColor +"; padding-left:6px;\">"+email+msg+"</span>";
                    data += "<script>emailvalid = false;</script>";
                }
                else {
                    msg = "Available";
                    data = "<span style=\"color:"+ availableColor +"; padding-left:6px;\">"+msg+"</span>";
                    data += "<script>emailvalid = true;</script>";
                }	
            } // end email checking case

            return data;
        }

        private bool userDuplicated(string usernameOrEmail){

        foreach (User user in userModel.users)
        {
			if(usernameOrEmail == user.UserName || usernameOrEmail == user.Email)
				return true;	
		}

		return false;
	    }
        

    }//class

    public class RegexUtilities
    {
        bool invalid = false;

        public bool IsValidEmail(string strIn)
        {
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names. 
            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format. 
            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }
    }//class
}//namespace