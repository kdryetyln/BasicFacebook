using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.Models.BaseEntity
{
    public class RelationShip:BaseEntity
    {
        public User User1 { get; set; }// for user
        public int User1ID { get; set; }
        public User User2 { get; set; }// for friend
        public int User2ID { get; set; }
        public Status Status { get; set; }
        public int StatusID { get; set; }
        public int ActionUserID { get; set; }//who sent the request?
    }
}