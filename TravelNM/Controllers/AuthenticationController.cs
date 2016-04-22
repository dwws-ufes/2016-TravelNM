using InterfacesTravelMN;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TravelNM.Helpers;
using TravelNM.Models;

namespace TravelNM.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : BaseController
    {
        private IAuthentication _authentication;
        private IMaintenance<Customer> _maintenance;
        private IMaintenance<City> _maintenanceCity;

        public AuthenticationController(IAuthentication authentication, IMaintenance<Customer> maintenance, 
            IMaintenance<City> maintenanceCity)
        {
            this._authentication = authentication;
            this._maintenance = maintenance;
            this._maintenanceCity = maintenanceCity;
        }

        public ActionResult LoginAdmin(CustomerView customerview)
        {
            New(customerview);
            ViewBag.Action = "LoginAdmin";
            return View("Login");
        }
        public ActionResult LoginCustomer(string ReturnUrl, CustomerView customerview)
        {
            New(customerview);
            Session["ReturnUrl"] = ReturnUrl;
            ViewBag.Action = "LoginCustomer";
            return View("Login");
        }

        public ActionResult New(CustomerView customerview)
        {
            customerview.Cities = _maintenanceCity.GetAll();
            return View(customerview);
        }

        [HttpPost]
        public ActionResult LoginCustomer(Customer customer, Methods methods)
        {
            try
            {
                customer.Password = methods.GenHashSalt(customer.Password, _maintenance.Search(new[] { customer.Email }).ToList().First().Salt);

                if (this._authentication.LoginCustomer(customer) == null)
                    return RedirectToAction("Index", "UnauthorizedError");
                else
                {
                    FormsAuthentication.SetAuthCookie(customer.Email, false);
                    Session["IdCustomer"] = _maintenance.Search(new[] { customer.Email }).ToList().First().Id.ToString();

                    string ReturnUrl = (string)Session["ReturnUrl"];
                    if (string.IsNullOrWhiteSpace(ReturnUrl))
                        return RedirectToAction("BuyPackageIndex", "TravelPackage");
                    else
                        return Redirect(ReturnUrl);
                }
            }
            catch
            {
                return RedirectToAction("Index", "UnauthorizedError");
            }
        }

        [HttpPost]
        public ActionResult LoginAdmin(User user)
        {
            try
            {
                if (this._authentication.Login(user) == null)
                    return RedirectToAction("Index", "UnauthorizedError");
                else
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    return RedirectToAction("Index", "Admin");
                }
            }
            catch
            {
                return RedirectToAction("Index", "UnauthorizedError");
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
