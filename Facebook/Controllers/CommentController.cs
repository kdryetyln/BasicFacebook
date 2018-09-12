using Facebook.Models.BaseEntity;
using Facebook.Models.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Facebook.SessionSettings.SessionSetting;

namespace Facebook.Controllers
{
    public class CommentController : Controller
    {

        BaseRepository<Comment> _br = new BaseRepository<Comment>();
        FacebookDbContext _db = new FacebookDbContext();

        public ActionResult AddComment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddComment(Comment comm,int id)
        {
            var user = SessionSet<User>.Get("Login");
            comm.PostID = id;
            comm.CreatedDate = DateTime.Now;
            comm.CommentUserID = user.ID;
            _br.AddModel(comm);
            var postUserID = _br.Query<Post>().Where(k => k.ID == id).FirstOrDefault().UserID;
            Notification notify = new Notification()
            {
                UserID = user.ID,
                ActionID = 2,
                PostUserID = postUserID,
                IsShow = true,
                NotifyText = user.Name + " commented on a post.",
                CreatedDate = DateTime.Now
            };
            _db.Notifications.Add(notify);
            _db.SaveChanges();
            return RedirectToAction("Home","Home");
        }

        [HttpGet]
        public ActionResult CommentDel(int id)
        {
            var comDel = _br.Query<Comment>().Where(k => k.ID == id).FirstOrDefault();
            _db.Entry(comDel).State = EntityState.Deleted;
            _db.SaveChanges();
            return RedirectToAction("Home", "Home"); 
        }
    }
}