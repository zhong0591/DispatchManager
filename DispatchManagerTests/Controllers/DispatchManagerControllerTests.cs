using Autofac;
using Autofac.Integration.Mvc;
using DispatchManager.Data.Models;
using DispatchManager.Infrastructure.Repository;
using DispatchManager.Services.Common;
using DispatchManager.Services.DispatchManage;
using DispatchManager.ViewModel;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DispatchManager.Controllers.Tests
{
    [TestFixture]
    public class DispatchManagerControllerTests : ManagerBase
    {
        public Mock<IDispatchManager> dispatchManager;
        public Mock<ITruckAPI> truckApi;
        public DispatchManagerController ctrl;
        [SetUp]
        public void Init()
        {
            dispatchManager = new Mock<IDispatchManager>();
            truckApi = new Mock<ITruckAPI>(); 
            ctrl = new DispatchManagerController(dispatchManager.Object, truckApi.Object);
        }

        [Test]
        public void IndexTest()
        {
            var result = ctrl.Index();
            Assert.IsInstanceOf(typeof(ActionResult), result);
        }

        [Test]
        public void SaveTruckTest()
        { 
            var truck = new Truck() { TruckName = "Truck Name is BMW" };
            dispatchManager.Setup(x => x.SaveTruck(truck)).Returns(new OpResult() { Ok = true, Errors = new List<string>() { "new error" } });

            var result = ctrl.SaveTruck(truck);
            Assert.IsInstanceOf(typeof(ActionResult), result); 
        }

        [Test]
        public void SaveMultiTruckTest()
        {
            var truck = new Truck() { TruckName = "Truck Name is BMW" };
            var op = dispatchManager.Setup(x => x.SaveTruck(truck)).Returns(new OpResult() { Ok = true, Errors = new List<string>() { "new error" } });

            var result = ctrl.SaveMultiTruck(truck);
            Assert.IsInstanceOf(typeof(JsonResult), result);
            Assert.IsTrue((bool)result.Data);
        }

        [Test]
        public void EditMultiTruckTest()
        {
            string vinNumbers = string.Empty; 
            var result = ctrl.EditMultiTruck(vinNumbers); 
            Assert.IsInstanceOf(typeof(ActionResult), result); 

            vinNumbers = "ONE|TWO|THREE|FOUR|FIVE|SIX";
            result = ctrl.EditMultiTruck(vinNumbers);
            Assert.IsInstanceOf(typeof(ActionResult), result);

            vinNumbers = "5UXWX7C5*BA|5UXCA7C5*BA|5UEFX7C5*BA";
            result = ctrl.EditMultiTruck(vinNumbers);
            Assert.IsInstanceOf(typeof(ActionResult), result);
        }

        [Test]
        public void EditTruckTest()
        { 
            var result = ctrl.EditTruck(string.Empty);
            Assert.IsInstanceOf(typeof(ActionResult), result);

            var mockTruck = dispatchManager.Setup(x => x.SearchTruck(truckApi.Object, "5UXWX7C5*BA")).Returns(new Truck() { ManufacturerName = "BMW" });

            result = ctrl.EditTruck("5UXWX7C5*BA"); 
            Assert.IsInstanceOf(typeof(ActionResult), result); 
        }

        
    }
}