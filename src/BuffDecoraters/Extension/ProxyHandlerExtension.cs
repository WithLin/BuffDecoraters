using System;
using BuffDecoraters.DispatchProxy;
using BuffDecoraters.ProxyHandler;


namespace BuffDecoraters.Extension
{
    public static class ProxyHandlerExtension
    {
        /// <summary>
        /// decorated on the instance,it will decorated all method
        /// </summary>
        /// <typeparam name="TProxyHandler"></typeparam>
        /// <param name="handler"></param>
        /// <param name="instance">proxy instance</param>
        /// <param name="instanceBaseType">must be interface </param>
        /// <param name="initializeAction">initialize handler parameters or other action</param>
        /// <returns></returns>
        public static Object DecoratedProxy<TProxyHandler>(Type instanceBaseType, Object instance,
            Action<TProxyHandler> initializeAction = null) where TProxyHandler : MethodsHandler
        {
            if (!instanceBaseType.IsInterface)
            {
                throw new ArgumentNullException(nameof(instanceBaseType));
            }
            object proxy = DispatchProxyAsync.Create(instanceBaseType, typeof(TProxyHandler));
            ((TProxyHandler)proxy).SetProxyInstance(instance);
            initializeAction?.Invoke((TProxyHandler)proxy);
            return proxy;
        }


        /// <summary>
        ///  decorated on the instance,it will decorated all method
        /// </summary>
        /// <typeparam name="TProxyHandler"></typeparam>
        /// <typeparam name="T"> must be interface</typeparam>
        /// <param name="handler"></param>
        /// <param name="instance"></param>
        /// <param name="initializeAction"></param>
        /// <returns></returns>
        public static Object DecoratedProxy<TProxyHandler, T>(T instance,
            Action<TProxyHandler> initializeAction = null) where TProxyHandler : MethodsHandler
        {
            if (!typeof(T).IsInterface)
            {
                throw new ArgumentNullException("T must interface type");
            }
            object proxy = DispatchProxyAsync.Create(typeof(T), typeof(TProxyHandler));
            ((TProxyHandler)proxy).SetProxyInstance(instance);
            initializeAction?.Invoke((TProxyHandler)proxy);
            return proxy;
        }

    }
}