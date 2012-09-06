using System;
using System.Collections.Generic;
using System.Linq;
using LinFu.IoC.Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace Castle.Aop.LinFu
{
    public sealed class ServiceLocatorAdapter : ServiceLocatorImplBase
    {
        readonly IServiceContainer _container;

        public ServiceLocatorAdapter(IServiceContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            _container = container;
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return key != null
                ? _container.GetService(key, serviceType)
                : _container.GetService(serviceType);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return _container.AvailableServices
                .Where(info => serviceType == info.ServiceType && (info.ArgumentTypes == null || !info.ArgumentTypes.Any()))
                .Select(service => _container.GetService(serviceType));
        }
    }
}
