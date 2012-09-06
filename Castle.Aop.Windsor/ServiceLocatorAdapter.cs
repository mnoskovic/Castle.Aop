using System;
using System.Collections.Generic;
using Castle.MicroKernel;
using Microsoft.Practices.ServiceLocation;

namespace Castle.Aop.Windsor
{
    /// <summary />
    public class ServiceLocatorAdapter : ServiceLocatorImplBase
    {
        private readonly IKernel _kernel;

        /// <summary />
        /// <param name="kernel"></param>
        public ServiceLocatorAdapter(IKernel kernel)
        {
            _kernel = kernel;
        }

        /// <summary />
        /// <param name="serviceType"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        protected override object DoGetInstance(Type serviceType, string key)
        {
            return key != null
                ? _kernel.Resolve(key, serviceType)
                : _kernel.Resolve(serviceType);
        }


        /// <summary />
        /// <param name="serviceType"></param>
        /// <returns></returns>
        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return (IEnumerable<object>)_kernel.ResolveAll(serviceType);
        }
    }
}
