using System;
using NUnit.Framework;
using DispatchManager.Controllers;
using Moq;
using DispatchManager.Services.TruckManage;
using DispatchManager.Data.Models;
using System.Web.Mvc;
using DispatchManager.Services.VinNumberManage;
using System.Threading.Tasks;
using DispatchManger.Tests.VinNumberAPI;

namespace DispatchManger.Tests
{
    [TestFixture]
    public class DispatchManagerTest
    {
        public Truck truck;
        public string vinNumber;

        public ITruckManager truckManager;
        public IVinNumberManager vinNumberManager;

        [SetUp]
        public void InitData()
        {
            truck = new Truck()
            {
                ManufacturerName = "Manufacturer Name",
                TruckName = "Turck Name",
                VinNumber = "Vin Number"
            };

            this.vinNumber = "5UXWX7C5*BA";

            var moq = new Mock<ITruckManager>();
            moq.Setup(q => q.SaveOrUpdate(truck));
            truckManager = moq.Object;

            var mov = new Mock<IVinNumberManager>();
            mov.Setup(v => v.GetTruckInfo(vinNumber));
            vinNumberManager = mov.Object;
        }


        [Test]
        public void Test_Edit_Truck_With_String_Empty_Return_ActionResult()
        {
            //Arrange
            DispatchManagerController dmc = new DispatchManagerController(truckManager, vinNumberManager);

            //Act
            ActionResult result = dmc.EditTruck(string.Empty);

            //Assert
            Assert.IsInstanceOf(typeof(ActionResult), result);
        }

        [Test]
        public void Test_Edit_Truck_With_Vin_Number_Return_ActionResult()
        {
            //Arrange
            DispatchManagerController dmc = new DispatchManagerController(truckManager, vinNumberManager);

            //Act
            ActionResult result = dmc.EditTruck(vinNumber);

            //Assert
            Assert.IsInstanceOf(typeof(ActionResult), result);
        }

        [Test]
        public void Test_Save_Truck_Return_ActionResult()
        {

            //Arrange 
            DispatchManagerController dmc = new DispatchManagerController(truckManager, vinNumberManager);

            //Act
            ActionResult result = dmc.SaveTruck(truck);

            //Assert
            Assert.IsInstanceOf(typeof(ActionResult), result);
        }

        [Test]
        public void Test_Save_Truck_Return_Not_Null()
        {

            //Arrange
            DispatchManagerController dmc = new DispatchManagerController(truckManager, vinNumberManager);

            //Act
            ActionResult result = dmc.SaveTruck(truck);

            //Assert
            Assert.NotNull(result);
        }

        [Test]
        public void Test_Save_Json_To_Truck()
        {

            //Arrange
            VinNumberManager vinNumberManager = new VinNumberManager();

            //Act
            var result = vinNumberManager.GetTruckInfo("5UXWX7C5*BA");

            //Assert
            Assert.IsNull(result);

             
        }

        [Test]
        public void Test_Get_Truck_Via_Vin_Number()
        {
            IVinNumberAPIInvoker invoker = new VinNumberAPIMock();
            VinNumberAPIManager manager = new VinNumberAPIManager(invoker); 

            Assert.AreEqual(manager.SearchTruck(string.Empty), null);
            Assert.AreEqual(manager.SearchTruck(""), null);
            Assert.AreEqual(manager.SearchTruck("abc").ErrorCode, "7 - Manufacturer is not registered with NHTSA for sale or importation in the U.S. for use on U.S roads; Please contact the manufacturer directly for more information.");
            Assert.AreEqual(manager.SearchTruck("5UXWX7C5*BA").ErrorCode, "6 - Incomplete VIN.");

        }
    } 

}
