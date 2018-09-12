using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Facebook.Controllers
{
    public class NotificationController : Controller
    {
        // GET: Notification
        public ActionResult GetNotify()
        {
            return View();
        }
    }
}