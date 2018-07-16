
 using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace DispatchManager.Data.Models
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DispatchManagerContext", throwIfV1Schema: false)
        {
        }

        public DbSet<TruckImage> TruckImage { get; set; }
        public DbSet<Truck> Truck{ get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
     
}
