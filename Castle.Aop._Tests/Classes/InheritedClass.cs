using Castle.Aop._Tests.Interceptors;

namespace Castle.Aop._Tests.Classes
{
    [Interceptor(typeof(InheritedClassInterceptor))]
    public class InheritedClass : BaseClass, IInheritedClass
    {
        [Interceptor(typeof(InheritedClassMethodInterceptor))]
        public void InvokeInheritedInterceptors()
        {
            
        }

        [Interceptor(typeof(ExceptionMethodInterceptor))]
        public void InvokeInheritedExceptionInterceptor()
        {
            
        }
    }
}
