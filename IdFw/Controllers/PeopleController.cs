using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdFw.Models;

namespace IdFw.Controllers
{
    [Authorize]
    public class PeopleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: people
        public ActionResult Index()
        {
            return View(db.people.ToList());
        }

        // GET: people/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person Person = db.people.Find(id);
            if (Person == null)
            {
                return HttpNotFound();
            }
            return View(Person);
        }

        // GET: people/Create
        public ActionResult Create()
        {
            ViewModelPerson vm = new ViewModelPerson();
            vm.country = db.Countries.ToList();
            return View(vm);
        }

        // POST: people/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Age")] Person Person, int City)
        {
            if (ModelState.IsValid && City > 0)
            {
                City city = db.Cityis.FirstOrDefault(x => x.Id == City);
                Person.City = city;
                db.people.Add(Person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Person);
        }

        // GET: people/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person Person = db.people.Find(id);
            if (Person == null)
            {
                return HttpNotFound();
            }
            ViewModelPerson vmp = new ViewModelPerson();
            vmp.country = db.Countries.ToList();
            vmp.Name = Person.Name;
            vmp.City = Person.City;
            vmp.Age = Person.Age;
            
            return View(vmp);
        }

        // POST: people/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Age")] Person Person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Person);
        }

        // GET: people/Delete/5
        [Authorize(Users = "Kinan")]
        public ActionResult Delete(int? id)
        {
        
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person Person = db.people.Find(id);
            if (Person == null)
            {
                return HttpNotFound();
            }
            return View(Person);

        }

        // POST: people/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize (Users = "Kinan")]
        public ActionResult DeleteConfirmed(int id)
        {
            Person Person = db.people.Find(id);
            db.people.Remove(Person);
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
    }
}
