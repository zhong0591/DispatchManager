using Microsoft.Owin;
using Owin;
using AutoMapper;
using DispatchManager.Data.Models;
using DispatchManager.ViewModel;

[assembly: OwinStartupAttribute(typeof(DispatchManager.Startup))]
namespace DispatchManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            Mapper.Initialize(cfg => cfg.CreateMap<TruckViewModel, Truck>());
           
        } 
    }
}
