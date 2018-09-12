using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.Models.BaseEntity
{
    public class Comment:BaseEntity
    {
        public User CommentUser { get; set; }
        public int CommentUserID { get; set; }
        public Post Post { get; set; }
        public int PostID { get; set; }
        public string CommentText { get; set; }
        public ICollection<Comment> CommentList { get; set; }
        public ICollection<Like> CommentLikes { get; set; }
    }
}