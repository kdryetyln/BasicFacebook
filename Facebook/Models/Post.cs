using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.Models.BaseEntity
{
    public class Post:BaseEntity
    {
        public User User { get; set; }
        public int UserID { get; set; }
        public string PostText { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Share> Shares { get; set; }
    }
}