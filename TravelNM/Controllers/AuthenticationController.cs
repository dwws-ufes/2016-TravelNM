using InterfacesTravelMN;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TravelNM.Helpers;

namespace TravelNM.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : BaseController
    {
        private IAuthentication _authentication;
        private IMaintenance<Customer> _maintenance;

        public AuthenticationController(IAuthentication authentication, IMaintenance<Customer> maintenance)
        {
            this._authentication = authentication;
            this._maintenance = maintenance;
        }

        public ActionResult LoginAdmin()
        {
            ViewBag.Action = "LoginAdmin";
            return View("Login");
        }
        public ActionResult LoginCustomer(string ReturnUrl)
        {
            Session["ReturnUrl"] = ReturnUrl;
            ViewBag.Action = "LoginCustomer";
            return View("Login");
        }

        [HttpPost]
        public ActionResult LoginCustomer(Customer customer, Methods methods)
        {
            customer.Password = methods.GenHashSalt(customer.Password, _maintenance.Search(new[] { customer.Email }).ToList().First().Salt);

            if (this._authentication.LoginCustomer(customer) == null)
                throw new Exception("Falha no login");
            else
            {
                FormsAuthentication.SetAuthCookie(customer.Email, false);
                Session["IdCustomer"] = _maintenance.Search(new[] { customer.Email }).ToList().First().Id.ToString();

                string ReturnUrl = (string) Session["ReturnUrl"];
                if (string.IsNullOrWhiteSpace(ReturnUrl))
                    return RedirectToAction("Index", "TravelPackageBuy");
                else
                    return Redirect(ReturnUrl);
            }

        }

        [HttpPost]
        public ActionResult LoginAdmin(User user)
        {

            if (this._authentication.Login(user) == null)
                throw new Exception("Falha no login");
            else
            {
                FormsAuthentication.SetAuthCookie(user.Email, false);
                return RedirectToAction("Index", "Admin");
            }

        }


        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            RouteData.Values["culture"] = culture;  // set culture

            var controller = (Request.UrlReferrer.Segments.Skip(2).Take(1).SingleOrDefault() ?? "Home").Trim('/');
            // Home is default controller

            var action = (Request.UrlReferrer.Segments.Skip(3).Take(1).SingleOrDefault() ?? "Index").Trim('/');

            var id = (Request.UrlReferrer.Segments.Skip(4).Take(1).SingleOrDefault() ?? "0").Trim('/');

            if ("0" == id)
                return RedirectToAction(action, controller, new { culture = culture });
            else
                return RedirectToAction(action, controller, new { culture = culture, id = id });

        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            FormsAuthentication.RedirectToLoginPage();
        }

    }
}
