using AlmacenVinos.Infraestructure.Interfaces;
using AlmacenVinos.Infraestructure.Repositories;
using Unity;

namespace AlmacenVinos.Services
{
    public static class ServiceUnityConfig
    {
        public static void RegisterComponents(UnityContainer container)
        {
            container.RegisterType<IBodegaRepository, BodegaRepository>();
        }
    }
}
