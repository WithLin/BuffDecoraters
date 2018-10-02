using System;
using System.Threading.Tasks;

namespace BuffDecoraters.ProxyHandler
{
    /// <summary>
    /// async handler like PipeMethodAttributeHandler
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    public abstract class AsyncPipeMethodAttributeHandler<TAttribute> 
        : PipeMethodAttributeHandler<TAttribute> where TAttribute : Attribute
    {
        public abstract Task OnExecutingAsync(MethodAttributeContext context);

        public abstract Task OnExecutedAsync(MethodAttributeContext context);

        public override void OnExecuting(MethodAttributeContext context)
        {
            OnExecutingAsync(context);
        }


        public override void OnExecuted(MethodAttributeContext context)
        {
            OnExecutedAsync(context);
        }
    }
}