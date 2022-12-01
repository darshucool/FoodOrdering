using Dinota.Core.Ioc;
using Dinota.Core.Services;

namespace Dinota.Core
{
    /// <summary>
    /// Base class to setup assembly specific configurations.
    /// </summary>
    public abstract class ConfigurationBase
    {
        /// <summary>
        /// Gets or sets the IoC container to resolve dependencies
        /// </summary>
        public virtual IContainer Container { get; set; }

        /// <summary>
        /// Place to register subscribers
        /// </summary>
        /// <param name="subscriptionService">The service injected</param>
        public virtual void SetupSubscritions(ISubscriptionService subscriptionService)
        {
        }
    }
}