using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HelpDesk.Models;

namespace HelpDesk.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Administrador")]
        // GET: Report
        public ActionResult Index()
        {

            var model = new ReportViewModel();

          
            model.OpenTickets = db.Tickets.Count(t => t.TicketStatusId == 1);

            model.AssignedTickets = db.Tickets.Count(t => t.TicketStatusId == 2);

            model.InProgressTickets = db.Tickets.Count(t => t.TicketStatusId == 3);

            model.PendingTickets = db.Tickets.Count(t => t.TicketStatusId == 4);

            model.ResolvedTickets = db.Tickets.Count(t => t.TicketStatusId == 5);

            model.ClosedTickets = db.Tickets.Count(t => t.TicketStatusId == 6);

            model.CancelledTickets = db.Tickets.Count(t => t.TicketStatusId == 7);

            // Total de tickets
            model.TotalTickets = db.Tickets.Count();

            // Tiempo promedio de resolución
            var resolvedTickets = db.Tickets
                .Where(t => t.TicketStatusId == 5 && t.UpdatedDate != null)
                .ToList();

            if (resolvedTickets.Any())
            {
                model.AverageResolutionHours = resolvedTickets
                    .Average(t => (t.UpdatedDate.Value - t.CreatedDate).TotalHours);
            }
            else
            {
                model.AverageResolutionHours = 0;
            }


            return View(model);
        }




        /*
        // GET: Report/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Report/Create
        public ActionResult Create()
        {
            ViewBag.AssignedUserId = new SelectList(db.ApplicationUsers, "Id", "Name");
            ViewBag.RequesterId = new SelectList(db.ApplicationUsers, "Id", "Name");
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "Id", "Name");
            return View();
        }

        // POST: Report/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,CreatedDate,UpdatedDate,Category,TicketStatusId,RequesterId,AssignedUserId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssignedUserId = new SelectList(db.ApplicationUsers, "Id", "Name", ticket.AssignedUserId);
            ViewBag.RequesterId = new SelectList(db.ApplicationUsers, "Id", "Name", ticket.RequesterId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "Id", "Name", ticket.TicketStatusId);
            return View(ticket);
        }

        // GET: Report/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssignedUserId = new SelectList(db.ApplicationUsers, "Id", "Name", ticket.AssignedUserId);
            ViewBag.RequesterId = new SelectList(db.ApplicationUsers, "Id", "Name", ticket.RequesterId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "Id", "Name", ticket.TicketStatusId);
            return View(ticket);
        }

        // POST: Report/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,CreatedDate,UpdatedDate,Category,TicketStatusId,RequesterId,AssignedUserId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssignedUserId = new SelectList(db.ApplicationUsers, "Id", "Name", ticket.AssignedUserId);
            ViewBag.RequesterId = new SelectList(db.ApplicationUsers, "Id", "Name", ticket.RequesterId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "Id", "Name", ticket.TicketStatusId);
            return View(ticket);
        }

        // GET: Report/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Report/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        */
    }
        
}
