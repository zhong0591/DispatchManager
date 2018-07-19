
using DispatchManager.Data.Models;
using NUnit.Framework;
using Mock = DispatchManger.Services.Tests.Mocks;
using System.Linq;
using AutoMapper;
using DispatchManager.ViewModel;

namespace DispatchManager.Services.DispatchManage.Tests
{
    [TestFixture]
    public class DispatchManagerTests
    {

        [OneTimeSetUp]
        public void AutoMapper_SetUp()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<TruckViewModel, Truck>());
        }

        [Test]
        public void Test_Auto_Mapper()
        { 
            TruckViewModel tv = new TruckViewModel
            {
                ManufacturerName = "Manufacturer Name",
                ActiveSafetySystemNote = "Active Safety System Note",
                TruckName = "Truck Name",
                TransmissionSpeeds = "Transmission Speeds",
                TransmissionStyle = "Transmission Style",
                Odemetor = "Odemetor",
                Make = "Make"
            };
            Truck truck = Mapper.Map<Truck>(tv);

            Assert.AreEqual( truck.ManufacturerName, "Manufacturer Name");
            Assert.AreEqual(truck.ActiveSafetySystemNote, "Active Safety System Note");
            Assert.AreEqual(truck.TransmissionSpeeds, "Transmission Speeds");
            Assert.AreEqual(truck.Make, "Make");
            Assert.AreEqual(truck.TransmissionStyle, "Transmission Style");
            Assert.AreEqual(truck.TruckName, "Truck Name");
            Assert.AreEqual(truck.Odemetor, "Odemetor");
        }

        [Test]
        public void Test_Save_Truck()
        {
            IDispatchManager dm = new DispatchManager();
            Truck truck = new Truck()
            {
                Id = 7,
                TruckName = "Truck Name is not null "
            };

            var rs = dm.SaveTruck(truck);
            Assert.IsTrue(rs.Ok);

            truck = null;
            rs = dm.SaveTruck(truck);
            Assert.IsTrue((!rs.Ok) && rs.Errors.Contains("truck can not be null"));
            truck = new Truck() { };
            Assert.IsFalse(rs.Ok && rs.Errors.Contains("Truck name is required!"));
        }

        [Test]
        public void Test_Search_Truck_From_Vin_Number()
        {
            IDispatchManager dm = new DispatchManager();
            Assert.IsNull(dm.SearchTruck(new Mock.TruckAPI(), string.Empty));
            Assert.AreEqual(dm.SearchTruck(new Mock.TruckAPI(), "abc").ErrorCode, "7 - Manufacturer is not registered with NHTSA for sale or importation in the U.S. for use on U.S roads; Please contact the manufacturer directly for more information.");
            Assert.AreEqual(dm.SearchTruck(new Mock.TruckAPI(), "5UXWX7C5*BA").ErrorCode, "6 - Incomplete VIN.");
        }

        [Test]
        public void Test_Search_Multi_Truck_From_Vin_Number_String()
        {
            IDispatchManager dm = new DispatchManager();
            Assert.IsNull(dm.SearchMultiTruck(new Mock.TruckAPI(), string.Empty));

            string errorCode = "abc";
            Assert.IsNotNull(dm.SearchMultiTruck(new Mock.TruckAPI(), errorCode)[0]);

            string moreThanFive = "ONE|TWO|THREE|FOUR|FIVE|SIX";
            Assert.IsNull(dm.SearchMultiTruck(new Mock.TruckAPI(), moreThanFive));

            string correctCodes = "5UXWX7C5*BA|5UXCA7C5*BA|5UEFX7C5*BA";
            Assert.AreEqual(dm.SearchMultiTruck(new Mock.TruckAPI(), correctCodes).Count(), 3);

        }
    }
}