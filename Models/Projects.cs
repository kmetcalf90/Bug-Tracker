using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication7.Models
{
    public class Projects
    {
        public Projects()
        {
            this.User = new HashSet<ApplicationUser>();
            this.Tickets = new HashSet<Tickets>();
        }
        public int Id { get; set; }

        public string name { get; set; }
        public string AssignedtoUserId { get; set; }

        //public DateTimeOffset? Begun { get; set; }
        //public DateTimeOffset? Discontinued { get; set; }
        public virtual ICollection<ApplicationUser> User { get; set; }
        public virtual ICollection<Tickets> Tickets { get; set; }
        public virtual ApplicationUser AssignedtoUser { get; set; }
    }
}