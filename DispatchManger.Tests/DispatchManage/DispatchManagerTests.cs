
using DispatchManager.Data.Models;
using NUnit.Framework;
using Mock = DispatchManger.Services.Tests.Mocks;
using System.Linq;
namespace DispatchManager.Services.DispatchManage.Tests
{
    [TestFixture]
    public class DispatchManagerTests
    { 
        [Test]
        public void SaveTruckTest()
        {
            IDispatchManager dm = new DispatchManager();
            Truck truck = new  Truck()
            {
                TruckName = "Truck Name is not null "
            };
           
            var rs = dm.SaveTruck( truck); 
            Assert.IsTrue(rs.Ok);

            truck = null; 
            rs = dm.SaveTruck( truck);
            Assert.IsTrue((!rs.Ok) && rs.Errors.Contains("truck can not be null"));
            truck = new Truck() { };
            Assert.IsFalse(rs.Ok && rs.Errors.Contains("Truck name is required!"));
        }

        [Test]
        public void SearchTruckTest()
        {
            IDispatchManager dm = new DispatchManager();
            Assert.IsNull(dm.SearchTruck(new Mock.TruckAPI(), string.Empty));
            Assert.AreEqual(dm.SearchTruck(new Mock.TruckAPI(), "abc").ErrorCode, "7 - Manufacturer is not registered with NHTSA for sale or importation in the U.S. for use on U.S roads; Please contact the manufacturer directly for more information.");
            Assert.AreEqual(dm.SearchTruck(new Mock.TruckAPI(), "5UXWX7C5*BA").ErrorCode, "6 - Incomplete VIN.");
        }

        [Test]
        public void SearchMultiTruckTest()
        {
            IDispatchManager dm = new DispatchManager();
            Assert.IsNull(dm.SearchMultiTruck(new Mock.TruckAPI(), string.Empty));

            string errorCode = "abc";
            Assert.IsNotNull(dm.SearchMultiTruck(new Mock.TruckAPI(), errorCode)[0]);

            string moreThanFive = "ONE|TWO|THREE|FOUR|FIVE|SIX";
            Assert.IsNull(dm.SearchMultiTruck(new Mock.TruckAPI(),moreThanFive));

            string correctCodes = "5UXWX7C5*BA|5UXCA7C5*BA|5UEFX7C5*BA";
            Assert.AreEqual(dm.SearchMultiTruck(new Mock.TruckAPI(), correctCodes).Count(),3); 
           
        }
    }
}