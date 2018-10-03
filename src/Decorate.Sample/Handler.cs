using System.Threading.Tasks;
using BuffDecoraters.DecoratedHandler;
using Serilog;

namespace Decorate.Sample
{
    public class AHandler:MethodAttributeHandler<ATestAttribute>
    {
        public override object Invoke(MethodAttributeContext context)
        {
            Log.Logger.Information("AHandler_Pre");
            var result = context.Method.Invoke(ProxyInstance, context.Parameters);
            Log.Logger.Information("AHandler_Post");
            return result;
        }

        public override Task InvokeAsync(MethodAttributeContext context)
        {
            throw new System.NotImplementedException();
        }

        public override Task<T> InvokeAsync<T>(MethodAttributeContext context)
        {
            throw new System.NotImplementedException();
        }


    }

    public class APipeProxyHandler:PipeMethodAttributeHandler<ATestAttribute>
    {
        public override void OnExecuting(MethodAttributeContext context)
        {
            Log.Logger.Information("APipe_Pre");
        }

        public override void OnExecuted(MethodAttributeContext context)
        {
            Log.Logger.Information("APipe_Post");
        }
    }

    public class BPipeProxyHandler : PipeMethodAttributeHandler<BTestAttribute>
    {
        public override void OnExecuting(MethodAttributeContext context)
        {
            Log.Logger.Information("BPipe_Pre");
            Log.Logger.Information(((BTestAttribute)context.Attribute).Vaule);
        }

        public override void OnExecuted(MethodAttributeContext context)
        {
            Log.Logger.Information("BPipe_Post");
        }
    }
}