using Facebook.Models.BaseEntity;
using Facebook.Models.Repository;
using Facebook.SessionSettings;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Facebook.SessionSettings.SessionSetting;

namespace Facebook.Controllers
{
    public class PostController : Controller
    {
        BaseRepository<Post> _br = new BaseRepository<Post>();
        BaseRepository<Like> _brLike = new BaseRepository<Like>();
        FacebookDbContext _db = new FacebookDbContext();
        // GET: Post
        public ActionResult AddPost()
        {
            return RedirectToAction("Home", "Home");
        }

        [HttpPost]
        public ActionResult AddPost(Post post)
        {

            var userID = SessionSet<User>.Get("Login").ID;
            post.CreatedDate = DateTime.Now;
            post.UserID = userID;
            _br.AddModel(post);

            return RedirectToAction("Home", "Home");
        }

        public ActionResult GetPost()
        {
            var userID = SessionSet<User>.Get("Login").ID;
            List<PostDto> postList = new List<PostDto>();
            var FriendsID = _br.Query<RelationShip>().Where(k =>( k.User1ID == userID||k.User2ID==userID) && k.StatusID == 2).ToList();
            List<User> myFriends = new List<User>();
            foreach (var item in FriendsID)
            {
                User friends = new User(); ;
                if (item.User1ID == userID)
                {
                    friends = _br.Query<User>().Where(k => k.ID == item.User2ID).FirstOrDefault();
                }
                else
                {
                    friends = _br.Query<User>().Where(k => k.ID == item.User1ID).FirstOrDefault();
                }
                    
                myFriends.Add(friends);
            }

            foreach (var item in myFriends)
            {
                var FriendPostList = _br.Query<Post>().Where(k => k.UserID == item.ID).ToList();
                foreach (var item2 in FriendPostList)
                {
                    PostDto post = new PostDto()
                    {
                        ID = item2.ID,
                        PostText = item2.PostText,
                        CreatedDate = item2.CreatedDate,
                        UserName = item.Name,
                        UserID = item.ID

                    };
                    postList.Add(post);
                }

            }

            var UserPostList = _br.Query<Post>().Where(k => k.UserID == userID).ToList();            
            foreach (var item in UserPostList)
            {
                var user = _br.Query<User>().Where(k => k.ID == userID).FirstOrDefault();
                PostDto post = new PostDto()
                {
                    ID=item.ID,
                    PostText = item.PostText,
                    CreatedDate = item.CreatedDate,
                    UserName = user.Name,
                    UserID=userID
                    
                };
                postList.Add(post);                    
            }
            var orderList = postList.OrderByDescending(k => k.CreatedDate).ToList();

            return View(orderList);
        }

        [HttpGet]
        public ActionResult PostDel(int id)
        {
            var resultCom = _br.Query<Comment>().Where(k => k.PostID == id).Any();
            if (resultCom)
            {
                var comList = _br.Query<Comment>().Where(k => k.PostID == id).ToList();
                foreach (var item in comList)
                {
                    _db.Entry(item).State = EntityState.Deleted;
                    _db.SaveChanges();
                }
            }

            var resultLike = _brLike.Query<Like>().Where(k => k.PostID == id).Any();
            if (resultLike)
            {
                var likeList = _brLike.Query<Like>().Where(k => k.PostID == id).ToList();
                foreach (var item in likeList)
                {
                    _db.Entry(item).State = EntityState.Deleted;
                    _db.SaveChanges();
                }
            }


            var delPost = _db.Posts.Where(k => k.ID == id).FirstOrDefault();
            _db.Entry(delPost).State = EntityState.Deleted;
            _db.SaveChanges();
            return RedirectToAction("Home", "Home");
        }

        [HttpGet]
        public ActionResult PostLike(int id)
        {
            var user = SessionSet<User>.Get("Login");
            var postUserID = _br.Query<Post>().Where(k => k.ID == id).FirstOrDefault().UserID;
            if (!_brLike.Query<Like>().Where(k => k.PostID == id && k.LikeUserID == user.ID).Any())
            {
                Like addLike = new Like()
                {
                    LikeUserID = user.ID,
                    PostID = id
                };
                _brLike.AddModel(addLike);
                Notification notify = new Notification()
                {
                    UserID = user.ID,
                    ActionID = 2,
                    PostUserID = postUserID,
                    IsShow = true,
                    NotifyText = user.Name + " liked your post.",
                    CreatedDate = DateTime.Now
                };
                _db.Notifications.Add(notify);
                _db.SaveChanges();
            }
            else {
                var delLike = _brLike.Query<Like>().Where(k => k.PostID == id && k.LikeUserID == user.ID).FirstOrDefault();
                _db.Entry(delLike).State = EntityState.Deleted;
                _db.SaveChanges();
            }
            
            return RedirectToAction("Home", "Home");
        }


    }
}