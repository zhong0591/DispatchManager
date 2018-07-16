using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DispatchManager.Data.Models
{
    public class Truck
    {
        public int Id { get; set; }
        public string TruckName { get; set; }
        public string ManufacturerName { get; set; }
        public string VinNumber { get; set; }
    }
}