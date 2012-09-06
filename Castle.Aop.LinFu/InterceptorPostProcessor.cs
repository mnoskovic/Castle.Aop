using Castle.DynamicProxy;
using LinFu.IoC.Interfaces;

namespace Castle.Aop.LinFu
{
    public class InterceptorPostProcessor : IPostProcessor
    {
        private readonly ProxyFactory _proxyFactory = new ProxyFactory(new ProxyGenerator());

        public void PostProcess(IServiceRequestResult result)
        {
            var instance = result.ActualResult;
            if (instance == null)
            {
                return;
            }

            // inteceptors could not be intercepted too, thus skip the code below
            if (instance is IInterceptor)
            {
                return;
            }

            if (InterceptorHelper.HasInterceptor(instance.GetType()))
            {
                var proxy = result.Container.GetService(typeof(IInterceptorProxy)) as IInterceptorProxy;
                result.ActualResult = _proxyFactory.CreateProxy(instance, proxy);
            }
        }
    }
}

