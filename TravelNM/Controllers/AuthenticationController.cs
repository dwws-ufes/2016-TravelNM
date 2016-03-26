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

        public AuthenticationController(IAuthentication authentication)
        {
            this._authentication = authentication;
        }

        public ActionResult Login()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (this._authentication.Login(user) ==  null)
            {
                throw new Exception("Falha no login");
            }
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

            if("0" == id)
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
