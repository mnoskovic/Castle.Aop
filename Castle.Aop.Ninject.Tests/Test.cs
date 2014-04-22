using Castle.Aop._Tests;
using Castle.Aop._Tests.Classes;
using Castle.Aop._Tests.Interceptors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace Castle.Aop.Ninject.Tests
{
    [TestClass]
    public class Test: TestBase
    {
        private InterceptorKernel _interceptorKernel;
     
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();

            _interceptorKernel = new InterceptorKernel();

            _interceptorKernel.Kernel.Bind<BaseClassInterceptor>().ToConstant(BaseClassInterceptor);
            _interceptorKernel.Kernel.Bind<BaseClassMethodInterceptor>().ToConstant(BaseClassMethodInterceptor);
            _interceptorKernel.Kernel.Bind<BaseClassValueTypeInterceptor>().ToConstant(BaseClassValueTypeInterceptor);
            _interceptorKernel.Kernel.Bind<InheritedClassInterceptor>().ToConstant(InheritedClassInterceptor);
            _interceptorKernel.Kernel.Bind<InheritedClassMethodInterceptor>().ToConstant(InheritedClassMethodInterceptor);
            _interceptorKernel.Kernel.Bind<BaseInterfaceInterceptor>().ToConstant(BaseInterfaceInterceptor);
            _interceptorKernel.Kernel.Bind<BaseInterfaceMethodInterceptor>().ToConstant(BaseInterfaceMethodInterceptor);
            _interceptorKernel.Kernel.Bind<InheritedInterfaceInterceptor>().ToConstant(InheritedInterfaceInterceptor);
            _interceptorKernel.Kernel.Bind<InheritedInterfaceMethodInterceptor>().ToConstant(InheritedInterfaceMethodInterceptor);

            _interceptorKernel.Kernel.Bind<ExceptionMethodInterceptor>().ToConstant(ExceptionMethodInterceptor);

            _interceptorKernel.Kernel.Bind<ClassInterceptorA>().ToConstant(ClassInterceptorA);
            _interceptorKernel.Kernel.Bind<ClassInterceptorB>().ToConstant(ClassInterceptorB);
            _interceptorKernel.Kernel.Bind<MethodInterceptorA>().ToConstant(MethodInterceptorA);
            _interceptorKernel.Kernel.Bind<MethodInterceptorB>().ToConstant(MethodInterceptorB);


            _interceptorKernel.Kernel.Bind<IBaseClass>().To<BaseClass>();
            _interceptorKernel.Kernel.Bind<IInheritedClass>().To<InheritedClass>();
            _interceptorKernel.Kernel.Bind<IOrderedClass>().To<OrderedClass>();         

        }

        [TestMethod]
        public void should_call_base_method_interceptors()
        {
            using (var kernel = _interceptorKernel.Kernel)
            {
                var instance = kernel.Get<IBaseClass>();
                instance.InvokeBaseInterceptors();
            }

           AssertBaseMethodInterceptorCall();

        }

        [TestMethod]
        public void should_call_inherited_method_interceptors()
        {
            using (var kernel = _interceptorKernel.Kernel)
            {
                var instance = kernel.Get<IInheritedClass>();
                instance.InvokeInheritedInterceptors();
            }

            AssertInheritedMethodInterceptorsCall();
        }

        [TestMethod]
        public void should_call_ordered_method_interceptors()
        {
            using (var kernel = _interceptorKernel.Kernel)
            {
                var instance = kernel.Get<IOrderedClass>();
                instance.InvokeOrderedInterceptors();
            }

            AssertOrderedInterceptorsCall();
        }


        [TestMethod]
        [ExpectedException(typeof(InvokeException), AllowDerivedTypes = true)]
        public void should_call_base_method_with_exception_interceptor()
        {
            using (var kernel = _interceptorKernel.Kernel)
            {
                var instance = kernel.Get<IBaseClass>();
                instance.InvokeBaseExceptionInterceptor();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvokeException), AllowDerivedTypes = true)]
        public void should_call_inherited_method_with_exception_interceptor()
        {
            using (var kernel = _interceptorKernel.Kernel)
            {
                var instance = kernel.Get<IInheritedClass>();
                instance.InvokeInheritedExceptionInterceptor();
            }
        }

        [TestMethod]
        public void should_call_value_type_interceptor_and_return_correctly()
        {
            const int RetValue = 10;
            using (var kernel = _interceptorKernel.Kernel)
            {
                var instance = kernel.Get<IBaseClass>();
                var value = instance.InvokeMethodReturningValueType(RetValue);

                Assert.AreEqual(RetValue, value);
            }

            AssertValueTypeInterceptorCalled();
        }
    }
}
