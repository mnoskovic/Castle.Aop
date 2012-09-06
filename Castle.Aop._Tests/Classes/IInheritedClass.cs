using Castle.Aop._Tests.Interceptors;

namespace Castle.Aop._Tests.Classes
{
    [Interceptor(typeof(InheritedInterfaceInterceptor))]
    public interface IInheritedClass
    {
        [Interceptor(typeof(InheritedInterfaceMethodInterceptor))]
        void InvokeInheritedInterceptors();

        void InvokeInheritedExceptionInterceptor();
    }
}