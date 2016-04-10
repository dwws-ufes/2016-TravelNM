using InterfacesTravelMN;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelNM.Models;
using System.Web.Helpers;

namespace TravelNM.Controllers
{
    public class RegisterController : BaseController
    {
        private IMaintenance<Customer> _maintenance;

        private IMaintenance<City> _maintenanceCity;

        public RegisterController(IMaintenance<Customer> maintenance, IMaintenance<City> maintenanceCity)
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

        public ActionResult Edit(CustomerView customerview)
        {
            int id = int.Parse(Session["IdCustomer"].ToString());
            customerview.Customer = this._maintenance.Get(id);
            customerview.Cities = _maintenanceCity.GetAll();
            return View(customerview);
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
                customerview.Customer.Salt = Crypto.GenerateSalt();
                customerview.Customer.Password = methods.GenHashSalt(customerview.Customer.Password, customerview.Customer.Salt);

                this._maintenance.Update(customerview.Customer);
                return RedirectToAction("Edit");   
            }      
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
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

                    this._maintenance.Save(customerview.Customer);
                    return RedirectToAction("../Authentication/Login/1");
                }
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
