using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.Models.BaseEntity
{
    public class Share:BaseEntity
    {
        public User ShUser { get; set; }
        public int ShUserID { get; set; }
        public Post ShPost { get; set; }
        public int ShPostID { get; set; }
        public int NewPostID { get; set; }
    }
}