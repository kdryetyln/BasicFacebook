using Facebook.Models.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Facebook.Controllers
{
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult ChatIndex()
        {
            return View();
        }
        public ActionResult Chat()
        {

            return View();
        }
        public JsonResult VerifyUserNameInUse(string userName)
        {

            try
            {
                using (var db = new FacebookDbContext())
                {
                    return Json(new { Success = true, InUse = db.Connections.Where(x => x.UserName.ToLower() == userName.ToLower() && x.IsOnline).SingleOrDefault() != null }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { Success = false, ErrorMessage = "Something wrong happened." }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}