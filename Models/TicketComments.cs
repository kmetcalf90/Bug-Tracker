﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication7.Models
{
    public class TicketComments
    {
        public int Id { get; set; }

        public string comment { get; set; }

        public DateTimeOffset created { get; set; }

        public int TicketId { get; set; }

        public string UserId { get; set; }

        public virtual Tickets Ticket { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}