namespace WebApplication7.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using WebApplication7.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Developer" });
            }

            if (!context.Roles.Any(r => r.Name == "ProjectManager"))
            {
                roleManager.Create(new IdentityRole { Name = "ProjectManager" });
            }

            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitter" });
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "kmetcalf@gtcc.edu"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "kmetcalf@gtcc.edu",
                    Email = "kmetcalf@gtcc.edu",
                    FirstName = "Kevin",
                    LastName = "Metcalf",
                    DisplayName = "Kevin Metcalf",
                }, "Uncgweil09!");
            }

            var administratorUserID = userManager.FindByEmail("kmetcalf@gtcc.edu").Id;
            userManager.AddToRole(administratorUserID, "Admin");

            if (!context.TicketPriorities.Any(u => u.name == "High"))
            { context.TicketPriorities.Add(new TicketPriority { name = "High" }); }

            if (!context.TicketPriorities.Any(u => u.name == "Medium"))
            { context.TicketPriorities.Add(new TicketPriority { name = "Medium" }); }

            if (!context.TicketPriorities.Any(u => u.name == "Low"))
            { context.TicketPriorities.Add(new TicketPriority { name = "Low" }); }

            if (!context.TicketPriorities.Any(u => u.name == "Urgent"))
            { context.TicketPriorities.Add(new TicketPriority { name = "Urgent" }); }

            if (!context.TicketTypes.Any(u => u.name == "Production Fix"))
            { context.TicketTypes.Add(new TicketType { name = "Production Fix" }); }

            if (!context.TicketTypes.Any(u => u.name == "Project Task"))
            { context.TicketTypes.Add(new TicketType { name = "Project Task" }); }

            if (!context.TicketTypes.Any(u => u.name == "Software Update"))
            { context.TicketTypes.Add(new TicketType { name = "Software Update" }); }

            if (!context.TicketStatuses.Any(u => u.name == "New"))
            { context.TicketStatuses.Add(new TicketStatus { name = "New" }); }

            if (!context.TicketStatuses.Any(u => u.name == "In Development"))
            { context.TicketStatuses.Add(new TicketStatus { name = "In Development" }); }

            if (!context.TicketStatuses.Any(u => u.name == "Completed"))
            { context.TicketStatuses.Add(new TicketStatus { name = "Completed" }); }

        }
    }
}