using InternetBankingDal;
using InternetBankingDal.Providers.Implements;
using Internet_Banking.Filters;
using Internet_Banking.Models;
using Internet_Banking.Services;
using Internet_Banking.Services.Implements;
using Internet_Banking.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Internet_Banking.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUsersService _userService;
        private readonly GenericDataRepository<AdditionalUserData> _repositoryUser = new GenericDataRepository<AdditionalUserData>();

        public AdminController()
        {
            _userService = new UsersService();
        }

        #region users
        public ActionResult AddUser()
        {
            return View("EditorTemplates/AdditionalUserData", new AdditionalUserDataModel());
        }

        public ActionResult UsersList()
        {
            List<AdditionalUserDataModel> model = _userService.GetUsers();
            return View(model);
        }

        public ActionResult SaveUser(AdditionalUserDataModel model)
        {
            if (ModelState.IsValid)
            {
                if (Membership.FindUsersByName(model.UserName).Count == 0)
                {
                    model.Password = _userService.AddUser(model);
                    return View("EditorTemplates/AdditionalUserData", model);
                   // return RedirectToAction("UsersList");
                }
                ModelState.AddModelError("", "Такой логин существует, придумайте другой.");
            }

            return View("EditorTemplates/AdditionalUserData", model);
        }

        public ActionResult DeleteUser(Guid id)
        {
            _userService.DeleteUser(id);
            return RedirectToAction("UsersList");
        }

        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult CreateAccount()
        {
            return View();
        }

        //
        // POST: /Accounts/Create

        [HttpPost]
        public ActionResult CreateAccount(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Dashboard");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult CreateCard()
        {
            return View();
        }

        //
        // POST: /Accounts/Create

        [HttpPost]
        public ActionResult CreateCard(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Dashboard");
            }
            catch
            {
                return View();
            }
        }

        #endregion
    }
}
