
namespace Dinota.Core.Data.Context
{
    /// <summary>
    /// Base contract to supply context info to entities before they are persisted
    /// </summary>
    public interface IEntityContext
    {
        string UserName { get; }

        TService GetService<TService>();
    }
}
