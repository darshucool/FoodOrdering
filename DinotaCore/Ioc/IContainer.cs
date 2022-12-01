using System;

namespace Dinota.Core.Ioc
{
    /// <summary>
    /// Contract that specifies the types of services provided by the IoC container
    /// </summary>
    public interface IContainer
    {
        TService Resolve<TService>();

        object Resolve(Type serviceType);

        TService ResolveNamed<TService>(string name);

        object ResolveNamed(Type serviceType, string name);
    }
}
