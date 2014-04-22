

using Castle.Aop._Tests;
using Castle.Aop._Tests.Classes;
using Castle.Aop._Tests.Interceptors;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Castle.Aop.Windsor.Tests
{
    [TestClass]
    public class Test: TestBase
    {
        private WindsorContainer _container;

        [TestInitialize]
        public override void TestInitialize()
        {
        
            base.TestInitialize();


            _container = new WindsorContainer();
            _container.AddFacility<InterceptorFacility>();

            _container.Register(Component.For(typeof(BaseClassInterceptor)).Instance(BaseClassInterceptor));
            _container.Register(Component.For(typeof(BaseClassMethodInterceptor)).Instance(BaseClassMethodInterceptor));
            _container.Register(Component.For(typeof(InheritedClassInterceptor)).Instance(InheritedClassInterceptor));
            _container.Register(Component.For(typeof(InheritedClassMethodInterceptor)).Instance(InheritedClassMethodInterceptor));
            _container.Register(Component.For(typeof(BaseInterfaceInterceptor)).Instance(BaseInterfaceInterceptor));
            _container.Register(Component.For(typeof(BaseInterfaceMethodInterceptor)).Instance(BaseInterfaceMethodInterceptor));
            _container.Register(Component.For(typeof(InheritedInterfaceInterceptor)).Instance(InheritedInterfaceInterceptor));
            _container.Register(Component.For(typeof(InheritedInterfaceMethodInterceptor)).Instance(InheritedInterfaceMethodInterceptor));

            _container.Register(Component.For(typeof(ExceptionMethodInterceptor)).Instance(ExceptionMethodInterceptor));


            _container.Register(Component.For(typeof(ClassInterceptorA)).Instance(ClassInterceptorA));
            _container.Register(Component.For(typeof(ClassInterceptorB)).Instance(ClassInterceptorB));
            _container.Register(Component.For(typeof(MethodInterceptorA)).Instance(MethodInterceptorA));
            _container.Register(Component.For(typeof(MethodInterceptorB)).Instance(MethodInterceptorB));

            _container.Register(Component.For(typeof(IBaseClass)).ImplementedBy(typeof(BaseClass)));
            _container.Register(Component.For(typeof(IInheritedClass)).ImplementedBy(typeof(InheritedClass)));
            _container.Register(Component.For(typeof(IOrderedClass)).ImplementedBy(typeof(OrderedClass)));

            _container.Register(Component.For<BaseClassValueTypeInterceptor>().Instance(BaseClassValueTypeInterceptor));

        }


        [TestMethod]
        public void should_call_base_method_interceptors()
        {
            var instance = _container.Resolve<IBaseClass>();
            instance.InvokeBaseInterceptors();

           AssertBaseMethodInterceptorCall();
        }

        [TestMethod]
        public void should_call_inherited_method_interceptors()
        {
            var instance = _container.Resolve<IInheritedClass>();
            instance.InvokeInheritedInterceptors();
            
            AssertInheritedMethodInterceptorsCall();
        }

        [TestMethod]
        public void should_call_ordered_method_interceptors()
        {
            var instance = _container.Resolve<IOrderedClass>();
            instance.InvokeOrderedInterceptors();

            AssertOrderedInterceptorsCall();
        }

        [TestMethod]
        [ExpectedException(typeof(InvokeException), AllowDerivedTypes = true)]
        public void should_call_base_method_with_exception_interceptor()
        {
            var instance = _container.Resolve<IBaseClass>();
            instance.InvokeBaseExceptionInterceptor();

        }

        [TestMethod]
        [ExpectedException(typeof(InvokeException), AllowDerivedTypes = true)]
        public void should_call_inherited_method_with_exception_interceptor()
        {
            var instance = _container.Resolve<IInheritedClass>();
            instance.InvokeInheritedExceptionInterceptor();
        }

        [TestMethod]
        public void should_call_value_type_interceptor_and_return_correctly()
        {
            const int RetValue = 10;
            var instance = _container.Resolve<IBaseClass>();
            var value = instance.InvokeMethodReturningValueType(RetValue);

            AssertValueTypeInterceptorCalled();
            Assert.AreEqual(RetValue, value);
        }
    }
}



