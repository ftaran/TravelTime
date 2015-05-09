using System;
using ApplicationServices.Interfaces;
using ApplicationServices.Services;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using IGeoliseWebApplication.Installers;

namespace IGeoliseWebApplication.App_Start
{
    public class ContainerBootstrapper : IContainerAccessor, IDisposable
    {
        readonly IWindsorContainer container;

        ContainerBootstrapper(IWindsorContainer container)
        {
            this.container = container;
        }

        public IWindsorContainer Container
        {
            get { return container; }
        }

        public static ContainerBootstrapper Bootstrap()
        {
            var container = new WindsorContainer().
                Install(FromAssembly.This(),new HttpClientInstaller());

            container.Register(Component.For(typeof(IGeoCodeService)).ImplementedBy(typeof(GeoCodeService)));
            container.Register(Component.For(typeof(ITravelTimeService)).ImplementedBy(typeof(TravelTimeService)));


            return new ContainerBootstrapper(container);
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}