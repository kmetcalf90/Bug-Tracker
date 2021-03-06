﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication7.Models
{
    public class TicketNotifications
    {
        public int Id { get; set; }

        public int TicketId { get; set; }

        public string UserId { get; set; }

        public string Message { get; set; }

        public DateTimeOffset Created { get; set; }

        public virtual Tickets Ticket { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}