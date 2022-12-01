using System.Collections.Generic;
using System.Xml.Serialization;
using Dinota.Core.Data;

namespace Dinota.Core.Xml
{
    [XmlRoot("Log")]
    public class XmlEntityTracking : XmlUserTracking
    {
        public List<KeyValue> KeysCollection { get; set; }
        public string EntityName { get; set; }
    }

    [XmlRoot("Log")]
    public class XmlUserTracking
    {
        public string Activity { get; set; }
        public string User { get; set; }
    }
    
    public class KeyValue
    {
        public string UnqiueKey { get; set; }
        public object Value { get; set; }
    }

    public class TrackingEntityRecord
    {
        public List<KeyValue> KeyValues { get; set; }
        public string EntityState { get; set; }
        public Entity Entity { get; set; }
    }
}
