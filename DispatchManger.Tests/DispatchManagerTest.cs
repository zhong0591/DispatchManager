using System; 
using NUnit.Framework;
using DispatchManager.Controllers;
using Moq;
using DispatchManager.Services.TruckManage;
using DispatchManager.Data.Models;
using System.Web.Mvc;

namespace DispatchManger.Tests
{
    [TestFixture]
    public class DispatchManagerTest
    {
        public Truck truck;

        [SetUp]
        public void InitData() {
            truck = new Truck()
            {
                ManufacturerName = "  Manufacturer Name",
                TruckName = "Turck Name",
                VinNumber = "Vin Number"
            };

        }

        [Test]
        public void Test_Save_Truck_Return_ActionResult()
        {

            var moq = new Mock<ITruckManager>();
            moq.Setup(q => q.SaveOrUpdate(truck));
            ITruckManager truckInfoManager = moq.Object; 

            //Arrange
              DispatchManagerController dmc = new DispatchManagerController(truckInfoManager);

            //Act
            ActionResult result = dmc.SaveTruck(truck); 
             
            //Assert
            Assert.IsInstanceOf(typeof(ActionResult), result); 
        }


        [Test]
        public void Test_Save_Truck_Return_Not_Null()
        {

            var moq = new Mock<ITruckManager>();
            moq.Setup(q => q.SaveOrUpdate(truck));
            ITruckManager truckInfoManager = moq.Object;

            //Arrange
            DispatchManagerController dmc = new DispatchManagerController(truckInfoManager);

            //Act
            ActionResult result = dmc.SaveTruck(truck); 

            //Assert
            Assert.NotNull(result);
        }
    }
}
