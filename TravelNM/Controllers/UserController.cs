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

        public ActionResult Edit(int id)
        {
            return View(this._maintenanceUser.Get(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Update(User user)
        {
            this._maintenanceUser.Update(user);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Delete(User user)
        {
            this._maintenanceUser.Delete(user);
            return Json("ok");
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
