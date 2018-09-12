using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.Models.BaseEntity
{
    public class User:BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<RelationShip> RelationShips { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}