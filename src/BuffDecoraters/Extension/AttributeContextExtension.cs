using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BuffDecoraters.DecoratedHandler;

namespace BuffDecoraters.Extension
{
    public static class AttributeContextExtension
    {

        /// <summary>
        /// try get ActivatorContext,if target attribute return ActivatorContext
        /// </summary>
        /// <param name="contexs"></param>
        /// <param name="method"></param>
        /// <param name="attributeType"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Boolean TryGetAttributeContext(this IEnumerable<MethodAttributeContext> contexs, MethodInfo method, Type attributeType, out MethodAttributeContext context)
        {
            context = contexs?.FirstOrDefault
                (i => i.Method.ToString() == method.ToString() && i.Attribute.GetType() == attributeType);
            if (context == null)
            {
                return false;
            }
            return true;
        }
    }
}