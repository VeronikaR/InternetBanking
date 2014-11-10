using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using InternetBankingDal;
using InternetBankingDal.Providers.Implements;

namespace Internet_Banking.Controllers
{
    public class AccountsController : Controller
    {
        private readonly GenericDataRepository<Accounts> _repositoryAccounts = new GenericDataRepository<Accounts>();
        
        //
        // GET: /Accounts/

        public ActionResult Index()
        {
            var membershipUser = Membership.GetUser();
            if (membershipUser != null && membershipUser.ProviderUserKey != null)
            {
                IList<Accounts> accounts = _repositoryAccounts.GetList(c => c.UserId.Equals((Guid)membershipUser.ProviderUserKey), c => c.Cards, c => c.AccountType);
                return View(accounts);
            }
            return View();
        }

        //
        // GET: /Accounts/Details/5

        public ActionResult Details(Guid id)
        {
            
            return View(_repositoryAccounts.GetSingle(a => a.AccountId == id, a => a.AccountType ,  a => a.Cards));
        }

        //
        // GET: /Accounts/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Accounts/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Accounts/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Accounts/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Accounts/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Accounts/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
