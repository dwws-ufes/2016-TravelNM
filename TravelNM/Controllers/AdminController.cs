using InterfacesTravelMN;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelNM.Models;


namespace TravelNM.Controllers
{
    public class AdminController : BaseController
    {
        private IMaintenance<City> _maintenanceCity;
        private IMaintenance<TravelPackage> _maintenanceTravelPackage;
        private IMaintenance<TravelPackageBuy> _maintenanceTravelPackageBuy;
        private IMaintenance<Customer> _maintenanceCustomer;

        public AdminController(IMaintenance<City> maintenanceCity, IMaintenance<TravelPackage> maintenanceTravelPackage,
            IMaintenance<TravelPackageBuy> maintenanceTravelPackageBuy, IMaintenance<Customer> maintenanceCustomer)
        {
            this._maintenanceCity = maintenanceCity;
            this._maintenanceTravelPackage = maintenanceTravelPackage;
            this._maintenanceTravelPackageBuy = maintenanceTravelPackageBuy;
            this._maintenanceCustomer = maintenanceCustomer;
        }

        public ActionResult Index(DashboardView dashboardview)
        {
            dashboardview.TotalCities = _maintenanceCity.GetAll().Count();
            dashboardview.TotalPackages = _maintenanceTravelPackage.GetAll().Count();
            dashboardview.TotaSoldPackages = _maintenanceTravelPackageBuy.GetAll().Count();
            dashboardview.TotalCustomers = _maintenanceCustomer.GetAll().Where(c => c.Status == 1).Count();

            dashboardview.TotaSoldPackagesCanceled = _maintenanceTravelPackageBuy.GetAll().Where(s => s.Status == 3).Count();
            dashboardview.TotaSoldPackagesWaitingPayment= _maintenanceTravelPackageBuy.GetAll().Where(s => s.Status == 1).Count();
            dashboardview.TotaSoldPackagesPaid = _maintenanceTravelPackageBuy.GetAll().Where(s => s.Status == 2).Count();

            return View(dashboardview);
        }

    }
}
