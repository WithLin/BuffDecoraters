using System;
using System.Reflection;
using System.Threading.Tasks;

namespace BuffDecoraters.DispatchProxy
{
    /// <summary>
    /// ref:https://github.com/NetCoreStack/DispatchProxyAsync
    /// </summary>
    public abstract class DispatchProxyAsync
    {
        internal static Object Create(Type targetType, Type proxyType)
        {
            return AsyncDispatchProxyGenerator.CreateProxyInstance(proxyType, targetType);
        }

        /// <summary>
        /// if method is sync
        /// </summary>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract object Invoke(MethodInfo method, object[] parameters);

        /// <summary>
        /// if method return type is task
        /// </summary>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract Task InvokeAsync(MethodInfo method, object[] parameters);

        /// <summary>
        ///  if method return type is task<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract Task<T> InvokeAsync<T>(MethodInfo method, object[] parameters);
    }
}