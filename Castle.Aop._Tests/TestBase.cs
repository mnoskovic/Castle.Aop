using Castle.Aop._Tests.Interceptors;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Castle.Aop._Tests
{
    public class TestBase
    {
        protected BaseClassInterceptor BaseClassInterceptor;
        protected BaseClassMethodInterceptor BaseClassMethodInterceptor;
        protected InheritedClassInterceptor InheritedClassInterceptor;
        protected InheritedClassMethodInterceptor InheritedClassMethodInterceptor;
        protected BaseInterfaceInterceptor BaseInterfaceInterceptor;
        protected BaseInterfaceMethodInterceptor BaseInterfaceMethodInterceptor;
        protected InheritedInterfaceInterceptor InheritedInterfaceInterceptor;
        protected InheritedInterfaceMethodInterceptor InheritedInterfaceMethodInterceptor;
        protected ExceptionMethodInterceptor ExceptionMethodInterceptor;
        protected BaseClassValueTypeInterceptor BaseClassValueTypeInterceptor;

        protected ClassInterceptorA ClassInterceptorA;
        protected ClassInterceptorB ClassInterceptorB;

        protected MethodInterceptorA MethodInterceptorA;
        protected MethodInterceptorB MethodInterceptorB;

        public virtual void TestInitialize()
        {
            Interceptor.Clear();

            BaseClassInterceptor = new BaseClassInterceptor();
            BaseClassMethodInterceptor = new BaseClassMethodInterceptor();
            InheritedClassInterceptor = new InheritedClassInterceptor();
            InheritedClassMethodInterceptor = new InheritedClassMethodInterceptor();
            BaseInterfaceInterceptor = new BaseInterfaceInterceptor();
            BaseInterfaceMethodInterceptor = new BaseInterfaceMethodInterceptor();
            BaseClassValueTypeInterceptor = new BaseClassValueTypeInterceptor();
            InheritedInterfaceInterceptor = new InheritedInterfaceInterceptor();
            InheritedInterfaceMethodInterceptor = new InheritedInterfaceMethodInterceptor();

            ExceptionMethodInterceptor = new ExceptionMethodInterceptor();

            ClassInterceptorA = new ClassInterceptorA();
            ClassInterceptorB = new ClassInterceptorB();

            MethodInterceptorA = new MethodInterceptorA();
            MethodInterceptorB = new MethodInterceptorB();
        }


        public void AssertBaseMethodInterceptorCall()
        {


            Assert.IsTrue(BaseClassInterceptor.Called(1));
            Assert.IsTrue(BaseInterfaceInterceptor.Called(0));
            Assert.IsTrue(BaseInterfaceMethodInterceptor.Called(2));
            Assert.IsTrue(BaseClassMethodInterceptor.Called(3));

            Assert.IsFalse(InheritedInterfaceInterceptor.Called());
            Assert.IsFalse(InheritedClassInterceptor.Called());
            Assert.IsFalse(InheritedInterfaceMethodInterceptor.Called());
            Assert.IsFalse(InheritedClassMethodInterceptor.Called());

        }

        public void AssertInheritedMethodInterceptorsCall()
        {
            Assert.IsTrue(BaseInterfaceInterceptor.Called(0));
            Assert.IsTrue(InheritedInterfaceInterceptor.Called(1));
            Assert.IsTrue(InheritedClassInterceptor.Called(2));
            Assert.IsTrue(BaseClassInterceptor.Called(3));
            Assert.IsTrue(InheritedInterfaceMethodInterceptor.Called(4));
            Assert.IsTrue(InheritedClassMethodInterceptor.Called(5));

            Assert.IsFalse(BaseInterfaceMethodInterceptor.Called());
            Assert.IsFalse(BaseClassMethodInterceptor.Called());
        }

        public void AssertValueTypeInterceptorCalled()
        {
            Assert.IsTrue(BaseClassValueTypeInterceptor.Called());
        }

        public void AssertOrderedInterceptorsCall()
        {
            Assert.IsTrue(ClassInterceptorB.Called(0));
            Assert.IsTrue(ClassInterceptorA.Called(1));
            Assert.IsTrue(MethodInterceptorB.Called(2));
            Assert.IsTrue(MethodInterceptorA.Called(3));
        }
    }
}



