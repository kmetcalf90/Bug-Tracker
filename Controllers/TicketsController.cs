using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    [Authorize(Roles = "Admin, Submitter, Project Manager, Developer")]
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tickets
        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.Project).Include(t => t.TicketPriorities).Include(t => t.TicketStatus).Include(t => t.TicketTypes);
            return View(tickets.ToList());
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tickets tickets = db.Tickets.Find(id);
            if (tickets == null)
            {
                return HttpNotFound();
            }
            return View(tickets);
        }



        // GET: Tickets/Create
        [Authorize(Roles = "Submitter")]

        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "name");
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "name");
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "name");
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "name");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Submitter")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Title,Description,Created,Updated,ProjectId,TicketTypeId,TicketPriorityId,TicketStatusId,OwnerUserId,AssignedtoUserId")] Tickets tickets)
        {
            if (ModelState.IsValid)
            {
                tickets.Created = DateTime.Now;
                tickets.OwnerUserId = User.Identity.GetUserId();
                tickets.AssignedtoUserId = User.Identity.GetUserId();
                db.Tickets.Add(tickets);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "name", tickets.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "name", tickets.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "name", tickets.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "name", tickets.TicketTypeId);
            ViewBag.AssignedtoUserId = new SelectList(db.Users, "Id", "name", tickets.AssignedtoUserId);
            return View(tickets);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tickets tickets = db.Tickets.Find(id);
            if (tickets == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "name", tickets.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "name", tickets.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "name", tickets.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "name", tickets.TicketTypeId);
            ViewBag.AssignedtoUserId = new SelectList(db.Users, "Id", "DisplayName", tickets.AssignedtoUserId);
            return View(tickets);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Title,Description,Created,Updated,ProjectId,TicketTypeId,TicketPriorityId,TicketStatusId,OwnerUserId,AssignedtoUserId")] Tickets tickets)
        {
            var editable = new List<string>() { "Title", "Description" };
            if (User.IsInRole("Admin"))
                editable.AddRange(new string[] { "AssignedToUserId", "TicketTypeId", "TicketPriorityId", "TicketStatusId", "Updated" });
            if (User.IsInRole("Project Manager"))
                editable.AddRange(new string[] { "AssignedToUserId", "TicketTypeId", "TicketPriorityId", "TicketStatusId", "Updated" });

            if (ModelState.IsValid)
            {
                tickets.Updated = DateTimeOffset.Now;
                db.Entry(tickets).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "name", tickets.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "name", tickets.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "name", tickets.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "name", tickets.TicketTypeId);
            ViewBag.AssignedtoUser = new SelectList(db.Users, "Id", "name", tickets.AssignedtoUser);
            return View(tickets);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tickets tickets = db.Tickets.Find(id);
            if (tickets == null)
            {
                return HttpNotFound();
            }
            return View(tickets);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tickets tickets = db.Tickets.Find(id);
            db.Tickets.Remove(tickets);
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

        public ActionResult AssignedTickets()
        {
            if (User.Identity.IsAuthenticated)
            {
                IEnumerable<Tickets> tickets;
                var user = db.Users.Find(User.Identity.GetUserId());

                tickets = db.Tickets.Where(t => t.AssignedtoUserId == user.Id);

                return View(tickets.ToList());
            }

            return View();
        }
        //GET : Tickets/UnassignedTickets
        public ActionResult UnassignedTickets()
        {
            if (User.Identity.IsAuthenticated)
            {
                IEnumerable<Tickets> tickets;
                tickets = db.Tickets.Where(t => t.AssignedtoUserId == null);

                return View(tickets.ToList());
            }

            return View();
        }

        //GET: Tickets/Notifications
        public ActionResult Notifications()
        {
            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            if (User.Identity.IsAuthenticated)
            {
                IEnumerable<TicketNotifications> notifications;
                notifications = db.TicketNotifications.Where(n => n.UserId == user.Id);

                return View(notifications.ToList());
            }
            return View();
      
        }
    }
}
