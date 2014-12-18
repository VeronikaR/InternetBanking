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
using System.ComponentModel.DataAnnotations;

namespace Internet_Banking.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUsersService _userService;
        private readonly GenericDataRepository<AdditionalUserData> _repositoryUser = new GenericDataRepository<AdditionalUserData>();
        private InternetBankingEntities db = new InternetBankingEntities();
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
            var ac = new Accounts();
            ac.StartDate = DateTime.Now.Date;
            ViewBag.UserId = new SelectList(db.aspnet_Users, "UserId", "UserName");
            ViewBag.Type = new SelectList(db.AccountTypes, "AccountTypeId", "Name");
            return View(ac);
        }

        //
        // POST: /Accounts/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccount(Accounts accounts)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var minTime = DateTime.Parse("01.01.1753");
                    var maxTime = DateTime.MaxValue;
                    if (accounts.StartDate > maxTime || accounts.StartDate < minTime)
                        ModelState.AddModelError("", "Дата вне диапазона.");
                    else
                    {
                        accounts.AccountId = Guid.NewGuid();
                        db.Accounts.Add(accounts);
                        db.SaveChanges();
                        return RedirectToAction("Dashboard");
                    }
                }
                ViewBag.UserId = new SelectList(db.aspnet_Users, "UserId", "UserName", accounts.UserId);
                ViewBag.Type = new SelectList(db.AccountTypes, "AccountTypeId", "Name", accounts.Type);
                return View(accounts);

               
            }
            catch
            {
                return View();
            }
        }
        public ActionResult CreateCard()
        {
            ViewBag.CardStatus = new EnumHelper().SelectList;
            ViewBag.AccountId = new SelectList(db.Accounts, "AccountId", "Number");
            return View();
        }

        //
        // POST: /Accounts/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCard(Cards cards)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var minTime = DateTime.Parse("01.01.1753");
                    var maxTime = DateTime.MaxValue;
                    if (cards.EndDate > maxTime || cards.EndDate < minTime)
                        ModelState.AddModelError("", "EndDate Дата вне диапазона.");
                    if (cards.StartDate > maxTime || cards.StartDate < minTime)
                        ModelState.AddModelError("", "StartDate Дата вне диапазона.");
                    else
                    {
                        cards.CardId = Guid.NewGuid();
                        db.Cards.Add(cards);
                        db.SaveChanges();
                        ModelState.AddModelError("", "Операция прошла успешно.");
                        return View(cards);
                        //return RedirectToAction("Dashboard");
                    }

                }
                //var states = from CardState s in Enum.GetValues(typeof(CardState))
                //             select new { ID = (int)s, Name = s.ToString() };
                ViewBag.CardStatus = new EnumHelper().SelectList;//new SelectList(states, "State", "Name", cards.State);
                ViewBag.AccountId = new SelectList(db.Accounts, "AccountId", "Number", cards.AccountId);
                return View(cards);

            }
            catch
            {
                return View();
            }
        }

        #endregion
    }
}
