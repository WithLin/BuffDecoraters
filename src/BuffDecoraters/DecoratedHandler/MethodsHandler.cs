using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using BuffDecoraters.DispatchProxy;

namespace BuffDecoraters.DecoratedHandler
{
    /// <summary>
    /// handler for ProxyInstance all methods
    /// </summary>
    public abstract class MethodsHandler : DispatchProxyAsync
    {
        private Object _proxyInstance;

        private IEnumerable<MethodAttributeContext> _attributeContexts;

        protected Object ProxyInstance => _proxyInstance;

        protected IEnumerable<MethodAttributeContext> AttributeContexts => _attributeContexts;

        public T CreateInstance<T>(
            Type instanceBaseType, T instance, IEnumerable<MethodAttributeContext> attributeContexts)
        {
            object proxy = Create<T, MethodsHandler>(instanceBaseType, this.GetType());
            ((MethodsHandler)proxy).SetProxyInstance(instance, attributeContexts);
            return (T)proxy;
        }


        public override object Invoke(MethodInfo method, object[] parameters)
        {
            return method.Invoke(ProxyInstance, parameters);
        }

        
        public override Task InvokeAsync(MethodInfo method, object[] parameters)
        {
            return (Task)method.Invoke(ProxyInstance, parameters);
        }


        public override Task<T> InvokeAsyncT<T>(MethodInfo method, object[] parameters)
        {
            return (Task<T>)method.Invoke(ProxyInstance, parameters);
        }

        internal void SetProxyInstance(Object instance, IEnumerable<MethodAttributeContext> attributeContexts)
        {
            _proxyInstance = instance ?? throw new ArgumentNullException(nameof(instance));
            _attributeContexts = attributeContexts;
        }
    }
}