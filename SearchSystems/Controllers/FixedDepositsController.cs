using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SearchSystems.Models;

namespace SearchSystems.Controllers
{
    public class FixedDepositsController : Controller
    {
        private Entities db = new Entities();

        // GET: FixedDeposits
        [Authorize(Users = "FDAdmin")]
        public ActionResult Index(bool fdView = false, bool closedFDView = false)
        {
            if (fdView)
            {
                DateTime testLessThanDate = DateTime.Now.Add(new TimeSpan(30, 0, 0, 0));
                IQueryable<FixedDeposit> fdAccounts = db.FixedDeposits.Where(e => e.MaturityDate <= testLessThanDate && e.Status == true);
                return View("FDView", fdAccounts.ToList());
            }
            if(closedFDView)
            {
                IQueryable<FixedDeposit> fdAccounts = db.FixedDeposits.Where(e => e.Status == false);
                return View("ClosedFDView", fdAccounts.ToList());
            }
            
            return View(db.FixedDeposits.ToList());
        }

        // GET: FixedDeposits/Details/5
        public ActionResult Details(int? serialnumber, string bankName)
        {
            if (serialnumber == null || String.IsNullOrEmpty(bankName))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FixedDeposit fixedDeposit = db.FixedDeposits.Find(serialnumber, bankName);
            if (fixedDeposit == null)
            {
                return HttpNotFound();
            }
            return View(fixedDeposit);
        }

        // GET: FixedDeposits/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FixedDeposits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SerialNumber,BankName,AccountName,FDNumber,DateOfPurchase,Amount,RateOfInterest,MaturityDate,MaturityAmount,Status")] FixedDeposit fixedDeposit)
        {
            if (ModelState.IsValid)
            {
                db.FixedDeposits.Add(fixedDeposit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fixedDeposit);
        }

        // GET: FixedDeposits/Edit/5
        public ActionResult Edit(int? serialnumber, string bankName)
        {
            if (serialnumber == null || String.IsNullOrEmpty(bankName))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FixedDeposit fixedDeposit = db.FixedDeposits.Find(serialnumber, bankName);
            if (fixedDeposit == null)
            {
                return HttpNotFound();
            }
            return View(fixedDeposit);
        }

        // POST: FixedDeposits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SerialNumber,BankName,AccountName,FDNumber,DateOfPurchase,Amount,RateOfInterest,MaturityDate,MaturityAmount,Status")] FixedDeposit fixedDeposit)
        {
            if (ModelState.IsValid)
            {
                int oSerialNumber = Convert.ToInt32(Request["SerialNumber"]);
                string oBankName = Request["BankName"].ToString();

                var services = db.FixedDeposits.Where(a => a.SerialNumber == oSerialNumber)
                                           .Where(a => a.BankName == oBankName);
                                          

                foreach (var s in services)
                {
                    db.FixedDeposits.Remove(s);
                }

                db.FixedDeposits.Add(fixedDeposit);
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    return RedirectToAction("Index");
                }
                db.Entry(fixedDeposit).State = EntityState.Modified;
                return RedirectToAction("Index");
            }
            //ViewBag.DayID = new SelectList(db.Days, "DayID", "Day", schedule.DayID);
            //ViewBag.LanguageID = new SelectList(db.Languages, "LanguageID", "Language", schedule.LanguageID);
            //ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "Location", schedule.LocationID);
            //ViewBag.TimeID = new SelectList(db.Times, "TimeID", "Time", schedule.TimeID);
            return View(fixedDeposit);
        }

        // GET: FixedDeposits/Delete/5
        public ActionResult Delete(int? serialnumber, string bankName)
        {
            if (serialnumber == null || String.IsNullOrEmpty(bankName))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FixedDeposit fixedDeposit = db.FixedDeposits.Find(serialnumber, bankName);
            if (fixedDeposit == null)
            {
                return HttpNotFound();
            }
            return View(fixedDeposit);
        }

        // POST: FixedDeposits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? serialnumber, string bankName)
        {
            FixedDeposit fixedDeposit = db.FixedDeposits.Find(serialnumber, bankName);
            db.FixedDeposits.Remove(fixedDeposit);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RenewFD([Bind(Include = "SerialNumber,BankName,DateOfPurchase,Amount,RateOfInterest,MaturityDate,MaturityAmount,Status")] IList<FixedDeposit> fdList)
        {
            var updatedFDList = new List<FixedDeposit>();

            foreach (FixedDeposit e in fdList)
            {
                FixedDeposit fd = db.FixedDeposits.Find(e.SerialNumber, e.BankName);

                fd.DateOfPurchase = e.DateOfPurchase;
                fd.Amount = e.Amount;
                fd.RateOfInterest = e.RateOfInterest;
                fd.MaturityDate = e.MaturityDate;
                fd.MaturityAmount = e.MaturityAmount;

                fd.Status = e.Status;

                updatedFDList.Add(e);
            }
            db.SaveChanges();

            return RedirectToAction("Index", new { fdview = true });

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
