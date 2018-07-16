using DispatchManager.Data.Models;
using DispatchManager.Services.TruckManage;
using DispatchManager.Services.VinNumberManage;
using DispatchManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DispatchManager.Controllers
{
    public class DispatchManagerController : Controller
    {

        private ITruckManager truckManager { get; set; }
        private IVinNumberManager vinNumberManager { get; set; }
        public DispatchManagerController(ITruckManager truckManager, IVinNumberManager vinNumberManager)
        {
            this.truckManager = truckManager;
            this.vinNumberManager = vinNumberManager;
        }


        public ActionResult EditTruck(string vinNumber)
        {
            var model = new TruckViewModel();

            if (!string.IsNullOrEmpty(vinNumber))
            {
                var result = vinNumberManager.GetTruckInfo(vinNumber);
                if (result != null)
                {
                    model.ManufacturerName = result.ManufacturerName;
                    model.VinNumber = vinNumber;
                    model.Model = result.Model;
                    model.ModelYear = result.ModelYear;
                    model.TransmissionSpeeds = result.TransmissionSpeeds;
                    model.TransmissionStyle = result.TransmissionStyle;
                }

            }
            return View(model);
        }



        [HttpPost]
        public ActionResult SaveTruck(Truck truck)
        {
            if (ModelState.IsValid)
            {
                truckManager.SaveOrUpdate(truck);
                return RedirectToAction("EditTruck");
            } 
            return View(); 
        }
    }
}
