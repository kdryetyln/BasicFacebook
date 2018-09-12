using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.Models.BaseEntity
{
    public class Connection:BaseEntity
    {
        public string ConnectionId { get; set; }
        public string UserName { get; set; }
        public bool IsOnline { get; set; }
    }
}