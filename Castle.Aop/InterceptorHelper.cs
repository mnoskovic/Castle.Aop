using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Castle.Aop
{
    /// <summary>
    /// 
    /// </summary>
    public static class InterceptorHelper
    {
        /// <summary />
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool HasInterceptor(Type type)
        {
            if (type == null)
            {
                return false;
            }

            var typeInterceptors = CollectTypeInterceptors(type);
            if (typeInterceptors.Any())
            {
                return true;
            }

            var targetMethods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var methodInterceptors = CollectMethodInterceptors(targetMethods);
            if (methodInterceptors.Any())
            {
                return true;
            }


            return false;
        }


        /// <summary />
        /// <param name="methods"></param>
        /// <returns></returns>
        public static IEnumerable<InterceptorAttribute> CollectMethodInterceptors(params MethodInfo[] methods)
        {
            if (methods == null)
            {
                return Enumerable.Empty<InterceptorAttribute>();
            }


            var interceptors = new List<InterceptorAttribute>();

            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes(typeof(InterceptorAttribute), true) as InterceptorAttribute[];
                if (attributes != null)
                {
                    interceptors.AddRange(attributes);
                }
            }

            return interceptors;
        }

        /// <summary />
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<InterceptorAttribute> CollectTypeInterceptors(Type type)
        {
            if (type == null)
            {
                return Enumerable.Empty<InterceptorAttribute>();
            }

            var interceptors = new List<InterceptorAttribute>();

            var interfaceTypes = type.GetInterfaces();
            foreach (var interfaceType in interfaceTypes)
            {
                var interfaceTypeInterceptors = Collect(interfaceType);
                interceptors.AddRange(interfaceTypeInterceptors);
            }

            var typeInterceptors = Collect(type);
            interceptors.AddRange(typeInterceptors);

            return interceptors;
        }

        private static IEnumerable<InterceptorAttribute> Collect(Type type)
        {

            var attributes = type.GetCustomAttributes(typeof(InterceptorAttribute), true) as InterceptorAttribute[];
            if (attributes != null)
            {
                foreach (var attribute in attributes)
                {
                    yield return attribute;
                }
            }
        }
    }
}
