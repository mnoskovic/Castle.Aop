using Castle.DynamicProxy;
using Microsoft.Practices.ServiceLocation;

namespace Castle.Aop
{
    /// <summary>
    /// Interceptor proxy
    /// </summary>
    public interface IInterceptorProxy : IInterceptor
    {
        /// <summary>
        /// IoC Container reference
        /// </summary>
        IServiceLocator Container { get; set; }
    }
}