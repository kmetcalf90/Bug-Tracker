using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication7.Models
{
    public class TicketAttachments
    {
        public int Id { get; set; }

        public int TicketId { get; set; }
          
        public string filepath { get; set; }

        public string description { get; set; }

        public DateTimeOffset created { get; set; }

        public string UserId { get; set; }

        public string fileurl { get; set; }

        public virtual Tickets Ticket { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}