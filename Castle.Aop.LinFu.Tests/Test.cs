using Castle.Aop._Tests;
using Castle.Aop._Tests.Classes;
using LinFu.IoC;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Castle.Aop.LinFu.Tests
{
    [TestClass]
    public class Test:TestBase
    {
        private ServiceContainer _container;

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();

            _container = new ServiceContainer();
            _container.InitInterceptor();

            _container.AddService(BaseClassInterceptor);
            _container.AddService(BaseClassMethodInterceptor);
            _container.AddService(InheritedClassInterceptor);
            _container.AddService(InheritedClassMethodInterceptor);
            _container.AddService(BaseInterfaceInterceptor);
            _container.AddService(BaseInterfaceMethodInterceptor);
            _container.AddService(InheritedInterfaceInterceptor);
            _container.AddService(InheritedInterfaceMethodInterceptor);

            _container.AddService(ExceptionMethodInterceptor);

            _container.AddService(ClassInterceptorA);
            _container.AddService(ClassInterceptorB);
            _container.AddService(MethodInterceptorA);
            _container.AddService(MethodInterceptorB);

            _container.AddService(typeof(IBaseClass), typeof(BaseClass));
            _container.AddService(typeof(IInheritedClass), typeof(InheritedClass));
            _container.AddService(typeof(IOrderedClass), typeof(OrderedClass));
        }

        [TestMethod]
        public void should_call_base_method_interceptors()
        {
            
            var instance = _container.GetService<IBaseClass>();
            instance.InvokeBaseInterceptors();

            AssertBaseMethodInterceptorCall();
        }

        [TestMethod]
        public void should_call_inherited_method_interceptors()
        {

            var instance = _container.GetService<IInheritedClass>();
            instance.InvokeInheritedInterceptors();

            AssertInheritedMethodInterceptorsCall();
        }

        [TestMethod]
        public void should_call_ordered_method_interceptors()
        {

            var instance = _container.GetService<IOrderedClass>();
            instance.InvokeOrderedInterceptors();

            AssertOrderedInterceptorsCall();
        }

        [TestMethod]
        [ExpectedException(typeof(InvokeException), AllowDerivedTypes = true)]
        public void should_call_base_method_with_exception_interceptor()
        {
            var instance = _container.GetService<IBaseClass>();
            instance.InvokeBaseExceptionInterceptor();

        }

        [TestMethod]
        [ExpectedException(typeof(InvokeException), AllowDerivedTypes = true)]
        public void should_call_inherited_method_with_exception_interceptor()
        {
            var instance = _container.GetService<IInheritedClass>();
            instance.InvokeInheritedExceptionInterceptor();
        }
    }
}
