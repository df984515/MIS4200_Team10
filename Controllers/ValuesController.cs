using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MIS4200_Team10.DAL;
using MIS4200_Team10.Models;

namespace MIS4200_Team10.Controllers
{
    public class ValuesController : Controller
    {
        private MIS4200Context db = new MIS4200Context();

        // GET: Values
        public ActionResult Index()
        {
            return View(db.Values.ToList());
        }

        // GET: Values/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Values values = db.Values.Find(id);
            if (values == null)
            {
                return HttpNotFound();
            }
            return View(values);
        }

        // GET: Values/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Values/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "valueID,coreValue")] Values values)
        {
            if (ModelState.IsValid)
            {
                db.Values.Add(values);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(values);
        }

        // GET: Values/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Values values = db.Values.Find(id);
            if (values == null)
            {
                return HttpNotFound();
            }
            return View(values);
        }

        // POST: Values/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "valueID,coreValue")] Values values)
        {
            if (ModelState.IsValid)
            {
                db.Entry(values).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(values);
        }

        // GET: Values/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Values values = db.Values.Find(id);
            if (values == null)
            {
                return HttpNotFound();
            }
            return View(values);
        }

        // POST: Values/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Values values = db.Values.Find(id);
            db.Values.Remove(values);
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
