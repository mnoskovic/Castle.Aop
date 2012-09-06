using System;
using System.Linq;
using Castle.DynamicProxy;

namespace Castle.Aop
{
    /// <summary>
    /// Responsible for creating proxies
    /// </summary>
    public class ProxyFactory
    {
        private readonly ProxyGenerator _proxyGenerator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="proxyGenerator"></param>
        public ProxyFactory(ProxyGenerator proxyGenerator)
        {
            _proxyGenerator = proxyGenerator;
        }

        /// <summary>
        /// Returns proxy by interface or class if interface not defined
        /// </summary>
        /// <param name="target"></param>
        /// <param name="interceptorProxy"></param>
        /// <returns></returns>
        public object CreateProxy(object target, IInterceptorProxy interceptorProxy)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }
            var targetType = target.GetType();
            var targetInterfaces = targetType.GetInterfaces();

            if (targetInterfaces.Any())
            {
                var proxy = _proxyGenerator.CreateInterfaceProxyWithTargetInterface(targetInterfaces.Last(), target, interceptorProxy);
                return proxy;
            }
            else
            {
                var greediestCtor = targetType.GetConstructors().OrderBy(x => x.GetParameters().Count()).LastOrDefault();
                var ctorDummyArgs = greediestCtor == null ? new object[0] : new object[greediestCtor.GetParameters().Count()];
                var proxy = _proxyGenerator.CreateClassProxyWithTarget(targetType, target, ctorDummyArgs, interceptorProxy);
                return proxy;
            }
        }
    }
}
