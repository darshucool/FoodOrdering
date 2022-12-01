using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using Dinota.Core.Data;
using Dinota.Core.Xml;

namespace Dinota.Security.Tracking
{
    public class TrackingService : EntityCrudService<Tracking, ITrackingRepo>
    {
        public TrackingService(ITrackingRepo repository)
            : base(repository)
        {
        }

        /// <summary>
        /// Tracking-xml for entity modification, deletion
        /// </summary>
        /// <param name="keysList"></param>
        /// <param name="trackingEntity"></param>
        /// <param name="tracking"></param>
        /// <param name="log"></param>
        /// <param name="userName"></param>
        /// <returns>tracking</returns>
        public Tracking GenerateTrackingXmlField(List<KeyValue> keysList,
                                                 Entity trackingEntity,
                                                 Tracking tracking, string log, string userName)
        {
            var xmlTracking = new XmlEntityTracking
                                  {
                                      EntityName = EditedEntityName(trackingEntity.GetType().Name),
                                      KeysCollection = keysList,
                                      Activity = log,
                                      User = userName
                                  };

            var xml = XmlSerializationUtils.ToXml(xmlTracking, typeof(XmlEntityTracking));
            tracking.Description = xml;

            return tracking;
        }

        /// <summary>
        /// Tracking-xml for entity adding
        /// </summary>
        /// <param name="underlyingObjectContext"></param>
        /// <param name="trackingEntity"></param>
        /// <param name="tracking"></param>
        /// <param name="log"></param>
        /// <param name="userName"></param>
        /// <returns>tracking</returns>
        public Tracking GenerateTrackingXmlField(ObjectContext underlyingObjectContext, Entity trackingEntity,
                                                 Tracking tracking, string log, string userName)
        {
            var xmlTracking = new XmlEntityTracking
            {
                EntityName = EditedEntityName(trackingEntity.GetType().Name),
                KeysCollection = GetKeyValueList(underlyingObjectContext, trackingEntity),
                Activity = log,
                User = userName
            };

            var xml = XmlSerializationUtils.ToXml(xmlTracking, typeof(XmlEntityTracking));
            tracking.Description = xml;

            return tracking;
        }

        /// <summary>
        /// Tracking Login/Logoff
        /// </summary>
        /// <param name="tracking"></param>
        /// <param name="log"></param>
        /// <param name="userName"></param>
        /// <returns>tracking</returns>
        public Tracking GenerateTrackingXmlField(Tracking tracking, string log, string userName)
        {
            var xmlTracking = new XmlUserTracking { Activity = log, User = userName };
            var xml = XmlSerializationUtils.ToXml(xmlTracking, typeof (XmlUserTracking));
            tracking.Description = xml;

            return tracking;
        }

        /// <summary>
        /// Get entity keys and values
        /// </summary>
        /// <param name="underlyingObjectContext"></param>
        /// <param name="trackingEntity"></param>
        /// <returns>list of entity-keys and values</returns>
        public List<KeyValue> GetKeyValueList(ObjectContext underlyingObjectContext,
                                                 Entity trackingEntity)
        {
            var entity = trackingEntity;
            var objectStateEntry = underlyingObjectContext.ObjectStateManager.GetObjectStateEntry(entity);
            var keyValues = objectStateEntry.EntityKey.EntityKeyValues;

            var keysList =
                keyValues.Select(
                    entityKeyMember => new KeyValue { UnqiueKey = entityKeyMember.Key, Value = entityKeyMember.Value }).
                    ToList
                    ();

            return keysList;
        }

        private static string EditedEntityName(string trackingEntityName)
        {
            if (string.IsNullOrEmpty(trackingEntityName)) return null;
            var index = trackingEntityName.IndexOf("_");
            return index != -1 ? trackingEntityName.Substring(0, index) : trackingEntityName;
        }
    }
}
