using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.Models.BaseEntity
{
    public class PostDto:BaseEntity
    {
        public string PostText { get; set; }
        public string UserName { get; set; }
        public int UserID { get; set; }
    }
}