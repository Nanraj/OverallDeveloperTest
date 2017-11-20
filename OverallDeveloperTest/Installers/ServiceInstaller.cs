using Castle.MicroKernel.Registration;
using FlickrManager;
using FourSquareManager;
using OverallDeveloperTest.Domain;

namespace OverallDeveloperTest.Installers
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<IRepository>().ImplementedBy<Repository>().LifestyleTransient());
            container.Register(Component.For<ILocationRepository>().ImplementedBy<LocationRepository>().LifestyleTransient());
            container.Register(Component.For<IFlickrPhotoRepository>().ImplementedBy<FlickrPhotoRepository>().LifestyleTransient());
            container.Register(Component.For<IFlickrService>().ImplementedBy<FlickrService>().LifestyleTransient());
            container.Register(Component.For<IFourSquareService>().ImplementedBy<FourSquareService>().LifestyleTransient());
        }
    }
}