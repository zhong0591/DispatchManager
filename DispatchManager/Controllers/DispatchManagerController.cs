using AutoMapper;
using DispatchManager.Data.Models;
using DispatchManager.Services.DispatchManage;
using DispatchManager.ViewModel;
using System.Web.Mvc;

namespace DispatchManager.Controllers
{
    public class DispatchManagerController : Controller
    {
        private IDispatchManager dispatchManager { get; set; }
        private ITruckAPI truckApi { get; set; }

        public DispatchManagerController(IDispatchManager dispatchManager, ITruckAPI truckApi)
        {
            this.dispatchManager = dispatchManager;
            this.truckApi = truckApi;

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditMultiTruck(string vinNumbers)
        {

            var trucks = dispatchManager.SearchMultiTruck(truckApi, vinNumbers);
            if (trucks == null)
            {
                return View();
            }
            return View(trucks);
        }


        [HttpPost]
        public JsonResult SaveMultiTruck(TruckViewModel tv)
        {
            if (ModelState.IsValid)
            {
                var truck = Mapper.Map<Truck>(tv);
                var result = dispatchManager.SaveTruck(truck);
                if (result.Ok)
                {
                    return new JsonResult() { Data = true };
                }
                else
                {
                    var strErr = string.Join(";", result.Errors);
                    return new JsonResult() { Data = strErr };
                }
            }
            return new JsonResult() { Data = false };
        }


        public ActionResult EditTruck(string vinNumber)
        {
            var model = new TruckViewModel();

            if (!string.IsNullOrEmpty(vinNumber))
            {
                var result = dispatchManager.SearchTruck(truckApi, vinNumber);
                if (result != null)
                {
                    model.ManufacturerName = result.ManufacturerName;
                    model.VinNumber = vinNumber;
                    model.Model = result.Model;
                    model.ModelYear = result.ModelYear;
                    model.TransmissionSpeeds = result.TransmissionSpeeds;
                    model.TransmissionStyle = result.TransmissionStyle;
                    model.Odemetor = result.Odemetor;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveTruck(TruckViewModel tv)
        {
            if (ModelState.IsValid)
            {
                var truck = Mapper.Map<Truck>(tv);
                var result = dispatchManager.SaveTruck(truck);
                if (result.Ok)
                {
                    return View("index");
                }
                else
                {
                    var strErr = string.Join(";", result.Errors);
                    ModelState.AddModelError("Error:", strErr);
                }
            }
            return View(tv);
        }
    }
}
