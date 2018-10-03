using System;
using System.Collections.Generic;
using System.Linq;
using BuffDecoraters.DecoratedHandler;

namespace BuffDecoraters.DependencyInjection
{
    public class ServiceTypeContext
    {
        public ServiceTypeContext(Type type)
        {
            Type = type;
            _methodContexts = new List<MethodAttributeContext>();
        }

        public Type Type { get; }
        private List<MethodAttributeContext> _methodContexts { get; }

        public IEnumerable<MethodAttributeContext> MethodContexts => _methodContexts;


        public void AddContext(MethodAttributeContext context)
        {
            var exContext =
                _methodContexts.FirstOrDefault(i => i.Attribute.TypeId == context.Attribute.TypeId);
            if (exContext != null && (context != null && exContext.Method.ToString() == context.Method.ToString()))
            {
                exContext.SetAttribute(context.Attribute);
            }
            else
            {
                _methodContexts.Add(context);
            }
        }
    }
}