using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
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

        [OneTimeSetUp]
        public void AutoMapper_SetUp() {
            Mapper.Initialize(cfg => cfg.CreateMap<TruckViewModel, Truck>());
        }

        [SetUp]
        public void DispatchManager_SetUp()
        {
            dispatchManager = new Mock<IDispatchManager>();
            truckApi = new Mock<ITruckAPI>(); 
            ctrl = new DispatchManagerController(dispatchManager.Object, truckApi.Object); 
        }

        [Test]
        public void Test_Index()
        {
            var result = ctrl.Index();
            Assert.IsInstanceOf(typeof(ActionResult), result);
        }

        [Test]
        public void Test_Save_Truck_Return_View()
        {
           
            dispatchManager.Setup(foo => foo.SaveTruck(It.IsAny<Truck>())).Returns(new OpResult() { Ok = true });

            var tv = new TruckViewModel() { TruckName = "New Name", ActiveSafetySystemNote = "Active Safety System Note" };           
            var result = ctrl.SaveTruck(tv);
            Assert.IsInstanceOf(typeof(ActionResult), result); 
        }

        [Test]
        public void Test_Save_Multiple_Truck_Return_View()
        {
        
            dispatchManager.Setup(foo => foo.SaveTruck(It.IsAny<Truck>())).Returns(new OpResult() { Ok = true});
            var tv = new TruckViewModel() { TruckName = "New Name", ActiveSafetySystemNote = "Active Safety System Note" };

            var result = ctrl.SaveMultiTruck(tv);
            Assert.IsInstanceOf(typeof(JsonResult), result);
            Assert.IsTrue((bool)result.Data);
        }

        [Test]
        public void Test_Edit_Multiple_Truck_Return_View()
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
        public void Test_Edit_Truck_Return_View()
        { 
            var result = ctrl.EditTruck(string.Empty);
            Assert.IsInstanceOf(typeof(ActionResult), result);

            var mockTruck = dispatchManager.Setup(x => x.SearchTruck(truckApi.Object, "5UXWX7C5*BA")).Returns(new Truck() { ManufacturerName = "BMW" });

            result = ctrl.EditTruck("5UXWX7C5*BA"); 
            Assert.IsInstanceOf(typeof(ActionResult), result); 
        }

        
    }
}