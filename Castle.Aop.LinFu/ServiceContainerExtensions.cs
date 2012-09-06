using System;
using LinFu.IoC;
using LinFu.IoC.Interfaces;

namespace Castle.Aop.LinFu
{
    public static class ServiceContainerExtensions
    {
        public static void InitInterceptor(this IServiceContainer serviceContainer)
        {
            if (serviceContainer == null)
            {
                throw new ArgumentNullException("serviceContainer");


            }

            var interceptorProxy = new InterceptorProxy {Container = new ServiceLocatorAdapter(serviceContainer)};
            serviceContainer.AddService(typeof(IInterceptorProxy), interceptorProxy);
            serviceContainer.PostProcessors.Add(new InterceptorPostProcessor());
        }
    }
}
