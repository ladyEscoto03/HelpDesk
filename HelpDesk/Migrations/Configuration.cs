namespace HelpDesk.Migrations
{
    using HelpDesk.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HelpDesk.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HelpDesk.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.


            context.Roles.AddOrUpdate(r => r.Name,
      new IdentityRole { Name = "Administrador" },
      new IdentityRole { Name = "Cliente" },
      new IdentityRole { Name = "Empleado" }
  );

            context.TicketStatuses.AddOrUpdate(s => s.Name,
                new Models.TicketStatus { Name = "Abierto" },
                new Models.TicketStatus { Name = "Asignado" },
                new Models.TicketStatus { Name = "En progreso" },
                new Models.TicketStatus { Name = "Pendiente" },
                new Models.TicketStatus { Name = "Resuelto" },
                new Models.TicketStatus { Name = "Cerrado" },
                new Models.TicketStatus { Name = "Cancelado" }
            );

            context.SaveChanges();

            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context)
            );

      
            var userAdmin = new ApplicationUser
            {
                UserName = "finn@gmail.com",
                Email = "finn@gmail.com",
                Name = "Finn Mertens",
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            var resultAdmin = userManager.Create(userAdmin, "Finn123.");

            if (resultAdmin.Succeeded)
            {
                userManager.AddToRole(userAdmin.Id, "Administrador");
            }

            var userEmployee = new ApplicationUser
            {
                UserName = "abadeer@gmail.com",
                Email = "abadeer@gmail.com",
                Name = "Hunson Abadeer",
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            var resultEmployee = userManager.Create(userEmployee, "Abadeer123.");

            if (resultEmployee.Succeeded)
            {
                userManager.AddToRole(userEmployee.Id, "Empleado");
            }


        }
    }
}
