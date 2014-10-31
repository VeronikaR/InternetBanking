using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Internet_Banking.Models;
using Internet_Banking.Services.Implements;
using Internet_Banking.Services.Interfaces;

namespace Internet_Banking.Controllers
{
    public class AccountsController : Controller
    {
        private IAccountsService _accountsService;
        public AccountsController()
        {
            _accountsService = new AccountsService();
        }
        //
        // GET: /Accounts/

        public ActionResult Index()
        {
            var membershipUser = Membership.GetUser(User.Identity.Name, true);
            var id = new Guid();
            if (membershipUser != null)
            {
                var providerUserKey = membershipUser.ProviderUserKey;
                if (providerUserKey != null)
                {
                    id = (Guid) providerUserKey;
                }
            }
            List<SummaryAccountsModel> zxc = _accountsService.GetAccounts(id);
            List<SummaryAccountsModel> model = _accountsService.GetAccounts(id);
            return View(model);
        }

        //
        // GET: /Accounts/Details/5

        public ActionResult Details(int id)
        {
            return View();
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
