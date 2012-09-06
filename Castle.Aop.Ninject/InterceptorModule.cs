using Ninject.Activation.Strategies;
using Ninject.Modules;

namespace Castle.Aop.Ninject
{
    public class InterceptorModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Components.Add<IActivationStrategy, InterceptorProxyActivationStrategy>();
        }
    }
}
