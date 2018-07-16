using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DispatchManager.Data;
using DispatchManager.Infrastructure;
using DispatchManager.Services.Common;
using DispatchManager.Data.Models;
using System.Data.SqlClient;

namespace DispatchManager.Services.TruckManage
{
    public class TruckManager : ManagerBase, ITruckManager
    {
        

        public Truck SaveOrUpdate(Truck truck)
        {

            var uow = Uow.Reposiotry<Truck>();
            try
            {
                if (truck.Id > 0)
                {
                    var existedTruck = uow.Get(truck.Id);
                    existedTruck.ManufacturerName = truck.ManufacturerName;
                    existedTruck.TruckName = truck.TruckName;
                    existedTruck.VinNumber = truck.VinNumber;
                    existedTruck.Make = truck.Make;
                    existedTruck.Model = truck.Model;
                    existedTruck.ModelYear = truck.ModelYear;
                    existedTruck.EngineModel = truck.EngineModel;
                    existedTruck.TransmissionSpeeds = truck.TransmissionSpeeds;
                    existedTruck.Capacity = truck.Capacity;
                    existedTruck.TransmissionStyle = truck.TransmissionStyle;
                    existedTruck.Odemetor = truck.Odemetor;
                    existedTruck.VehicleType = truck.VehicleType;  
                    Uow.Commit(); 
                }
                else
                {
                    truck = new Truck()
                    {
                        ManufacturerName = truck.ManufacturerName,
                        TruckName = truck.TruckName,
                        VinNumber = truck.VinNumber,
                        Make = truck.Make,
                        Model = truck.Model,
                        ModelYear = truck.ModelYear,
                        EngineModel = truck.EngineModel,
                        TransmissionSpeeds = truck.TransmissionSpeeds,
                        Capacity = truck.Capacity,
                        TransmissionStyle = truck.TransmissionStyle,
                        Odemetor = truck.Odemetor,
                        VehicleType = truck.VehicleType,
                    };
                    uow.Add(truck);
                    Uow.Commit();
                    return truck;
                }
            }
            catch (SqlException se)
            {
                Console.WriteLine(se.Message);
            }
            return truck;
        }
    }
}
