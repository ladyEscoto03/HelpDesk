using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HelpDesk.Models;
using Microsoft.AspNet.Identity;

namespace HelpDesk.Controllers
{
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tickets
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var tickets = db.Tickets.Include(t => t.AssignedUser).Include(t => t.Requester).Include(t => t.TicketStatus).Where(t=>t.RequesterId==userId);
            return View(tickets.ToList());
        }

        // GET: Tickets/Details/5
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

        // GET: Tickets/Create
        public ActionResult Create()
        {
            ViewBag.AssignedUserId = new SelectList(db.Users, "Id", "Name");
            ViewBag.RequesterId = new SelectList(db.Users, "Id", "Name");
            ViewBag.TicketStatusId = new SelectList(db.Users, "Id", "Name");
            return PartialView();
        }

        // POST: Tickets/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create([Bind(Include = "Id,Title,Description,Category")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.CreatedDate = DateTime.Now;
                ticket.TicketStatusId = 1;
                ticket.RequesterId= User.Identity.GetUserId();
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return Json(new { success = true });

            }

            ViewBag.AssignedUserId = new SelectList(db.Users, "Id", "Name", ticket.AssignedUserId);
            ViewBag.RequesterId = new SelectList(db.Users, "Id", "Name", ticket.RequesterId);
            ViewBag.TicketStatusId = new SelectList(db.Users, "Id", "Name", ticket.TicketStatusId);
            return Json(new { success = false });
        }

        // GET: Tickets/Edit/5
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
            ViewBag.AssignedUserId = new SelectList(db.Users, "Id", "Name", ticket.AssignedUserId);
            ViewBag.RequesterId = new SelectList(db.Users, "Id", "Name", ticket.RequesterId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);



            ViewBag.Comments = db.Comments.Where(c => c.TicketId == id).ToList();


            return PartialView(ticket);
        }

        // POST: Tickets/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit( Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var ticketDb = db.Tickets.Find(ticket.Id);
                ticket.UpdatedDate = DateTime.Now;

                ticketDb.AssignedUserId = ticket.AssignedUserId;
                ticketDb.TicketStatusId = ticket.TicketStatusId;
                ticketDb.UpdatedDate = DateTime.Now;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssignedUserId = new SelectList(db.Users, "Id", "Name", ticket.AssignedUserId);
            ViewBag.RequesterId = new SelectList(db.Users, "Id", "Name", ticket.RequesterId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
            return View(ticket);
        }


        [HttpGet]
        public ActionResult Search(int? id, string text, string status)
        {
            var tickets = db.Tickets
                .Include(t => t.Requester)
                .Include(t => t.TicketStatus);

            if (id.HasValue)
            {
                tickets = tickets.Where(t => t.Id == id.Value);
            }

            if (!string.IsNullOrEmpty(text))
            {
                tickets = tickets.Where(t =>
                    t.Title.Contains(text) ||
                    t.Description.Contains(text) ||
                    t.Requester.Name.Contains(text));
            }

            if (!string.IsNullOrEmpty(status))
            {
                tickets = tickets.Where(t =>
                    t.TicketStatus.Name == status);
            }

            return PartialView("_TablaTickets", tickets.ToList());
        }

        [HttpGet]
        public ActionResult RefreshTickets()
        {
            var userId = User.Identity.GetUserId();
            var tickets = db.Tickets.Include(t => t.AssignedUser).Include(t => t.Requester).Include(t => t.TicketStatus).Where(t => t.RequesterId == userId);

            return PartialView("_TablaTickets", tickets);
        }




        // GET: Tickets/Delete/5
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

        // POST: Tickets/Delete/5
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
    }
}
