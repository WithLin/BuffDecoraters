using System;
using System.Reflection;

namespace BuffDecoraters.DecoratedHandler
{
    /// <summary>
    /// MethodAttributeContext
    /// </summary>
    public class MethodAttributeContext
    {
        public MethodAttributeContext(MethodInfo method, Attribute attribute)
        {
            Method = method;
            Attribute = attribute;
        }
        public object[] Parameters { get; private set; }

        public MethodInfo Method { get; }
        public Attribute Attribute { get; private set; }

        public void SetAttribute(Attribute targetAttribute)
        {
            Attribute = targetAttribute ?? throw new ArgumentNullException(nameof(targetAttribute));
        }

        public void SetParameters(object[] parameters)
        {
            Parameters = parameters;
        }
    }
}