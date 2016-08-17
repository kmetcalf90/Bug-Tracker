using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication7.Models
{
    public class Tickets
    { 
        public Tickets()
        {
            TicketAttachments = new HashSet<TicketAttachments>();
            TicketComments = new HashSet<TicketComments>();
            TicketHistories = new HashSet<TicketHistories>();
            TicketNotifications = new HashSet<TicketNotifications>();
        }
     
        public int id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset? Updated { get; set; }

        public int ProjectId { get; set; }
        public int TicketTypeId { get; set; }

        public int TicketPriorityId { get; set; }

        public int TicketStatusId { get; set; }

        public string OwnerUserId { get; set; }
  
        public string AssignedtoUserId { get; set;}

        public virtual Projects Project { get; set; }

        public virtual TicketType TicketTypes { get; set; }

        public virtual TicketPriority TicketPriorities { get; set; }

        public virtual TicketStatus TicketStatus { get; set; }

        public virtual ICollection<TicketAttachments> TicketAttachments { get; set; }
        public virtual ICollection<TicketComments> TicketComments { get; set; }
        public virtual ICollection<TicketHistories> TicketHistories { get; set; }
        public virtual ICollection<TicketNotifications> TicketNotifications { get; set; }
    }
}