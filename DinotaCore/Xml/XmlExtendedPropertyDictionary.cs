using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Dinota.Core.Xml
{
    /// <summary>
    /// Class used to serialize extended properties.
    /// To identify the modifications the key contains LastModifiedDate and LastModifiedBy attributes
    /// </summary>
    [XmlRoot("ExtendedProperties")]
    public class XmlExtendedPropertyDictionary : Dictionary<ExtendedPropertyKey, object>, IXmlSerializable
    {
        private Dictionary<string, object> extendedProperties = new Dictionary<string, object>();

        private readonly XmlSerializer ObjectSerializer = new XmlSerializer(typeof(object));

        private const string lastModifiedDate = "LastModifiedDate";

        private const string lastModifiedBy = "LastModifiedBy";

        #region IXmlSerializable Members

        /// <summary>
        /// Deserialized properties
        /// </summary>
        [XmlIgnore]
        public Dictionary<string, object> ExtendedProperties
        {
            get { return extendedProperties; }
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (reader.IsEmptyElement)
            {
                return;
            }

            reader.Read();
            while (reader.NodeType != XmlNodeType.EndElement)
            {                
                var key = new ExtendedPropertyKey();
                key.PropertyName = reader.Name;

                reader.MoveToAttribute(lastModifiedDate);
                key.LastModifiedDate = DateTime.Parse(reader[lastModifiedDate], System.Globalization.CultureInfo.InvariantCulture);

                reader.MoveToAttribute(lastModifiedBy);
                key.LastModifiedBy = reader[lastModifiedBy];

                reader.MoveToElement();
                reader.Read();

                object value = ObjectSerializer.Deserialize(reader);
                Add(key, value);

                reader.ReadEndElement();
            }
            reader.ReadEndElement();

            extendedProperties = new Dictionary<string, object>();

            //create a regular name value pair dictionary from this
            foreach (var pair in this)
            {
                extendedProperties.Add(pair.Key.PropertyName, pair.Value);
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            if (extendedProperties.Count == 0)
            {
                return;
            }

            BuildXmlExtendedPropertyDictionary();

            foreach (var keyValuePair in this)
            {
                writer.WriteStartElement(keyValuePair.Key.PropertyName);

                writer.WriteAttributeString(lastModifiedDate, keyValuePair.Key.LastModifiedDate.ToString("s", System.Globalization.CultureInfo.InvariantCulture));
                writer.WriteAttributeString(lastModifiedBy, keyValuePair.Key.LastModifiedBy);

                ObjectSerializer.Serialize(writer, keyValuePair.Value);

                writer.WriteEndElement();
            }
        }

        private void BuildXmlExtendedPropertyDictionary()
        {
            foreach (var pair in extendedProperties)
            {
                var key = new ExtendedPropertyKey(pair.Key);

                if (ContainsKey(key))
                {
                    //value modified?
                    if ((this[key] != null && !this[key].Equals(pair.Value)) 
                        || (pair.Value != null && !pair.Value.Equals(this[key])))
                    {
                        Remove(key);

                        key.PropertyName = pair.Key;
                        key.LastModifiedDate = DateTime.UtcNow;
                        key.LastModifiedBy = "MSM";

                        Add(key, pair.Value);
                    }
                }
                else//value missing. add it
                {
                    key.PropertyName = pair.Key;
                    key.LastModifiedDate = DateTime.UtcNow;
                    key.LastModifiedBy = "MSM";

                    Add(key, pair.Value);
                }
            }

            var keysToRemove = new List<ExtendedPropertyKey>();
            foreach (var key in Keys)
            {
                //note: make every property modified by as MSM to detect changes
                key.LastModifiedBy = "MSM";

                //remove invalid properties
                if (!ExtendedProperties.ContainsKey(key.PropertyName))
                {
                    keysToRemove.Add(key);
                }
            }

            foreach (ExtendedPropertyKey key in keysToRemove)
            {
                Remove(key);
            }
        }
        #endregion

        internal void RebuildDictionary()
        {
            extendedProperties.Clear();

            foreach (var keyValuePair in this)
            {
                extendedProperties.Add(keyValuePair.Key.PropertyName, keyValuePair.Value);
            }
        }

        /// <summary>
        /// Returns d extended key for a given property. Returns null if not found
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns>ExtendedPropertyKey. Returns null if not found</returns>
        public ExtendedPropertyKey GetKey(string propertyName)
        {
            foreach (var extendedPropertyKey in Keys)
            {
                if (extendedPropertyKey.PropertyName == propertyName)
                {
                    return extendedPropertyKey;
                }
            }

            return null;
        }
    }

    /// <summary>
    /// Represents a Extended Property Key
    /// </summary>
    public class ExtendedPropertyKey : IEquatable<ExtendedPropertyKey>
    {
        public ExtendedPropertyKey()
        {            
        }

        public ExtendedPropertyKey(string propertyName)
        {
            PropertyName = propertyName;
            LastModifiedDate = DateTime.UtcNow;
            LastModifiedBy = "MSM";
        }
        
        public string LastModifiedBy { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public string PropertyName { get; set; }

        public bool Equals(ExtendedPropertyKey other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.PropertyName, PropertyName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ExtendedPropertyKey)) return false;
            return Equals((ExtendedPropertyKey)obj);
        }

        public override int GetHashCode()
        {
            return PropertyName.GetHashCode();
        }

        public static bool operator ==(ExtendedPropertyKey left, ExtendedPropertyKey right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ExtendedPropertyKey left, ExtendedPropertyKey right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return PropertyName;
        }
    }
}
