using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{
    public class TicketStatus
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Estado")]
        public string Name { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}