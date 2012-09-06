using System;
using System.Linq;
using Castle.DynamicProxy;
using Microsoft.Practices.ServiceLocation;

namespace Castle.Aop
{
    /// <summary />
    public class InterceptorProxy : IInterceptorProxy
    {
        /// <summary />
        public IServiceLocator Container { get; set; }

        /// <summary />
        /// <param name="invocation"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Intercept(IInvocation invocation)
        {
            if (invocation == null)
            {
                throw new ArgumentNullException("invocation");
            }

            var typeInterceptorAttributes = InterceptorHelper.CollectTypeInterceptors(invocation.TargetType);
            var methodInterceptorAttributes = InterceptorHelper.CollectMethodInterceptors(invocation.Method, invocation.MethodInvocationTarget);
            if (typeInterceptorAttributes.Any() || methodInterceptorAttributes.Any())
            {
                var typeInterceptors = typeInterceptorAttributes.OrderBy(SortOrder).Select(CreateInterceptor).Where(InterceptorExists);
                var methodInterceptors = methodInterceptorAttributes.OrderBy(SortOrder).Select(CreateInterceptor).Where(InterceptorExists);
                var interceptors = typeInterceptors.Union(methodInterceptors);
                var interceptorInvocation = new InterceptorInvocation(invocation, interceptors.ToArray());

                interceptorInvocation.Proceed();
            }
            else
            {
                invocation.Proceed();
            }
        }

        private static int SortOrder(InterceptorAttribute a)
        {
            return a.Order;
        }

        private IInterceptor CreateInterceptor(InterceptorAttribute interceptorAttribute)
        {
            if (String.IsNullOrEmpty(interceptorAttribute.InterceptorName))
            {
                return Container.GetInstance(interceptorAttribute.InterceptorType) as IInterceptor;
            }

            return Container.GetInstance(interceptorAttribute.InterceptorType, interceptorAttribute.InterceptorName) as IInterceptor;
        }

        private bool InterceptorExists(IInterceptor interceptor)
        {
            return interceptor != null;
        }
    }
}
