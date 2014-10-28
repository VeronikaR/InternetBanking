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
        private IUsersService _userService;

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
                _userService.AddUser(model);
                return RedirectToAction("UsersList");
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
        #endregion
    }
}
