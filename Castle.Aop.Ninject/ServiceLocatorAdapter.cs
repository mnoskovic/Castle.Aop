using System;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using Ninject;

namespace Castle.Aop.Ninject
{
    public class ServiceLocatorAdapter : ServiceLocatorImplBase
    {
        private readonly IKernel _kernel;

        public ServiceLocatorAdapter(IKernel kernel)
        {
            _kernel = kernel;
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            if (String.IsNullOrEmpty(key))
            {
                return _kernel.Get(serviceType);
            }
            return _kernel.Get(serviceType, key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
    }
}

