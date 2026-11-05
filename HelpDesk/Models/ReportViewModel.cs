using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpDesk.Models
{
    public class ReportViewModel
    {
        public int OpenTickets { get; set; }
        public int AssignedTickets { get; set; }
        public int InProgressTickets { get; set; }
        public int PendingTickets { get; set; }
        public int ResolvedTickets { get; set; }
        public int ClosedTickets { get; set; }
        public int CancelledTickets { get; set; }

        public int TotalTickets { get; set; }

        public double AverageResolutionHours { get; set; }
    }
}