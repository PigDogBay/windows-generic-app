using MpdBaileyTechnology.GenericApp.Service.Implementation;
using MpdBaileyTechnology.GenericApp.Service.Services;
using MpdBaileyTechnology.Shared.WPF.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MpdBaileyTechnology.GenericApp.Service
{
    public class AppServiceFactory : IServiceFactory
    {
        public ServiceContainer CreateServices()
        {
            WPFServiceFactory wpfServiceFactory = new WPFServiceFactory();
            ServiceContainer container = wpfServiceFactory.CreateServices();
            //Add application services to the container
            container.Register<IAboutBox>(() => new AppAboutBox());
            return container;
        }
    }
}
