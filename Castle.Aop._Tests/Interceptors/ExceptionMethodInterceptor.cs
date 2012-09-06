using Castle.DynamicProxy;

namespace Castle.Aop._Tests
{
    public class ExceptionMethodInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            throw new InvokeException();
        }
    }
}
