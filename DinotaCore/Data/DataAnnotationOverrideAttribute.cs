using System;

namespace Dinota.Core.Data
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class DataAnnotationOverrideAttribute : Attribute
    {
        public DataAnnotationOverrideAttribute(Type provider)
        {
            Provider = provider;
        }

        public Type Provider { get; set; }

        private IPropertyMetadataProvider _metadataProvider;

        public PropertyMetadata GetMetadataFor(string propertyName)
        {
            if (_metadataProvider == null)
            {
                _metadataProvider = (IPropertyMetadataProvider)Provider.GetConstructor(new Type[] { }).Invoke(null);
            }

            return _metadataProvider.GetMetadataFor(propertyName);
        }
    }
}
