using System;
using BuffDecoraters.DecoratedHandler;
using BuffDecoraters.DispatchProxy;


namespace BuffDecoraters.Extension
{
    public static class DecoratedHandlerExtension
    {
        /// <summary>
        /// decorated instance and return 
        /// </summary>
        /// <typeparam name="TProxyHandler"></typeparam>
        /// <param name="instance"> will be decorated,must be as interface</param>
        /// <param name="instanceBaseType">must be interface </param>
        /// <param name="initializeAction">initialize handler parameters or other action</param>
        /// <returns></returns>
        public static Object GetDecoratedProxy<TProxyHandler>(Type instanceBaseType, Object instance,
            Action<TProxyHandler> initializeAction = null) where TProxyHandler : MethodsHandler
        {
            object proxy = DispatchProxyAsync.Create(instanceBaseType, typeof(TProxyHandler));
            ((TProxyHandler)proxy).SetProxyInstance(instance);
            initializeAction?.Invoke((TProxyHandler)proxy);
            return proxy;
        }


        /// <summary>
        ///  decorated instance and return 
        /// </summary>
        /// <typeparam name="TProxyHandler"></typeparam>
        /// <typeparam name="T">must be as interface</typeparam>
        /// <param name="instance"> will be decorated </param>
        /// <param name="initializeAction"> initialize handler parameters or other action </param>
        /// <returns></returns>
        public static Object GetDecoratedProxy<TProxyHandler, T>(T instance, Action<TProxyHandler> initializeAction = null)
            where TProxyHandler : MethodsHandler
        {
            object proxy = DispatchProxyAsync.Create(typeof(T), typeof(TProxyHandler));
            ((TProxyHandler)proxy).SetProxyInstance(instance);
            initializeAction?.Invoke((TProxyHandler)proxy);
            return proxy;
        }

        /// <summary>
        /// decorated instance and return 
        /// </summary>
        /// <param name="instanceType">must be interface</param>
        /// <param name="instance"></param>
        /// <param name="proxyHandlerType"></param>
        /// <param name="initializeAction"> initialize handler parameters or other action </param>
        /// <returns></returns>
        public static Object GetDecoratedProxy(Type instanceType, Object instance, Type proxyHandlerType, 
            Action<Object> initializeAction = null)
        {
            if (!(proxyHandlerType.IsSubclassOf(typeof(MethodsHandler))))
            {
                throw new ArgumentException(nameof(proxyHandlerType));
            }

            object proxy = DispatchProxyAsync.Create(instanceType, proxyHandlerType);
            ((MethodsHandler)proxy).SetProxyInstance(instance);
            initializeAction?.Invoke(proxy);
            return proxy;
        }

    }
}