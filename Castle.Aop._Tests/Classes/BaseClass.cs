using Castle.Aop._Tests.Interceptors;

namespace Castle.Aop._Tests.Classes
{
    [Interceptor(typeof(BaseClassInterceptor))]
    public class BaseClass : IBaseClass
    {
        [Interceptor(typeof(BaseClassMethodInterceptor))]
        public void InvokeBaseInterceptors()
        {

        }

        [Interceptor(typeof(ExceptionMethodInterceptor))]
        public virtual void InvokeBaseExceptionInterceptor()
        {

        }

        
    }
}