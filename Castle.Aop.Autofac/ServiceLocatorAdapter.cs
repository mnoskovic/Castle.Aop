using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Microsoft.Practices.ServiceLocation;

namespace Castle.Aop.Autofac
{
    public class ServiceLocatorAdapter : ServiceLocatorImplBase
    {
        private readonly IComponentContext _container;

        public ServiceLocatorAdapter(IComponentContext container)
        {
            _container = container;
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return key != null
                ? _container.ResolveNamed(key, serviceType)
                : _container.Resolve(serviceType);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            var enumerableType = typeof(IEnumerable<>).MakeGenericType(serviceType);
            return ((IEnumerable)_container.Resolve(enumerableType)).Cast<object>();
        }
    } 
}
