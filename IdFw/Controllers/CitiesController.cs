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
    [Authorize(Users = "Admin")]
    public class CitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cities
        public ActionResult Index()
        {
            return View(db.Cityis.ToList());
        }

        // GET: Cities/Details/5
        public ActionResult Details(int id=0)
        {
            if (id <1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cityis.Include("Countries").FirstOrDefault(x => x.Id == id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // GET: Cities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] City city)
        {
            if (ModelState.IsValid)
            {
                db.Cityis.Add(city);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(city);
        }

        // GET: Cities/Edit/5
        public ActionResult Edit(int id =0)
        {
            if (id <0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cityis.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] City city)
        {
            if (ModelState.IsValid)
            {
                db.Entry(city).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(city);
        }

        // GET: Cities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cityis.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Users= "admin@admin.se")]
        public ActionResult DeleteConfirmed(int id)
        {
            City city = db.Cityis.Find(id);
            db.Cityis.Remove(city);
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

        [HttpGet]
        public ActionResult AllCountries(int id = 0)
        {

            City city = db.Cityis.Include("Countries").FirstOrDefault(x => x.Id == id);

            List<Country> country = db.Countries.ToList();

            ViewBag.cId = id;              //////////// Cours ID//////
            return View(country);
        }
        [HttpGet]

        public ActionResult CountrisToCities(int id = 0, int ciId = 0)
        {

            Country country = db.Countries.FirstOrDefault(s => s.Id == ciId);

            City city = db.Cityis.Include("Countries").SingleOrDefault(c => c.Id == id);

            country.Cities.Add(city);   
            db.SaveChanges();       /// Save Changes to the  Data Base

            return RedirectToAction("Details", new { id = id });
        }
    }
}
