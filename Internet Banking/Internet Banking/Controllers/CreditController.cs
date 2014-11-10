using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using InternetBankingDal;
using InternetBankingDal.Providers.Implements;

namespace Internet_Banking.Controllers
{
    public class CreditController : Controller
    {
        private readonly InternetBankingEntities _db = new InternetBankingEntities();
        private readonly GenericDataRepository<Credit> _repositoryCredids = new GenericDataRepository<Credit>();

        //
        // GET: /Credit/

        public ActionResult Index()
        {
            var membershipUser = Membership.GetUser();
            if (membershipUser != null && membershipUser.ProviderUserKey != null)
            {
                IList<Credit> credit = _repositoryCredids.GetList(c => c.Accounts.UserId.Equals((Guid)membershipUser.ProviderUserKey), c => c.Accounts.AccountType);
                return View(credit);
            }
            return View();
        }

        //
        // GET: /Credit/Details/5

        public ActionResult Details(Guid id)
        {
            Credit credit = _db.Credit.Find(id);
            if (credit == null)
            {
                return HttpNotFound();
            }
            return View(credit);
        }

        //
        // GET: /Credit/Create

        public ActionResult Create()
        {
            ViewBag.AccountId = new SelectList(_db.Accounts, "AccountId", "Number");
            ViewBag.CreditPaymentId = new SelectList(_db.CreditPayments, "CreditPaymentId", "CreditPaymentId");
            return View();
        }

        //
        // POST: /Credit/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Credit credit)
        {
            if (ModelState.IsValid)
            {
                credit.CreditId = Guid.NewGuid();
                _db.Credit.Add(credit);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountId = new SelectList(_db.Accounts, "AccountId", "Number", credit.AccountId);
            ViewBag.CreditPaymentId = new SelectList(_db.CreditPayments, "CreditPaymentId", "CreditPaymentId", credit.CreditPaymentId);
            return View(credit);
        }

        //
        // GET: /Credit/Edit/5

        public ActionResult Edit(Guid id)
        {
            Credit credit = _db.Credit.Find(id);
            if (credit == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountId = new SelectList(_db.Accounts, "AccountId", "Number", credit.AccountId);
            ViewBag.CreditPaymentId = new SelectList(_db.CreditPayments, "CreditPaymentId", "CreditPaymentId", credit.CreditPaymentId);
            return View(credit);
        }

        //
        // POST: /Credit/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Credit credit)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(credit).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountId = new SelectList(_db.Accounts, "AccountId", "Number", credit.AccountId);
            ViewBag.CreditPaymentId = new SelectList(_db.CreditPayments, "CreditPaymentId", "CreditPaymentId", credit.CreditPaymentId);
            return View(credit);
        }

        //
        // GET: /Credit/Delete/5

        public ActionResult Delete(Guid id)
        {
            Credit credit = _db.Credit.Find(id);
            if (credit == null)
            {
                return HttpNotFound();
            }
            return View(credit);
        }

        //
        // POST: /Credit/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Credit credit = _db.Credit.Find(id);
            _db.Credit.Remove(credit);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}