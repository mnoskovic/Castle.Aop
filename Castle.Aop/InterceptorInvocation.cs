using System;
using System.Reflection;
using Castle.DynamicProxy;

namespace Castle.Aop
{
    /// <summary>
    /// 
    /// </summary>
    public class InterceptorInvocation : AbstractInvocation
    {
        private readonly IInvocation _parent;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="interceptors"></param>
        public InterceptorInvocation(IInvocation parent, IInterceptor[] interceptors)
            : base(parent.Proxy, interceptors, parent.Method, parent.Arguments)
        {
            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }

            _parent = parent;
        }

        /// <summary />
        protected override void InvokeMethodOnTarget()
        {
            ReturnValue = _parent.Method.Invoke(_parent.InvocationTarget, _parent.Arguments);
        }

        /// <summary />
        public override object InvocationTarget
        {
            get { return _parent.InvocationTarget; }
        }

        /// <summary />
        public override Type TargetType
        {
            get { return _parent.TargetType; }
        }

        /// <summary />
        public override MethodInfo MethodInvocationTarget
        {
            get { return _parent.MethodInvocationTarget; }
        }

        /// <summary />
        public new object ReturnValue
        {
            get { return _parent.ReturnValue; }
            set { _parent.ReturnValue = value; }
        }
    }
}
