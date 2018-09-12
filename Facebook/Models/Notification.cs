using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.Models.BaseEntity
{
    public class Notification:BaseEntity
    {
        public int? PostID { get; set; }
        public int UserID { get; set; }
        public int PostUserID { get; set; }
        public int ActionID { get; set; }//1->ekleme 2->like 3->yorum 4-> share
        public string NotifyText { get; set; }//şu arkadaşın şunu yaptı.
        public bool IsShow { get; set; }
    }
}