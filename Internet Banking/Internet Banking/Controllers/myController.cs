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
    public class myController : Controller
    {
        private InternetBankingEntities db = new InternetBankingEntities();

        //
        // GET: /my/

        public ActionResult Index()
        {
            var accounts = db.Accounts.Include(a => a.aspnet_Users).Include(a => a.AccountType);
            return View(accounts.ToList());
        }

        //
        // GET: /my/Details/5

        public ActionResult Details(Guid id )
        {
            Accounts accounts = db.Accounts.Find(id);
            if (accounts == null)
            {
                return HttpNotFound();
            }
            return View(accounts);
        }

        //
        // GET: /my/Create

        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.aspnet_Users, "UserId", "UserName");
            ViewBag.Type = new SelectList(db.AccountTypes, "AccountTypeId", "Name");
            return View();
        }

        //
        // POST: /my/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Accounts accounts)
        {
            if (ModelState.IsValid)
            {
                accounts.AccountId = Guid.NewGuid();
                db.Accounts.Add(accounts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.aspnet_Users, "UserId", "UserName", accounts.UserId);
            ViewBag.Type = new SelectList(db.AccountTypes, "AccountTypeId", "Name", accounts.Type);
            return View(accounts);
        }

        //
        // GET: /my/Edit/5

        public ActionResult Edit(Guid id )
        {
            Accounts accounts = db.Accounts.Find(id);
            if (accounts == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.aspnet_Users, "UserId", "UserName", accounts.UserId);
            ViewBag.Type = new SelectList(db.AccountTypes, "AccountTypeId", "Name", accounts.Type);
            return View(accounts);
        }

        //
        // POST: /my/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Accounts accounts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accounts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.aspnet_Users, "UserId", "UserName", accounts.UserId);
            ViewBag.Type = new SelectList(db.AccountTypes, "AccountTypeId", "Name", accounts.Type);
            return View(accounts);
        }

        //
        // GET: /my/Delete/5

        public ActionResult Delete(Guid id )
        {
            Accounts accounts = db.Accounts.Find(id);
            if (accounts == null)
            {
                return HttpNotFound();
            }
            return View(accounts);
        }

        //
        // POST: /my/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Accounts accounts = db.Accounts.Find(id);
            db.Accounts.Remove(accounts);
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