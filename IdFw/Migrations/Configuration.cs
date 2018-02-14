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
            AutomaticMigrationsEnabled = true;
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

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);


            if (roleManager.FindByName("Admin") == null)
            {
                roleManager.Create(new IdentityRole("Admin"));
            }
            if (roleManager.FindByName("Commen") == null)
            {
                roleManager.Create(new IdentityRole("Commen"));
            }

            ApplicationUser myAdmin;
            ApplicationUser myFoo;


            if (userManager.FindByName("Kinan") == null)
            {
                myAdmin = new ApplicationUser()
                {
                    Email = "Kinan@Kinan.se",
                    UserName = "Kinan",
                    FirstName = "kinan",
                    LastName = "Karam",
                    Age = 25,
                    PhoneNumber="0729026684",
                    Adress = "Karlskrona",

                };
                userManager.Create(myAdmin, "!23Qwe");
            }

            if (context.Users.SingleOrDefault(u => u.Email == "Foo@Foo.se") == null)
            {
                myFoo = new ApplicationUser()
                {
                    Email = "Foo@Foo.se",
                    UserName = "Foo",
                    FirstName = "Foo",
                    LastName = "Foo",
                    Age = 25,
                    PhoneNumber = "0004000014",
                    Adress = "Växjö",

                };

                userManager.Create(myFoo, "!23Qwe");
            }


            context.SaveChanges(); // save Changes to Database

            myAdmin = userManager.FindByName("Kinan");//UserName
            myFoo = userManager.FindByEmail("Foo@Foo.se");

            userManager.AddToRole(myAdmin.Id, "Admin");
            userManager.AddToRole(myFoo.Id, "Commen");
        }
    }
}
