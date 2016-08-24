using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class RolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper helper = new UserRolesHelper();

        // GET: Roles
        public ActionResult Index()
        {
            ViewBag.User = db.Users.ToList();
            return View();
        }


        [HttpPost]
        public ActionResult Index(string uid, string role)
        {
            helper.AddUserToRole(uid, role);
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult UserRoles()
        {
            var users = db.Users.ToList().OrderBy(u => u.LastName);
            return View(users.ToList());
        }
       
    }
}