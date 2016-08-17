﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication7.Models
{
    public class Projects
    {
        public Projects()
        {
            this.Users = new HashSet<ApplicationUser>();
            this.Tickets = new HashSet<Tickets>();
        }
        public int Id { get; set; }

        public string name { get; set; }

        //public DateTimeOffset? Begun { get; set; }
        //public DateTimeOffset? Discontinued { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<Tickets> Tickets { get; set; }

    }
}