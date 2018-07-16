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
                    Uow.Commit(); 
                }
                else
                {
                    truck = new Truck()
                    {
                        ManufacturerName = truck.ManufacturerName,
                        TruckName = truck.TruckName,
                        VinNumber = truck.VinNumber
                    };

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
