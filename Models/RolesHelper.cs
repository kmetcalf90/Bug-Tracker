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


    }
}