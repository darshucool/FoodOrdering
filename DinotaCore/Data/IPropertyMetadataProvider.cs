
namespace Dinota.Core.Data
{
    public interface IPropertyMetadataProvider
    {
        PropertyMetadata GetMetadataFor(string propertyName);
    }
}
