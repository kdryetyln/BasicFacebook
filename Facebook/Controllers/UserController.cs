using Facebook.Models.BaseEntity;
using Facebook.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Facebook.SessionSettings.SessionSetting;

namespace Facebook.Controllers
{
    public class UserController : Controller
    {
        BaseRepository<User> _br = new BaseRepository<User>();
        FacebookDbContext _db =new FacebookDbContext();
        // GET: User
        public ActionResult Login()
        {
            return RedirectToAction("Start", "Home");
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var email = user.Email;
            var pass = user.Password;
            var result = _br.Query<User>().Where(k => (k.Email.Equals(email)) && (k.Password.Equals(pass))).Any();
            if (result)
            {
                var myuser = _br.Query<User>().Where(k => (k.Email.Equals(email)) && (k.Password.Equals(pass))).FirstOrDefault();
                User _temp = new User()
                {
                    ID = myuser.ID,
                    Name = myuser.Name,
                    Email = myuser.Email,
                    Password = myuser.Password,
                    CreatedDate = myuser.CreatedDate
                };
                SessionSet<User>.Set(_temp, "Login");
                return RedirectToAction("Home", "Home");
            }
            else
            {
                string msg = "<script language=\"javascript\">";
                msg += "alert('" + "The username and password you entered did not match our records. Please double-check and try again." + "');";
                msg += "</script>";
                Response.Write(msg);
                return RedirectToAction("Start", "Home");
            }            
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User user)
        {
            var email = user.Email;
            var result = _br.Query<User>().Where(k => k.Email.Equals(email)).Any();
            if (!result)
            {
                user.CreatedDate = DateTime.Now;
                _br.AddModel(user);
            }
            return RedirectToAction("Start", "Home");
        }
        public ActionResult GetPeople()
        {
            var userID = SessionSet<User>.Get("Login").ID;
            var allUser = _br.Query<User>().Where(k => k.ID != userID).ToList();
            List<User> otherUser = new List<User>();
            foreach (var item in allUser)
            {
                var result= _br.Query<RelationShip>().Where(k =>(k.User1ID==userID&&k.User2ID==item.ID)||(k.User1ID==item.ID&&k.User2ID==userID)).Any();
                if (!result)
                {
                    otherUser.Add(item);
                }
            }
            return View(otherUser);
        }
    }
}