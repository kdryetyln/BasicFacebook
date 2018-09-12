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
    public class RelationShipController : Controller
    {
        BaseRepository<RelationShip> _br = new BaseRepository<RelationShip>();
        FacebookDbContext _db = new FacebookDbContext();
        // GET: RelationShip
        [HttpGet]
        public ActionResult AddFriend(int id)
        {
            var user = SessionSet<User>.Get("Login");
            RelationShip rs = new RelationShip()
            {
                User1ID = user.ID,
                User2ID = id,
                CreatedDate = DateTime.Now,
                ActionUserID = user.ID,
                StatusID = 1
            };
            _br.AddModel(rs);
            Notification notify = new Notification()
            {
                UserID = user.ID,
                ActionID = 1,
                PostUserID = id,
                IsShow = true,
                NotifyText = user.Name + " wants to add you as a friend.",
                CreatedDate = DateTime.Now                
            };
            _db.Notifications.Add(notify);
            _db.SaveChanges();

            return RedirectToAction("Home", "Home");
        }

        public ActionResult GetRequest()
        {
            var userID = SessionSet<User>.Get("Login").ID;
            var result = _br.Query<RelationShip>().Where(k => k.User2ID == userID && k.StatusID == 1).Any();
            List<User> requestUser = new List<User>();
            if (result)
            {
                var requests = _br.Query<RelationShip>().Where(k => k.User2ID == userID && k.StatusID == 1).ToList();

                foreach (var item in requests)
                {
                    var rID = item.User1ID;
                    var user = _br.Query<User>().Where(k => k.ID == rID).FirstOrDefault();
                    requestUser.Add(user);
                }
            }
            return View(requestUser);
        }

        [HttpGet]
        public ActionResult Confirm(int id)
        {
            var user = SessionSet<User>.Get("Login");
            var confirm = _br.Query<RelationShip>().Where(k => k.User1ID == id && k.User2ID == user.ID && k.StatusID == 1).FirstOrDefault();
            confirm.StatusID = 2;
            confirm.CreatedDate = DateTime.Now;
            confirm.ActionUserID = user.ID;
            _br.Update(confirm);
            Notification notify = new Notification()
            {
                UserID = user.ID,
                ActionID = 4,
                PostUserID = id,
                IsShow = true,
                NotifyText = user.Name + " accepted your friendship request.",
                CreatedDate = DateTime.Now
            };
            _db.Notifications.Add(notify);
            _db.SaveChanges();
            return RedirectToAction("Home", "Home");
        }
        public ActionResult Delete(int id)
        {
            var userID = SessionSet<User>.Get("Login").ID;
            var confirm = _br.Query<RelationShip>().Where(k => k.User1ID == id && k.User2ID == userID && k.StatusID == 1).FirstOrDefault();
            _db.Entry(confirm).State = EntityState.Deleted;
            _db.SaveChanges();
            return RedirectToAction("Home", "Home");
        }

        public ActionResult GetFriendns()
        {
            var user = SessionSet<User>.Get("Login");
            var usersID = _br.Query<RelationShip>().Where(k => (k.User1ID == user.ID ) && k.StatusID == 2).ToList();
            var users2ID = _br.Query<RelationShip>().Where(k => (k.User2ID == user.ID ) && k.StatusID == 2).ToList();
            List<User> friendList = new List<User>();
            foreach (var item in usersID)
            {
                var users = _br.Query<User>().Where(k => k.ID == item.User2ID).FirstOrDefault();
                friendList.Add(users);
            }
            foreach (var item in users2ID)
            {
                var users = _br.Query<User>().Where(k => k.ID == item.User1ID).FirstOrDefault();
                friendList.Add(users);
            }
            return View(friendList);
        }

        public ActionResult RemoveFriend(int id)
        {
            var userID = SessionSet<User>.Get("Login").ID;
            var remove=_br.Query<RelationShip>().Where(k =>( k.User1ID == id && k.User2ID == userID) || (k.User1ID == userID && k.User2ID == id) && k.StatusID == 2).FirstOrDefault();
            _db.Entry(remove).State = EntityState.Deleted;
            _db.SaveChanges();
            return RedirectToAction("GetFriendns");
        }

    }
}