using InterfacesTravelMN;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TravelNM.Controllers
{
    public class UserController : BaseController
    {
        private IMaintenance<User> _maintenanceUser;

        public UserController(IMaintenance<User> maintenanceUser)
        {
            this._maintenanceUser = maintenanceUser;
        }

        public ActionResult Index()
        {
            return View(this._maintenanceUser.GetAll());
        }

        public ActionResult New()
        {
            return View(new User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken()] 
        public ActionResult Create(User user)
        {
            this._maintenanceUser.Save(user);
            return RedirectToAction("Index");
        }
    }
}
