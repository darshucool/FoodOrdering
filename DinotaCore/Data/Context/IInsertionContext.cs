
namespace Dinota.Core.Data.Context
{
    public interface IInsertionContext : IEntityContext
    {
        string GenerateID(int idType, params object[] args);
    }
}
