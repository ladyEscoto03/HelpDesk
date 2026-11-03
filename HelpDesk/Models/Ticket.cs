using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Título")]
        public string Title { get; set; }

        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Display(Name = "Fecha de registro")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Fecha de actualización")]
        public DateTime ?UpdatedDate { get; set; }

        [Display(Name = "Categoría")]
        public string Category { get; set; }


        public int TicketStatusId { get; set; }

        [ForeignKey("TicketStatusId")]
        public TicketStatus TicketStatus { get; set; }

        public string RequesterId { get; set; }

        [ForeignKey("RequesterId")]
        public ApplicationUser Requester { get; set; }

        public string AssignedUserId { get; set; }

        [ForeignKey("AssignedUserId")]
        public ApplicationUser AssignedUser{ get; set; }

        public ICollection<Comment> Comments { get; set; }


    }
}