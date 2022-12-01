using System;

namespace Dinota.Core.Data
{
    /// <summary>
    /// Indicates that a Type having this attribute should not be persisted.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class NotTrackedAttribute : Attribute
    {
    }
}
