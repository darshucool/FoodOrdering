using System;
using Autofac;

namespace Dinota.Core.Ioc
{
    public class AutofacContainerAdapter : IContainer
    {
        private readonly Func<ILifetimeScope> _scopeResolver;

        public AutofacContainerAdapter(Func<ILifetimeScope> scopeResolver)
        {
            _scopeResolver = scopeResolver;
        }

        public ILifetimeScope Container
        {
            get
            {
                return _scopeResolver();
            }
        }

        public TService Resolve<TService>()
        {
            return Container.Resolve<TService>();
        }

        public object Resolve(Type serviceType)
        {
            return Container.Resolve(serviceType);
        }

        public TService ResolveNamed<TService>(string name)
        {
            return Container.ResolveNamed<TService>(name);
        }

        public object ResolveNamed(Type serviceType, string name)
        {
            return Container.ResolveNamed(name, serviceType);
        }
    }
}
