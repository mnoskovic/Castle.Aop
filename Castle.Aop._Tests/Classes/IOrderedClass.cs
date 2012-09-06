using Castle.Aop._Tests.Interceptors;

namespace Castle.Aop._Tests.Classes
{
    public interface IOrderedClass
    {
        [Interceptor(typeof(MethodInterceptorA), Order = 2)]
        [Interceptor(typeof(MethodInterceptorB), Order = 1)]
        void InvokeOrderedInterceptors();
    }
}