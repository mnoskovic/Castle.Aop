using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;

namespace Castle.Aop.Windsor
{
    /// <summary />
    public class InterceptorFacility : AbstractFacility
    {
        /// <summary />
        protected override void Init()
        {
            var container = new ServiceLocatorAdapter(Kernel);
            var proxy = new InterceptorProxy { Container = container };

            Kernel.Register(Component.For(proxy.GetType()).Instance(proxy));
            Kernel.ComponentRegistered += ComponentRegistered;

        }

        private void ComponentRegistered(string key, IHandler handler)
        {
            if (InterceptorHelper.HasInterceptor(handler.ComponentModel.Implementation))
            {
                handler.ComponentModel.Interceptors.AddIfNotInCollection(new InterceptorReference(typeof(InterceptorProxy)));
            }
        }
    }
}
