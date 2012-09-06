using Castle.Aop._Tests;
using Castle.Aop._Tests.Classes;
using Castle.Aop._Tests.Interceptors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Castle.Aop.StructureMap.Tests
{


    [TestClass]
    public class Test : TestBase
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();

            ObjectFactory.Initialize(x => { });
            ObjectFactory.Configure(c => c.RegisterInterceptor(new StructureMapInterceptor()));

            ObjectFactory.Configure(c => c.For<BaseClassInterceptor>().Use(BaseClassInterceptor));
            ObjectFactory.Configure(c => c.For<BaseClassMethodInterceptor>().Use(BaseClassMethodInterceptor));

            ObjectFactory.Configure(c => c.For<InheritedClassInterceptor>().Use(InheritedClassInterceptor));
            ObjectFactory.Configure(c => c.For<InheritedClassMethodInterceptor>().Use(InheritedClassMethodInterceptor));

            ObjectFactory.Configure(c => c.For<BaseInterfaceInterceptor>().Use(BaseInterfaceInterceptor));
            ObjectFactory.Configure(c => c.For<BaseInterfaceMethodInterceptor>().Use(BaseInterfaceMethodInterceptor));

            ObjectFactory.Configure(c => c.For<InheritedInterfaceInterceptor>().Use(InheritedInterfaceInterceptor));
            ObjectFactory.Configure(c => c.For<InheritedInterfaceMethodInterceptor>().Use(InheritedInterfaceMethodInterceptor));

            ObjectFactory.Configure(c => c.For<ExceptionMethodInterceptor>().Use(new ExceptionMethodInterceptor()));

            ObjectFactory.Configure(c => c.For<ClassInterceptorA>().Use(ClassInterceptorA));
            ObjectFactory.Configure(c => c.For<ClassInterceptorB>().Use(ClassInterceptorB));
            ObjectFactory.Configure(c => c.For<MethodInterceptorA>().Use(MethodInterceptorA));
            ObjectFactory.Configure(c => c.For<MethodInterceptorB>().Use(MethodInterceptorB));

            ObjectFactory.Configure(c => c.For<IBaseClass>().Use<BaseClass>());
            ObjectFactory.Configure(c => c.For<IInheritedClass>().Use<InheritedClass>());
            ObjectFactory.Configure(c => c.For<IOrderedClass>().Use<OrderedClass>());
        }


        [TestMethod]
        public void should_call_base_method_interceptors()
        {
            var instance = ObjectFactory.GetInstance<IBaseClass>();
            instance.InvokeBaseInterceptors();

            AssertBaseMethodInterceptorCall();
        }

        [TestMethod]
        public void should_call_inherited_method_interceptors()
        {
            var instance = ObjectFactory.GetInstance<IInheritedClass>();
            instance.InvokeInheritedInterceptors();

            AssertInheritedMethodInterceptorsCall();
        }


        [TestMethod]
        public void should_call_ordered_interceptors()
        {
            var instance = ObjectFactory.GetInstance<IOrderedClass>();
            instance.InvokeOrderedInterceptors();

            AssertOrderedInterceptorsCall();
        }

        [TestMethod]
        [ExpectedException(typeof(InvokeException), AllowDerivedTypes = true)]
        public void should_call_base_method_with_exception_interceptor()
        {
            var instance = ObjectFactory.GetInstance<IBaseClass>();
            instance.InvokeBaseExceptionInterceptor();
        }

        [TestMethod]
        [ExpectedException(typeof(InvokeException), AllowDerivedTypes = true)]
        public void should_call_inherited_method_with_exception_interceptor()
        {
            var instance = ObjectFactory.GetInstance<IInheritedClass>();
            instance.InvokeInheritedExceptionInterceptor();
        }
    }
}

