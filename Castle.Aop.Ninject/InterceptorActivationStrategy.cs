using Castle.DynamicProxy;
using Ninject;
using Ninject.Activation;
using Ninject.Activation.Strategies;

namespace Castle.Aop.Ninject
{
    public class InterceptorProxyActivationStrategy : ActivationStrategy
    {
        private readonly ProxyFactory _proxyFactory = new ProxyFactory(new ProxyGenerator());

        public override void Activate(IContext context, InstanceReference reference)
        {
            if (reference.Instance is IInterceptorProxy)
            {
                return;
            }

            if (reference.Instance is IInterceptor)
            {
                return;
            }

            if (InterceptorHelper.HasInterceptor(reference.Instance.GetType()))
            {
                var proxy = context.Kernel.Get<IInterceptorProxy>();
                reference.Instance = _proxyFactory.CreateProxy(reference.Instance, proxy);
            }

            base.Activate(context, reference);
        }
    }
}
