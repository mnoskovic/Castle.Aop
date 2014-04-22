using Autofac;
using Castle.Aop._Tests;
using Castle.Aop._Tests.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Castle.Aop.Autofac.Tests
{
    [TestClass]
    public class Test : TestBase
    {
        private ContainerBuilder _builder;

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();

            _builder = new ContainerBuilder();
            _builder.RegisterModule(new InterceptorModule());

            _builder.RegisterInstance(BaseClassInterceptor);
            _builder.RegisterInstance(BaseClassMethodInterceptor);
            _builder.RegisterInstance(InheritedClassInterceptor);
            _builder.RegisterInstance(InheritedClassMethodInterceptor);
            _builder.RegisterInstance(BaseInterfaceInterceptor);
            _builder.RegisterInstance(BaseInterfaceMethodInterceptor);
            _builder.RegisterInstance(InheritedInterfaceInterceptor);
            _builder.RegisterInstance(InheritedInterfaceMethodInterceptor);
            _builder.RegisterInstance(BaseClassValueTypeInterceptor);

            _builder.RegisterInstance(ExceptionMethodInterceptor);

            _builder.RegisterInstance(ClassInterceptorA);
            _builder.RegisterInstance(ClassInterceptorB);
            _builder.RegisterInstance(MethodInterceptorA);
            _builder.RegisterInstance(MethodInterceptorB);

            _builder.RegisterType<BaseClass>().As<IBaseClass>();
            _builder.RegisterType<InheritedClass>().As<IInheritedClass>();
            _builder.RegisterType<OrderedClass>().As<IOrderedClass>();


        }

        [TestMethod]
        public void should_call_base_method_interceptors()
        {
            using (var container = _builder.Build())
            {
                var instance = container.Resolve<IBaseClass>();
                instance.InvokeBaseInterceptors();
            }

            AssertBaseMethodInterceptorCall();
        }

        [TestMethod]
        public void should_call_inherited_method_interceptors()
        {
            using (var container = _builder.Build())
            {
                var instance = container.Resolve<IInheritedClass>();
                instance.InvokeInheritedInterceptors();
            }

            AssertInheritedMethodInterceptorsCall();
        }

        [TestMethod]
        public void should_call_ordered_interceptors()
        {
            using (var container = _builder.Build())
            {
                var instance = container.Resolve<IOrderedClass>();
                instance.InvokeOrderedInterceptors();
            }

            AssertOrderedInterceptorsCall();
        }

        [TestMethod]
        [ExpectedException(typeof(InvokeException), AllowDerivedTypes = true)]
        public void should_call_base_method_with_exception_interceptor()
        {
            using (var container = _builder.Build())
            {
                var instance = container.Resolve<IBaseClass>();
                instance.InvokeBaseExceptionInterceptor();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvokeException), AllowDerivedTypes = true)]
        public void should_call_inherited_method_with_exception_interceptor()
        {
            using (var container = _builder.Build())
            {
                var instance = container.Resolve<IInheritedClass>();
                instance.InvokeInheritedExceptionInterceptor();
            }
        }

        [TestMethod]
        public void should_call_value_type_interceptor_and_return_correctly()
        {
            const int RetValue = 10;
            using (var container = _builder.Build())
            {
                var instance = container.Resolve<IBaseClass>();
                var value = instance.InvokeMethodReturningValueType(RetValue);
                
                Assert.AreEqual(RetValue, value);
            }

            AssertValueTypeInterceptorCalled();            
        }
    }
}
