using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MIS4200_Team10.DAL;
using MIS4200_Team10.Models;

namespace MIS4200_Team10.Controllers
{
    public class UserDetailsController : Controller
    {
        private MIS4200Context db = new MIS4200Context();

        // GET: UserDetails
        public ActionResult Index(string searchString)
        {
            if (User.Identity.IsAuthenticated)
            {
                
                var testusers = from u in db.UserDetails select u;
                if (!String.IsNullOrEmpty(searchString))
                {
                    testusers = testusers.Where(u =>
                    u.lastName.Contains(searchString)
                    || u.firstName.Contains(searchString));
                    // if here, users were found so view them
                    return View(testusers.ToList());
                }
                else
                {
                    return View(db.UserDetails.ToList());
                }                

            }
            else
            {
                return View("NotAuthenticated");
            }           

        }       

            // GET: UserDetails/Details/5
            public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rec = db.Recognitions.Where(r => r.ID == id).OrderByDescending(a => a.recognitionDate).Take(10);
            var recList = rec.ToList();
            ViewBag.rec = recList;

            var totalCount = recList.Count(); //counts all the recognitions for that person
            var rec1Count = recList.Where(r => r.valueID == 1).Count();//Commit to Delivery Excellence
            var rec2Count = recList.Where(r => r.valueID == 2).Count();//Embrace Integrity and Openness
            var rec3Count = recList.Where(r => r.valueID == 3).Count();//Practice Responsible Stewardship
            var rec4Count = recList.Where(r => r.valueID == 4).Count();//Invest in an Exceptional Culture
            var rec5Count = recList.Where(r => r.valueID == 5).Count();//Ignite Passion for the Greater Good
            var rec6Count = recList.Where(r => r.valueID == 6).Count();//Strive to Innovate
            var rec7Count = recList.Where(r => r.valueID == 7).Count();//Live a Balanced Life

            ViewBag.Total = totalCount;
            ViewBag.Excellence = rec1Count;
            ViewBag.Integrity = rec2Count;
            ViewBag.Stewardship = rec3Count;
            ViewBag.Culture = rec4Count;
            ViewBag.Ignite = rec5Count;
            ViewBag.Innovate = rec6Count;
            ViewBag.Balanced = rec7Count;

            if (rec == null)
            {
                return HttpNotFound();
            }
            return View("Details");
        }

        // GET: UserDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Email,firstName,lastName,PhoneNumber,Office,Position,hireDate")] UserDetails userDetails)
        {
            if (ModelState.IsValid)
            {
                //userDetails.ID = Guid.NewGuid();
                Guid memberID;
                Guid.TryParse(User.Identity.GetUserId(), out memberID);
                userDetails.ID = memberID;
                db.UserDetails.Add(userDetails);
                //db.SaveChanges will throw an Exception if the user already exists
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return View("DuplicateUser");
                }
            }

            return View(userDetails);
        }

        // GET: UserDetails/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetails userDetails = db.UserDetails.Find(id);
            if (userDetails == null)
            {
                return HttpNotFound();
            }
            Guid memberID;
            Guid.TryParse(User.Identity.GetUserId(), out memberID);
            if (userDetails.ID == memberID)
            {
                return View("Edit");
            }
            else
            {
                return View("FailedDelete");
            }
           // return View(userDetails);
        }

        // POST: UserDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Email,firstName,lastName,PhoneNumber,Office,Position,hireDate")] UserDetails userDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userDetails);
        }

        // GET: UserDetails/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetails userDetails = db.UserDetails.Find(id);
            if (userDetails == null)
            {
                return HttpNotFound();
            }
            Guid memberID;
            Guid.TryParse(User.Identity.GetUserId(), out memberID);
            if (userDetails.ID == memberID)
            {
                return View("Delete");
            }
            else
            {
                return View("FailedDelete");
            }
            // return View(userDetails);

        }

        // POST: UserDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            UserDetails userDetails = db.UserDetails.Find(id);
            db.UserDetails.Remove(userDetails);
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
