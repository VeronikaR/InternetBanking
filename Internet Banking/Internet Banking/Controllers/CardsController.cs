using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using System.Web.Security;
using InternetBankingDal;
using InternetBankingDal.Providers.Implements;

namespace Internet_Banking.Controllers
{
    public class CardsController : Controller
    {
        private readonly GenericDataRepository<Cards> _repositoryCards = new GenericDataRepository<Cards>();
        private readonly InternetBankingEntities _db = new InternetBankingEntities();

        //
        // GET: /Cards/

        public ActionResult Index()
        {
            var membershipUser = Membership.GetUser();
            if (membershipUser != null && membershipUser.ProviderUserKey != null)
            {
                IList<Cards> cards = _repositoryCards.GetList(c => c.Accounts.UserId.Equals((Guid) membershipUser.ProviderUserKey), c => c.Accounts.AccountType);
                return View(cards);
            }
            return View();
        }

        //
        // GET: /Cards/Details/5

        public ActionResult Details(Guid id)
        {
            return View(_repositoryCards.GetSingle(a => a.CardId == id, a => a.Accounts.AccountType));
        }

        //
        // GET: /Cards/Create

        public ActionResult Create()
        {
            ViewBag.AccountId = new SelectList(_db.Accounts, "AccountId", "Number");
            return View();
        }

        //
        // POST: /Cards/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cards cards)
        {
            if (ModelState.IsValid)
            {
                cards.CardId = Guid.NewGuid();
                _db.Cards.Add(cards);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountId = new SelectList(_db.Accounts, "AccountId", "Number", cards.AccountId);
            return View(cards);
        }

        //
        // GET: /Cards/Edit/5

        public ActionResult Edit(Guid id)
        {
            Cards cards = _db.Cards.Find(id);
            if (cards == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountId = new SelectList(_db.Accounts, "AccountId", "Number", cards.AccountId);
            return View(cards);
        }

        //
        // POST: /Cards/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cards cards)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(cards).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountId = new SelectList(_db.Accounts, "AccountId", "Number", cards.AccountId);
            return View(cards);
        }

        //
        // GET: /Cards/Delete/5

        public ActionResult Delete(Guid id)
        {
            Cards cards = _db.Cards.Find(id);
            if (cards == null)
            {
                return HttpNotFound();
            }
            return View(cards);
        }

        //
        // POST: /Cards/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Cards cards = _db.Cards.Find(id);
            _db.Cards.Remove(cards);
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