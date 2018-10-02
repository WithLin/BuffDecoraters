using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using BuffDecoraters.Extension;

namespace BuffDecoraters.DecoratedHandler
{

    /// <summary>
    /// like mvc ActionFilter, it means  multiple handler
    /// consider you have two handler,it will like A.OnExecuting -> B.OnExecuting -> Instance.Method.Invoke -> B.OnExecuted -> A.OnExecuted
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    public abstract class PipeMethodAttributeHandler<TAttribute> : MethodsHandler where TAttribute : Attribute
    {
        private IEnumerable<MethodAttributeContext> Contexts { get; set; }

        public abstract void OnExecuting(MethodAttributeContext context);

        public abstract void OnExecuted(MethodAttributeContext context);


        public override object Invoke(MethodInfo method, object[] parameters)
        {
            return Contexts.TryGetAttributeContext(method, typeof(TAttribute), out MethodAttributeContext context)
                ? PipeInvoke(method, parameters, context)
                : method.Invoke(ProxyInstance, parameters);
        }


        public override Task InvokeAsync(MethodInfo method, object[] parameters)
        {
            return Contexts.TryGetAttributeContext(method, typeof(TAttribute), out MethodAttributeContext context)
                ? (Task)PipeInvoke(method, parameters, context)
                : (Task)method.Invoke(ProxyInstance, parameters);
        }

        public override Task<T> InvokeAsync<T>(MethodInfo method, object[] parameters)
        {
            return Contexts.TryGetAttributeContext(method, typeof(TAttribute), out MethodAttributeContext context)
                ? (Task<T>)PipeInvoke(method, parameters, context)
                : (Task<T>)method.Invoke(ProxyInstance, parameters);
        }


        private Object PipeInvoke(MethodInfo method, Object[] parameters,
            MethodAttributeContext context)
        {
            context.SetParameters(parameters);
            OnExecuting(context);
            var resul = method.Invoke(ProxyInstance, parameters);
            OnExecuted(context);
            return resul;
        }


    }
}