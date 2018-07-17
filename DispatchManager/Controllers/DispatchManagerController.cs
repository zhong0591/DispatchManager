using DispatchManager.Data.Models;
using DispatchManager.Infrastructure.Repository;
using DispatchManager.Services.DispatchManage;
using DispatchManager.ViewModel;
using System.Web.Mvc;

namespace DispatchManager.Controllers
{
    public class DispatchManagerController : Controller
    { 
        private IDispatchManager dispatchManager { get; set; }
        private ITruckAPI truckApi { get; set; } 
        private IUnitOfWork uow { get; set; }
        public DispatchManagerController(IDispatchManager dispatchManager, ITruckAPI truckApi,IUnitOfWork uow)
        {
            this.dispatchManager = dispatchManager;
            this.truckApi = truckApi;
            this.uow = uow;
        }

        public ActionResult EditMultiTruck() {

            return View();
        }
        

        public ActionResult EditTruck(string vinNumber)
        {
            var model = new TruckViewModel();

            if (!string.IsNullOrEmpty(vinNumber))
            {
                var result = Search(vinNumber);
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
        public ActionResult SaveTruck(Truck truck)
        {
            if (ModelState.IsValid)
            {
                dispatchManager.SaveTruck(uow, truck);
                return RedirectToAction("EditTruck");
            } 
            return View(truck); 
        }

        public Truck Search(string vinNbr)
        {
           return dispatchManager.SearchTruck(truckApi, vinNbr);
            
        }
    }
}
