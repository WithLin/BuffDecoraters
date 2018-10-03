using System;

namespace Decorate.Sample
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ATestAttribute : Attribute
    {
        
    }


    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class BTestAttribute : Attribute
    {
        public String Vaule { get; }
        public BTestAttribute(string vaule)
        {
            Vaule = vaule;
        }
    }
}