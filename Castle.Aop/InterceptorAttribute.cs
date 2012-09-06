using System;
using Castle.DynamicProxy;

namespace Castle.Aop
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class InterceptorAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public InterceptorAttribute(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (!typeof(IInterceptor).IsAssignableFrom(type))
            {
                throw new ArgumentOutOfRangeException("type", type, "Type has to implement Castle.DynamicProxy.IInterceptor");
            }

            InterceptorType = type;
            Order = Int32.MaxValue;
        }

        /// <summary>
        /// 
        /// </summary>
        public string InterceptorName { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Type InterceptorType { get; private set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int Order { get; set; }
    }
}