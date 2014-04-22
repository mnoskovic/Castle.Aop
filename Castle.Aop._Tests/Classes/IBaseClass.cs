using Castle.Aop._Tests.Interceptors;

namespace Castle.Aop._Tests.Classes
{
    [Interceptor(typeof(BaseInterfaceInterceptor))]
    public interface IBaseClass
    {
        [Interceptor(typeof(BaseInterfaceMethodInterceptor))]
        void InvokeBaseInterceptors();

        void InvokeBaseExceptionInterceptor();

        int InvokeMethodReturningValueType(int retValue);
    }
}