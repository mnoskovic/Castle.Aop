using System.Collections.Generic;
using Castle.DynamicProxy;

namespace Castle.Aop._Tests.Interceptors
{
    public abstract class Interceptor : IInterceptor
    {
        public static void Clear()
        {
            Indexes.Clear();
        }

        public static IDictionary<Interceptor, int> Indexes = new Dictionary<Interceptor, int>();

        public bool Called(int? index = null)
        {
            if (Indexes.ContainsKey(this))
            {
                if (index.HasValue)
                {
                    return Indexes[this] == index;
                }
                return true;
            }
            return false;
        }



        public void Intercept(IInvocation invocation)
        {
            var count = Indexes.Values.Count;
            Indexes.Add(this, count);
            invocation.Proceed();
        }
    }
}
