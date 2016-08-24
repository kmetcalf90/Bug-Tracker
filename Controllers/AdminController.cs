using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            var users = db.Users.ToList();
            return View(users);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult UserRolesList(AdminUserViewModel model, string id)
        {
            //var user = User.Identity.GetUserId();
            UserRolesHelper helper = new UserRolesHelper();
            //var currentRoles = helper.ListUserRoles(id);
            if (model.SelectedRoles == null)
            {
                model.SelectedRoles = new string[] { "" };
            }
            foreach (var role in db.Roles.Select(r => r.Name))
            {
                if (model.SelectedRoles.Contains(role))
                {
                    helper.AddUserToRole(id, role);
                }
                else
                {
                    helper.RemoveUserFromrole(id, role);
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult UserRolesList(string id)
        {
            var user = db.Users.Find(id);

            AdminUserViewModel AdminModel = new AdminUserViewModel();
            UserRolesHelper helper = new UserRolesHelper();
            var selected = helper.ListUserRoles(id);
            AdminModel.Roles = new MultiSelectList(db.Roles, "Name", "Name", selected);
            AdminModel.User = user;
            return View(AdminModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult EditUserRoles(AdminUserViewModel model, string id)
        {
            //var user = User.Identity.GetUserId();
            UserRolesHelper helper = new UserRolesHelper();
            //var currentRoles = helper.ListUserRoles(id);
            if (model.SelectedRoles == null)
            {
                model.SelectedRoles = new string[] { "" };
            }
            foreach (var role in db.Roles.Select(r => r.Name))
            {
                if (model.SelectedRoles.Contains(role))
                {
                    helper.AddUserToRole(id, role);
                }
                else
                {
                    helper.RemoveUserFromrole(id, role);
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult EditUserRoles(string id)
        {
            var user = db.Users.Find(id);

            AdminUserViewModel AdminModel = new AdminUserViewModel();
            UserRolesHelper helper = new UserRolesHelper();
            var selected = helper.ListUserRoles(id);
            AdminModel.Roles = new MultiSelectList(db.Roles, "Name", "Name", selected);
            AdminModel.User = user;
            return View(AdminModel);
        }
    }
}