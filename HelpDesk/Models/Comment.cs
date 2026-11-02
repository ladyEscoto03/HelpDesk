using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Description { get; set; }
        
        public DateTime CreatedDate { get; set; }

        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}