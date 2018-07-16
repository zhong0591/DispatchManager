using DispatchManager.Data.Models;
using DispatchManager.Services.TruckManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DispatchManager.Controllers
{
    public class DispatchManagerController : Controller
    {

        private ITruckManager truckManager { get; set; }

        public DispatchManagerController(ITruckManager truckManager) {
            this.truckManager = truckManager;
        }


        public ActionResult EditTruck( )
        {
            return View();
        }



        [HttpPost]
        public ActionResult SaveTruck(Truck truck)
        {
            try
            { 
                truckManager.SaveOrUpdate(truck);
                return RedirectToAction("EditTruck");
            }
            catch
            {
                return View();
            }
        } 
    }
}
