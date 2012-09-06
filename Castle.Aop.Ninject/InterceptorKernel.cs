using System;
using System.Linq;
using Ninject;
using Ninject.Modules;

namespace Castle.Aop.Ninject
{
    public class InterceptorKernel : IDisposable
    {
        private readonly InterceptorModule _interceptor = new InterceptorModule();
        private readonly StandardKernel _kernel;

        public InterceptorKernel(params INinjectModule[] modules)
            : this(null, modules)
        {
        }

        public InterceptorKernel(INinjectSettings settings, params INinjectModule[] modules)
        {
            
            var containerModules = modules == null ? new[] { _interceptor } : modules.Union(new[] { _interceptor }).ToArray();
            _kernel = settings == null ? new StandardKernel(containerModules) : new StandardKernel(settings, containerModules);
           
            var proxy = new InterceptorProxy { Container = new ServiceLocatorAdapter(_kernel) };
            _kernel.Bind<IInterceptorProxy>().ToConstant(proxy);
        }

        public IKernel Kernel { get { return _kernel; } }

        public void Dispose()
        {
            _kernel.Dispose();
        }
    }
}
