using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BuffDecoraters.Extension;

namespace BuffDecoraters.ProxyHandler
{

    /// <summary>
    /// handler for ProxyInstance attribute methods
    /// if a attribute has multiple handler or a methods has multiple proxy attribute,you should consider the context
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    public abstract class MethodAttributeHandler<TAttribute> : MethodsHandler where TAttribute : Attribute
    {
        private IEnumerable<MethodAttributeContext> Contexts { get; set; }

        /// <summary>
        /// if method is sync
        /// </summary>
        /// <returns></returns>
        public abstract object Invoke(MethodAttributeContext context);

        /// <summary>
        /// if method return type is task
        /// </summary>
        /// <returns></returns>
        public abstract Task InvokeAsync(MethodAttributeContext context);

        /// <summary>
        ///  if method return type is task<T>
        /// </summary>
        /// <returns></returns>
        public abstract Task<T> InvokeAsync<T>(MethodAttributeContext context);


        public override object Invoke(MethodInfo method, object[] parameters)
        {
            return Contexts.TryGetAttributeContext(method, typeof(TAttribute), out MethodAttributeContext context)
                ? DefaultInvoke(parameters, context, Invoke)
                : method.Invoke(ProxyInstance, parameters);
        }


        public override Task InvokeAsync(MethodInfo method, object[] parameters)
        {
            return Contexts.TryGetAttributeContext(method, typeof(TAttribute), out MethodAttributeContext context)
                ? (Task)DefaultInvoke(parameters, context, InvokeAsync)
                : (Task)method.Invoke(ProxyInstance, parameters);
        }

        public override Task<T> InvokeAsync<T>(MethodInfo method, object[] parameters)
        {
            return Contexts.TryGetAttributeContext(method, typeof(TAttribute), out MethodAttributeContext context)
                ? (Task<T>)DefaultInvoke(parameters, context, InvokeAsync<T>)
                : (Task<T>)method.Invoke(ProxyInstance, parameters);
        }


        private  Object DefaultInvoke(Object[] parameters,
            MethodAttributeContext context,
            Func<MethodAttributeContext, Object> action)
        {
            context.SetParameters(parameters);
            return action?.Invoke(context);
        }

    }

}