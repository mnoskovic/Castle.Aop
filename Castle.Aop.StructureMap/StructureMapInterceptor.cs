using System;
using Castle.DynamicProxy;
using StructureMap;
using StructureMap.Interceptors;
using StructureMap.ServiceLocatorAdapter;

namespace Castle.Aop.StructureMap
{
    /// <summary />
    public class StructureMapInterceptor : TypeInterceptor
    {
        private readonly IContainer _container;

        private readonly ProxyFactory _proxyFactory = new ProxyFactory(new ProxyGenerator());


        /// <summary />
        public StructureMapInterceptor()
        {
            _container = ObjectFactory.Container;
            var interceptorProxy = new InterceptorProxy { Container = new StructureMapServiceLocator(_container) };
            _container.Configure(c => c.For<IInterceptorProxy>().Use(interceptorProxy));

        }

        /// <summary />
        /// <param name="target"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public object Process(object target, IContext context)
        {
            var proxy = _container.GetInstance<IInterceptorProxy>();
            return _proxyFactory.CreateProxy(target, proxy);
        }

        /// <summary />
        /// <param name="type"></param>
        /// <returns></returns>
        public bool MatchesType(Type type)
        {
            return InterceptorHelper.HasInterceptor(type);
        }
    }
}
