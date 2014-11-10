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
    public class DepositController : Controller
    {
        private InternetBankingEntities db = new InternetBankingEntities();

        //
        // GET: /Deposit/

        public ActionResult Index()
        {
            var deposit = db.Deposit.Include(d => d.Accounts);
            return View(deposit.ToList());
        }

        //
        // GET: /Deposit/Details/5

        public ActionResult Details(Guid id)
        {
            Deposit deposit = db.Deposit.Find(id);
            if (deposit == null)
            {
                return HttpNotFound();
            }
            return View(deposit);
        }

        //
        // GET: /Deposit/Create

        public ActionResult Create()
        {
            ViewBag.AccountId = new SelectList(db.Accounts, "AccountId", "Number");
            return View();
        }

        //
        // POST: /Deposit/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Deposit deposit)
        {
            if (ModelState.IsValid)
            {
                deposit.DepositId = Guid.NewGuid();
                db.Deposit.Add(deposit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountId = new SelectList(db.Accounts, "AccountId", "Number", deposit.AccountId);
            return View(deposit);
        }

        //
        // GET: /Deposit/Edit/5

        public ActionResult Edit(Guid id)
        {
            Deposit deposit = db.Deposit.Find(id);
            if (deposit == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "AccountId", "Number", deposit.AccountId);
            return View(deposit);
        }

        //
        // POST: /Deposit/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Deposit deposit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deposit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "AccountId", "Number", deposit.AccountId);
            return View(deposit);
        }

        //
        // GET: /Deposit/Delete/5

        public ActionResult Delete(Guid id)
        {
            Deposit deposit = db.Deposit.Find(id);
            if (deposit == null)
            {
                return HttpNotFound();
            }
            return View(deposit);
        }

        //
        // POST: /Deposit/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Deposit deposit = db.Deposit.Find(id);
            db.Deposit.Remove(deposit);
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