using Autofac;
using Autofac.Core;
using Castle.DynamicProxy;

namespace Castle.Aop.Autofac
{
    public class InterceptorModule : Module
    {
        private readonly ProxyFactory _proxyFactory = new ProxyFactory(new ProxyGenerator());

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InterceptorProxy>().As<IInterceptorProxy>();
        }

        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {

            registration.Activating += RegistrationActivating;
        }

        private void RegistrationActivating(object sender, ActivatingEventArgs<object> e)
        {
            var interceptorProxy = e.Instance as IInterceptorProxy;
            if (interceptorProxy != null)
            {
                interceptorProxy.Container = new ServiceLocatorAdapter(e.Context.Resolve<IComponentContext>());
                return;
            }

            if (e.Instance is IInterceptor)
            {
                return;
            }

            if (InterceptorHelper.HasInterceptor(e.Instance.GetType()))
            {
                var proxy = e.Context.Resolve<IInterceptorProxy>();
                e.Instance = _proxyFactory.CreateProxy(e.Instance, proxy);
            }
        }
    }
}
