using InterfacesTravelMN;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelNM.Models;
using System.Web.Helpers;
using System.Web.Security;

namespace TravelNM.Controllers
{
    public class CustomerController : BaseController
    {
        private IMaintenance<Customer> _maintenance;
        private IMaintenance<City> _maintenanceCity;

        public CustomerController(IMaintenance<Customer> maintenance, IMaintenance<City> maintenanceCity)
        {
            this._maintenance = maintenance;
            this._maintenanceCity = maintenanceCity;
        }

        public ActionResult Index()
        {
            return View(this._maintenance.GetAll());
        }

        public ActionResult New(CustomerView customerview)
        {
            customerview.Cities = _maintenanceCity.GetAll();
            return View(customerview);
        }

        public ActionResult Edit(int id, CustomerView customerview)
        {
            customerview.Customer = this._maintenance.Get(id);
            customerview.Cities = _maintenanceCity.GetAll();
            customerview.Customer.SupportPassword = customerview.Customer.Password;
            return View(customerview);
        }

        public ActionResult EditCustomer(int id, CustomerView customerview)
        {
            if (id == int.Parse(Session["idCustomer"].ToString()))
            {
                customerview.Customer = this._maintenance.Get(id);
                customerview.Cities = _maintenanceCity.GetAll();
                customerview.Customer.SupportPassword = customerview.Customer.Password;
                return View(customerview);
            }
            else
                return RedirectToAction("EditCustomer/" + (Session["idCustomer"]).ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Update(CustomerView customerview, Methods methods)
        {
            var culture = System.Globalization.CultureInfo.CurrentCulture;
   
            if (customerview.Customer.Password.ToString().Trim().Length < 6)
            {
                if (culture.Name == "en-US")
                    MessageVal("The password field duty of 6 or more characters.", "Edit" + Convert.ToString(customerview.Customer.Id));
                else
                    MessageVal("O campo de senha dever de 6 ou mais caracteres.", "Edit" + Convert.ToString(customerview.Customer.Id));

                return null;
            }
            else
            {
                if (Session["idCustomer"] == null)
                    customerview.Customer.Status = int.Parse(Request.Form["Status"].ToString());

                if (customerview.Customer.Password != customerview.Customer.SupportPassword)
                    customerview.Customer.Password = methods.GenHashSalt(customerview.Customer.Password, customerview.Customer.Salt);

                this._maintenance.Update(customerview.Customer);

                if (Session["idCustomer"] == null)
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("EditCustomer/" + (Session["idCustomer"]).ToString());
            }      
        }

        [HttpPost]
        public JsonResult Delete(Customer customer)
        {
            this._maintenance.Delete(customer);
            return Json("ok");
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [AllowAnonymous]
        public ActionResult Create(CustomerView customerview, Methods methods)
        {
            var culture = System.Globalization.CultureInfo.CurrentCulture;

            if (ExistsEmail(customerview.Customer.Email))
            {
                if (culture.Name == "en-US")
                    MessageVal("The email is already registered in our database.", "New");
                else
                    MessageVal("E-mail já resgistrado em nossa base de dados.", "New");

                return null;
            }
            else
                if (customerview.Customer.Password.ToString().Trim().Length < 6)
                {
                    if (culture.Name == "en-US")
                        MessageVal("The password field duty of 6 or more characters.", "New");
                    else
                        MessageVal("O campo de senha dever de 6 ou mais caracteres.", "New");

                    return null;
                }
                else
                {
                    customerview.Customer.Salt = Crypto.GenerateSalt();
                    customerview.Customer.Password = methods.GenHashSalt(customerview.Customer.Password, customerview.Customer.Salt);

                    if (Request.Form["Status"] != null)
                        customerview.Customer.Status = int.Parse(Request.Form["Status"].ToString());
                    else
                        customerview.Customer.Status = 1;

                    this._maintenance.Save(customerview.Customer);
                    return RedirectToAction("Index");
                }
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [AllowAnonymous]
        public ActionResult CreateUser(CustomerView customerview, Methods methods)
        {
            var culture = System.Globalization.CultureInfo.CurrentCulture;
            
            customerview.Customer.Salt = Crypto.GenerateSalt();
            customerview.Customer.Password = methods.GenHashSalt(customerview.Customer.Password, customerview.Customer.Salt);

            if (Request.Form["Status"] != null)
                customerview.Customer.Status = int.Parse(Request.Form["Status"].ToString());
            else
                customerview.Customer.Status = 1;

            this._maintenance.Save(customerview.Customer);

            FormsAuthentication.SetAuthCookie(customerview.Customer.Email, false);
            Session["IdCustomer"] = _maintenance.Search(new[] { customerview.Customer.Email }).ToList().First().Id.ToString();

            string ReturnUrl = (string)Session["ReturnUrl"];
            if (string.IsNullOrWhiteSpace(ReturnUrl))
                return RedirectToAction("BuyPackageIndex", "TravelPackage");
            else
                return Redirect(ReturnUrl);
        }

        public void MessageVal(string Message, string Url)
        {
            Response.Write("<script> alert('" + Message + "'); window.location = '" + Url + "' </script>");
        }

        public Boolean ExistsEmail(string Email)
        {
            var Customers = _maintenance.GetAll();

            foreach (var customer in Customers)
            {
                if (customer.Email == Email)
                    return true;
            }
            return false;
        }
    }
}
