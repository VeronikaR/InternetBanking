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
    public class CardsController : Controller
    {
        private InternetBankingEntities db = new InternetBankingEntities();

        //
        // GET: /Cards/

        public ActionResult Index()
        {
            var cards = db.Cards.Include(c => c.Accounts);
            return View(cards.ToList());
        }

        //
        // GET: /Cards/Details/5

        public ActionResult Details(Guid id)
        {
            Cards cards = db.Cards.Find(id);
            if (cards == null)
            {
                return HttpNotFound();
            }
            return View(cards);
        }

        //
        // GET: /Cards/Create

        public ActionResult Create()
        {
            ViewBag.AccountId = new SelectList(db.Accounts, "AccountId", "Number");
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
                db.Cards.Add(cards);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountId = new SelectList(db.Accounts, "AccountId", "Number", cards.AccountId);
            return View(cards);
        }

        //
        // GET: /Cards/Edit/5

        public ActionResult Edit(Guid id)
        {
            Cards cards = db.Cards.Find(id);
            if (cards == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "AccountId", "Number", cards.AccountId);
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
                db.Entry(cards).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "AccountId", "Number", cards.AccountId);
            return View(cards);
        }

        //
        // GET: /Cards/Delete/5

        public ActionResult Delete(Guid id)
        {
            Cards cards = db.Cards.Find(id);
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
            Cards cards = db.Cards.Find(id);
            db.Cards.Remove(cards);
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