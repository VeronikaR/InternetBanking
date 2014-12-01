using System;
using System.Web.Mvc;
using System.Web.Security;
using InternetBankingDal;
using InternetBankingDal.Providers.Implements;
using Internet_Banking.Models;
using Internet_Banking.Services.Implements;
using Internet_Banking.Services.Interfaces;

namespace Internet_Banking.Controllers
{
    [Authorize]
    public class UserPageController : Controller
    {
        private readonly GenericDataRepository<AdditionalUserData> _repositoryUser = new GenericDataRepository<AdditionalUserData>();

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            FormsAuthentication.SignOut();
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /UserPage/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && Membership.ValidateUser(model.UserName, model.Password))
            {
                var userData = _repositoryUser.GetSingle(x => x.UserId == (Guid)Membership.GetUser(model.UserName).ProviderUserKey);
                if (userData != null && userData.IsTemporary)
                    return View("CreatePassword", new CreatePasswordModel() { UserName = model.UserName });
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                return RedirectToLocal(returnUrl);
            }
            var membershipUser = Membership.GetUser();
            if (membershipUser != null && membershipUser.IsLockedOut)
                ModelState.AddModelError("", "Пользователь заблокирован.");
            else
            // If we got this far, something failed, redisplay form
                ModelState.AddModelError("", "Введен неверный логин или пароль. Для восстановления необходимо обратиться в отделение МБанка (при себе иметь паспорт).");
            return View(model);
        }

        //
        // POST: /UserPage/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /UserPage/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /UserPage/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    Membership.CreateUser(model.UserName, model.Password);
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                   // WebSecurity.Login(model.UserName, model.Password);
                    return RedirectToAction("Index", "Home");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /UserPage/Manage

        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Ваш пароль был изменен."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : "";
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /UserPage/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (ModelState.IsValid)
            {
                // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    changePasswordSucceeded = Membership.Provider.ChangePassword(User.Identity.Name, model.OldPassword,
                        model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("Manage", new {Message = ManageMessageId.ChangePasswordSuccess});
                }
                ModelState.AddModelError("", "Пароли введены некорректно.");
            }
            
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        //
        // GET: /UserPage/CreatePassword
        public ActionResult CreatePassword(ManageMessageId? message, string userName)
        {
            ViewBag.UserName = userName;
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Ваш пароль был изменен."
                : message == ManageMessageId.SetPasswordSuccess ? "Ваш пароль успешно установлен."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : "";
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /UserPage/CreatePassword

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult CreatePassword(CreatePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = Membership.GetUser(model.UserName);
                // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                bool setPasswordSuccess;
                try
                {
// ReSharper disable once PossibleNullReferenceException
                    setPasswordSuccess = user.ChangePassword(user.ResetPassword(), model.NewPassword);
                    //Membership.Provider.ChangePassword(User.Identity.Name, oldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    setPasswordSuccess = false;
                }

                if (setPasswordSuccess)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);

                    var userData =
                        _repositoryUser.GetSingle(
                            x => user.ProviderUserKey != null && x.UserId == (Guid) user.ProviderUserKey);
                    userData.IsTemporary = false;
                    _repositoryUser.Update(userData);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Пароли введены некорректно.");
            }
             //If we got this far, something failed, redisplay form
            return View(model);
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
