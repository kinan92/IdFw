namespace IdFw.Migrations
{
    using IdFw.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IdFw.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "IdFw.Models.ApplicationDbContext";
        }

        protected override void Seed(IdFw.Models.ApplicationDbContext context)
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

            ApplicationUser myAdmin;
            ApplicationUser myFoo;
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            if (context.Users.SingleOrDefault(u => u.Email == "admin@admin.se") == null)
            {
                myAdmin = new ApplicationUser() { Email = "admin@admin.se", UserName = "admin@admin.se" };
                userManager.Create(myAdmin, "!23Qwe");
            }

            if (context.Users.SingleOrDefault(u => u.Email == "Foo@Foo.se") == null)
            {
                myFoo = new ApplicationUser() { Email = "Foo@Foo.se", UserName = "Foo@Foo.se" };
                userManager.Create(myFoo, "!23Qwe");
            }

            if (roleManager.FindByName("Admin") == null)
            {
                roleManager.Create(new IdentityRole("Admin"));
            }
            if (roleManager.FindByName("Commen") == null)
            {
                roleManager.Create(new IdentityRole("Commen"));
            }

            context.SaveChanges(); // save Changes to Database

            myAdmin = userManager.FindByEmail("admin@admin.se");
            myFoo = userManager.FindByEmail("Foo@Foo.se");

            userManager.AddToRole(myAdmin.Id, "Admin");
            userManager.AddToRole(myFoo.Id, "Commen");
        }
    }
}
