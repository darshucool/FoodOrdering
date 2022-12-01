
namespace Dinota.Core.Services
{
    using System;

    /// <summary>
    /// Contract to subscrib to events.
    /// </summary>
    public interface ISubscriptionService
    {
        /// <summary>
        /// Subscribes to a type of messages.
        /// </summary>
        /// <typeparam name="T">Message type</typeparam>
        /// <param name="subscriber">Delegate to be called when a matching event is published</param>
        void Subscribe<T>(Action<object, Message> subscriber) where T : Message;
    }
}
