using System;

namespace July.Ioc
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class IgnoreAttribute : Attribute
    {

    }
}
