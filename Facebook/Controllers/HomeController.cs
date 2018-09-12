using Facebook.Models.BaseEntity;
using Facebook.Models.Repository;
using Facebook.SessionSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static Facebook.SessionSettings.SessionSetting;

namespace Facebook.Controllers
{
    public class HomeController : Controller
    {
        BaseRepository<Post> _br = new BaseRepository<Post>();
        public ActionResult Start()
        {
            return View();
        }
        public ActionResult Home()
        {
            var userID=SessionSet<User>.Get("Login").ID;

            if (userID != null)
                return View();
            else
                return RedirectToAction("Start", "Home");

        }
        [HttpPost]
        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();

            Session.Clear();
            if (Session["ID"] == null && Session["Name"] == null)
            {
                return RedirectToAction("Start", "Home");
            }
            else
                return View();
        }
        public ActionResult MyProfile()
        {
            var user = SessionSet<User>.Get("Login");
            var postIDList = _br.Query<Post>().Where(k => k.UserID == user.ID).ToList();
            List<PostDto> postList = new List<PostDto>();
            foreach (var item in postIDList)
            {
                PostDto dto = new PostDto()
                {
                    ID=item.ID,
                    PostText=item.PostText,
                    UserID=user.ID,
                    CreatedDate=item.CreatedDate,
                    UserName=user.Name
                };
                postList.Add(dto);
            }

            return View(postList.OrderByDescending(k=>k.CreatedDate).ToList());
        }

    }
}