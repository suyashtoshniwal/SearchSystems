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
        private SEARCHSYSTEMSEntities4 db = new SEARCHSYSTEMSEntities4();

        // GET: FixedDeposits
        public ActionResult Index()
        {
            return View(db.FixedDeposits.ToList());
        }

        // GET: FixedDeposits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FixedDeposit fixedDeposit = db.FixedDeposits.Find(id);
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
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FixedDeposit fixedDeposit = db.FixedDeposits.Find(id);
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
                db.Entry(fixedDeposit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fixedDeposit);
        }

        // GET: FixedDeposits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FixedDeposit fixedDeposit = db.FixedDeposits.Find(id);
            if (fixedDeposit == null)
            {
                return HttpNotFound();
            }
            return View(fixedDeposit);
        }

        // POST: FixedDeposits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FixedDeposit fixedDeposit = db.FixedDeposits.Find(id);
            db.FixedDeposits.Remove(fixedDeposit);
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
