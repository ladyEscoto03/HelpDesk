namespace HelpDesk.Migrations
{
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


        }
    }
}
