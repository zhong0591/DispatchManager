using DispatchManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispatchManager.Services.TruckManage
{
    public interface ITruckManager
    {
        Truck SaveOrUpdate(Truck truck);
    }
}
