using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Dinota.Core.Xml
{
    public class XmlSerializableDictionary
            : Dictionary<string, object>, IXmlSerializable
    {
        private static readonly XmlSerializerNamespaces XmlSerializerNamespaces = new XmlSerializerNamespaces();

        private static readonly XmlSerializer XmlSerializer = new XmlSerializer(typeof(XmlSerializableDictionary));

        private static readonly UnicodeEncoding UnicodeEncoding = new UnicodeEncoding();

        private readonly XmlSerializer ObjectSerializer = new XmlSerializer(typeof(object));

        public XmlSerializableDictionary()
        {
        }

        public XmlSerializableDictionary(IDictionary<string, object> dictionary)
            : base(dictionary)
        {
        }

        #region Implementation of IXmlSerializable

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

       
        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            if (reader.IsEmptyElement)
            {
                return;
            }

            reader.Read();
            while (reader.NodeType != XmlNodeType.EndElement)
            {
                reader.Read();
                object value = ObjectSerializer.Deserialize(reader);
                Add(reader.Name, value);
                reader.ReadEndElement();
            }
            reader.ReadEndElement();
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            foreach (KeyValuePair<string, object> keyValuePair in this)
            {
                writer.WriteStartElement(keyValuePair.Key);
                ObjectSerializer.Serialize(writer, keyValuePair.Value);
                writer.WriteEndElement();
            }
        }

        #endregion

        public string ToXml()
        {
            var memoryStream = new MemoryStream();
            var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.Unicode);
            XmlSerializer.Serialize(xmlTextWriter, this, XmlSerializerNamespaces);

            return UnicodeEncoding.GetString(memoryStream.ToArray(), 0, (int)memoryStream.Length);
        }

        public static XmlSerializableDictionary FromXml(string xml)
        {
            var memoryStream = new MemoryStream(UnicodeEncoding.GetBytes(xml));
            memoryStream.Seek(0, SeekOrigin.Begin);
            var xmlTextReader = new XmlTextReader(memoryStream);

            return (XmlSerializableDictionary)XmlSerializer.Deserialize(xmlTextReader);
        }
    }
}
