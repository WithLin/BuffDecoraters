using System;
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

        protected Object ProxyInstance => _proxyInstance;

        
        public override object Invoke(MethodInfo method, object[] parameters)
        {
            return method.Invoke(ProxyInstance, parameters);
        }

        
        public override Task InvokeAsync(MethodInfo method, object[] parameters)
        {
            return (Task)method.Invoke(ProxyInstance, parameters);
        }


        public override Task<T> InvokeAsync<T>(MethodInfo method, object[] parameters)
        {
            return (Task<T>)method.Invoke(ProxyInstance, parameters);
        }

        internal void SetProxyInstance(Object instance)
        {
            _proxyInstance = instance ?? throw new ArgumentNullException(nameof(instance));
        }
    }
}