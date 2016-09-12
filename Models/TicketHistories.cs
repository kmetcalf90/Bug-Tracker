using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication7.Models
{
    public class TicketHistories
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string property { get; set; }
        public string propertydisplay { get; set; }
        public string oldvalue { get; set; }
        public string oldvaluedisplay { get; set; }
        public string newvalue { get; set; }
        public string newvaluedisplay { get; set; }

        public DateTimeOffset changed { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Tickets Ticket { get; set; }
    }
}