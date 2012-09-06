using Castle.Aop._Tests.Interceptors;

namespace Castle.Aop._Tests.Classes
{
    [Interceptor(typeof(ClassInterceptorA), Order = 2)]
    [Interceptor(typeof(ClassInterceptorB), Order = 1)]
    public class OrderedClass : IOrderedClass
    {
        [Interceptor(typeof(MethodInterceptorA), Order = 2)]
        [Interceptor(typeof(MethodInterceptorB), Order = 1)]
        public void InvokeOrderedInterceptors()
        {

        }
    }
}
