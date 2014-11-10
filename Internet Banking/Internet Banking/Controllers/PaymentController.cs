using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InternetBankingDal;

namespace Internet_Banking.Controllers
{
    public class PaymentController : Controller
    {
        private InternetBankingEntities db = new InternetBankingEntities();

        //
        // GET: /Payment/

        public ActionResult Index()
        {
            var payments = db.Payments.Include(p => p.PaymentCompanies);
            return View(payments.ToList());
        }

        //
        // GET: /Payment/Details/5

        public ActionResult Details(Guid id)
        {
            Payments payments = db.Payments.Find(id);
            if (payments == null)
            {
                return HttpNotFound();
            }
            return View(payments);
        }

        //
        // GET: /Payment/Create

        public ActionResult Create()
        {
            ViewBag.PaymentCompanyId = new SelectList(db.PaymentCompanies, "PaymentCompanyId", "Name");
            return View();
        }

        //
        // POST: /Payment/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Payments payments)
        {
            if (ModelState.IsValid)
            {
                payments.PaymentId = Guid.NewGuid();
                db.Payments.Add(payments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PaymentCompanyId = new SelectList(db.PaymentCompanies, "PaymentCompanyId", "Name", payments.PaymentCompanyId);
            return View(payments);
        }

        //
        // GET: /Payment/Edit/5

        public ActionResult Edit(Guid id)
        {
            Payments payments = db.Payments.Find(id);
            if (payments == null)
            {
                return HttpNotFound();
            }
            ViewBag.PaymentCompanyId = new SelectList(db.PaymentCompanies, "PaymentCompanyId", "Name", payments.PaymentCompanyId);
            return View(payments);
        }

        //
        // POST: /Payment/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Payments payments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PaymentCompanyId = new SelectList(db.PaymentCompanies, "PaymentCompanyId", "Name", payments.PaymentCompanyId);
            return View(payments);
        }

        //
        // GET: /Payment/Delete/5

        public ActionResult Delete(Guid id)
        {
            Payments payments = db.Payments.Find(id);
            if (payments == null)
            {
                return HttpNotFound();
            }
            return View(payments);
        }

        //
        // POST: /Payment/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Payments payments = db.Payments.Find(id);
            db.Payments.Remove(payments);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}