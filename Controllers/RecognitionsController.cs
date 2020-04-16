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
using System.Net.Mail;

namespace MIS4200_Team10.Controllers
{
    public class RecognitionsController : Controller
    {
        private MIS4200Context db = new MIS4200Context();

        // GET: Recognitions
        public ActionResult Index(string searchString)
        {           
                var testusers = from u in db.Recognitions.Include(r => r.UserDetails).Include(r => r.Values) select u;
                if (!String.IsNullOrEmpty(searchString))
                {
                    testusers = testusers.Where(u =>
                    u.employee.Contains(searchString));                    
                    // if here, users were found so view them
                    return View(testusers.ToList());
                }
                else
                {
                    return View(db.Recognitions.ToList());
                }           

        }       
        

        // GET: Recognitions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recognition recognition = db.Recognitions.Find(id);
            if (recognition == null)
            {
                return HttpNotFound();
            }
            return View(recognition);
        }

        // GET: Recognitions/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.ID = new SelectList(db.UserDetails, "ID", "fullName");
                ViewBag.valueID = new SelectList(db.Values, "valueID", "coreValue");
                return View();
            }            
            else
            {
                return View("NotAuthenticated");
            }
                
        }

        // POST: Recognitions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "recognitionID,ID,recognitionDate,valueID,recognitionReason")] Recognition recognition)
        {
            if (ModelState.IsValid)
            {
                db.Recognitions.Add(recognition);
                db.SaveChanges();

                ViewBag.ID = new SelectList(db.UserDetails, "ID", "fullName", recognition.ID);
                ViewBag.valueID = new SelectList(db.Values, "valueID", "coreValue", recognition.valueID);

                SmtpClient myClient = new SmtpClient();
                // the following line has to contain the email address and password of someone
                // authorized to use the email server (you will need a valid Ohio account/password
                // for this to work)

                myClient.Credentials = new NetworkCredential("AuthorizedUser", "UserPassword");
                MailMessage myMessage = new MailMessage();

                // the syntax here is email address, username (that will appear in the email)
                MailAddress from = new MailAddress("luce@ohio.edu", "Recognition System Admin");
                myMessage.From = from;
                var user = db.UserDetails.Find(recognition.ID);
                var userEmail = user.Email;
                var value = db.Values.Find(recognition.valueID);
                var recognitionValue = value.coreValue;
                myMessage.To.Add(userEmail); // this should be replaced with model data
                                             // as shown at the end of this document
                myMessage.Subject = "You Have Been Recognized!";
                // the body of the email is hard coded here but could be dynamically created using data
                // from the model- see the note at the end of this document
                myMessage.Body = "Congratulations!" + Environment.NewLine + Environment.NewLine; 
                myMessage.Body += "You have been recgonized for the core value of "; 
                myMessage.Body += "'" + Convert.ToString(recognitionValue) + "'" + "!" + Environment.NewLine + Environment.NewLine;                
                myMessage.Body += "This was the reason for recognizing you: ";
                myMessage.Body += "'" + Convert.ToString(recognition.recognitionReason) + "'" + Environment.NewLine + Environment.NewLine;
                myMessage.Body += "Again, congratulations on being recognized and thank you for living our values!" + Environment.NewLine + Environment.NewLine;
                myMessage.Body += "Best Regards," + Environment.NewLine + Environment.NewLine;
                myMessage.Body += "Dave Rosevelt - CEO of Centric Consulting";
                try
                {
                    myClient.Send(myMessage);
                    TempData["mailError"] = "";
                }
                catch (Exception ex)
                {
                    // this captures an Exception and allows you to display the message in the View
                    TempData["mailError"] = ex.Message;
                }

                return View("Email");                
            }
            else
            {
                return View("Email");
            }            
        }
                

        // GET: Recognitions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recognition recognition = db.Recognitions.Find(id);
            if (recognition == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.UserDetails, "ID", "fullName", recognition.ID);
            ViewBag.valueID = new SelectList(db.Values, "valueID", "coreValue", recognition.valueID);
            return View(recognition);
        }

        // POST: Recognitions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "recognitionID,ID,recognitionDate,valueID,recognitionReason")] Recognition recognition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recognition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.UserDetails, "ID", "fullName", recognition.ID);
            ViewBag.valueID = new SelectList(db.Values, "valueID", "coreValue", recognition.valueID);
            return View(recognition);
        }

        // GET: Recognitions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recognition recognition = db.Recognitions.Find(id);
            if (recognition == null)
            {
                return HttpNotFound();
            }
            return View(recognition);
        }

        // POST: Recognitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recognition recognition = db.Recognitions.Find(id);
            db.Recognitions.Remove(recognition);
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
