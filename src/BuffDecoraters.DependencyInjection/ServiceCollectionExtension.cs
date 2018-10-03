using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BuffDecoraters.DecoratedHandler;
using BuffDecoraters.DispatchProxy;
using Microsoft.Extensions.DependencyInjection;
using BuffDecoraters.Extension;

namespace BuffDecoraters.DependencyInjection
{
    public static class ServiceCollectionExtension
    {

        public static IServiceCollection AddDecoratedProxy(this IServiceCollection services,
            Type serviceType,
            Type proxyHandlerType)
        {
            services.Decorate(serviceType, 
                (inner, provider) =>
                    DecoratedHandlerExtension.GetDecoratedProxy(serviceType, inner, proxyHandlerType));
            return services;
        }


        public static IServiceCollection AddMethodAttributeDecorated(this IServiceCollection services,
            Type attibuteType, Assembly decoratedAssembly, Assembly handlerAssembly)
        {
            var serviceTypeContexts = GetServiceTypeContexts(services, decoratedAssembly, attibuteType).ToList();
            if (serviceTypeContexts.Any())
            {
                var handlerTypes = handlerAssembly.GetTypes()
                    .Where(HandlerFilter(attibuteType)).ToList();
                foreach (var ctx in serviceTypeContexts)
                {
                    foreach (var handlerType in handlerTypes)
                    {
                        services.AddDecoratedProxy(ctx.Type, handlerType);
                    }
                }
            }
            return services;
        }



        private static IEnumerable<ServiceTypeContext> GetServiceTypeContexts(IServiceCollection services, Assembly decoratedAssembly, Type attributeType)
        {
            var result = new List<ServiceTypeContext>();

            var scopeServiceDesc = services.Where(i =>i.ServiceType.IsInterface&&
                   (i.ServiceType.Assembly == decoratedAssembly|| i.ImplementationType != null && i.ImplementationType.Assembly == decoratedAssembly)).ToList();

            foreach (var scope in scopeServiceDesc)
            {
                var scopeContext = ServiceTypeContextFifter(scope.ServiceType, scope.ImplementationType, attributeType);
                if (scopeContext != null)
                {
                    result.Add(scopeContext);
                }
            }
            return result;
        }


        private static List<ServiceTypeContext> ServiceTypeContextListCache = new List<ServiceTypeContext>();



        private static ServiceTypeContext ServiceTypeContextFifter(Type serviceType, Type implementationType, Type attributeType)
        {
            ServiceTypeContext result = ServiceTypeContextListCache.FirstOrDefault(i => i.Type == serviceType);
            Boolean isContext = true;
            if (result == null)
            {
                isContext = false;
                result = new ServiceTypeContext(serviceType);
                isContext = TryAddMethodAttributeContext(serviceType, attributeType, result, isContext);
                if (implementationType != null)
                {
                    isContext= TryAddMethodAttributeContext
                        (implementationType, attributeType, result, isContext);
                }
                if (isContext)
                {
                    ServiceTypeContextListCache.Add(result);
                }
            }
            return result;
        }


        private static bool TryAddMethodAttributeContext
            (Type serviceType, Type attributeType, ServiceTypeContext result, bool isContext)
        {
            foreach (var method in serviceType.GetMethods())
            {
                foreach (var attribute in method.GetCustomAttributes())
                {
                    if (attribute.GetType() == attributeType)
                    {
                        isContext = true;
                    }
                    result.AddContext(new MethodAttributeContext(method, attribute));
                }
            }

            return isContext;
        }



        private static Func<Type, bool> HandlerFilter(Type attibuteType)
        {
            return t =>   !t.IsAbstract 
                        && t.BaseType != null 
                        && t.BaseType.IsGenericType
                        && t.IsSubclassOf(typeof(DispatchProxyAsync)) 
                        && t.BaseType.GetTypeInfo().GenericTypeArguments[0] == attibuteType;
        }

    }
}