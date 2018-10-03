using System;
using System.Reflection;
using System.Threading.Tasks;

namespace BuffDecoraters.DispatchProxy
{
    public abstract class DispatchProxyAsync
    {
        public static T Create<T, TProxy>(Type instanceBaseType, Type proxyType) where TProxy : DispatchProxyAsync
        {
            return (T)AsyncDispatchProxyGenerator.CreateProxyInstance(proxyType, instanceBaseType);
        }

        public abstract object Invoke(MethodInfo method, object[] args);

        public abstract Task InvokeAsync(MethodInfo method, object[] args);

        public abstract Task<T> InvokeAsyncT<T>(MethodInfo method, object[] args);
    }
}
