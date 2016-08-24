using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication7.Models;

namespace WebApplication7.Models
{
    public class UserRolesHelper
    {

        private ApplicationDbContext db;
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public UserRolesHelper()
        {
         db = new ApplicationDbContext();
            userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(db));
            roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(db));
            

        }
        public bool IsUserInRole(string userId, string roleName)
        {
            return userManager.IsInRole(userId, roleName);
        }
        public IList<string> ListUserRoles(string userid)
        {
            return userManager.GetRoles(userid);
        }

        public bool AddUserToRole(string userId, string roleName)
        {
            var result = userManager.AddToRole(userId, roleName);
            return result.Succeeded;
        }

        public bool RemoveUserFromrole(string userid, string roleName)
        {
            var result = userManager.RemoveFromRole(userid, roleName);
            return result.Succeeded;


        }

        public IList<ApplicationUser> UsersInRole(string roleName)
        {
            var db = new ApplicationDbContext();//'db' is an instance of the entire database

            var role = db.Roles.FirstOrDefault(r => r.Name == roleName);//go to the 'Roles' table in the database and get the first
            //instance where the Name property of the role has the value of the roleName passed to this method, returning all of the
            //properties and their values(properties being Name and Id in this case).

            if (role == null)
                return new List<ApplicationUser>();

            return db.Users.Where(u => u.Roles.Any(r => r.RoleId == role.Id)).ToList();//go to the 'Users' table in the database and
            //if a user has any roles that have a RoleId value the same as the role.Id value from the 'role' variable, add that user to 
            //the return sequence. Once the query is complete, the 'ToList()' method is called on the return sequence and that list
            //of users is returned. The 'ToList()' method prevents lazy execution of the query.
        }

        public IList<ApplicationUser> UserNotInRole(string roleName)
        {
            var db = new ApplicationDbContext();

            var role = db.Roles.FirstOrDefault(r => r.Name == roleName);

            if (role == null)
                return new List<ApplicationUser>();

            return db.Users.Where(u => !u.Roles.Any(r => r.RoleId == role.Id)).ToList();
        }


    }
}