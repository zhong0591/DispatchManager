using DispatchManager.Data.Models;
using DispatchManager.Infrastructure.Repository;
using System.Collections.Generic;

namespace DispatchManager.Services.DispatchManage
{
    public interface IDispatchManager
    {
        Truck SearchTruck(ITruckAPI truckAPI, string vinNbr);

        List<Truck> SearchMultiTruck(ITruckAPI truckAPI, string vinNbrs);

        OpResult SaveTruck(Truck truck);

        OpResult SaveTruckViewModel(int truckId);
    }
}
