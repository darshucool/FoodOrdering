
namespace Dinota.Core.Services
{
    /// <summary>
    /// Contract to implement event publication service. This is part of the Pub/Sub system to publish events.
    /// </summary>
    public interface IPublicationService
    {
        /// <summary>
        /// Publishes an event.
        /// </summary>
        /// <param name="sender">source of the event</param>
        /// <param name="message">Message to be published</param>
        void Publish(object sender, Message message);
    }
}
