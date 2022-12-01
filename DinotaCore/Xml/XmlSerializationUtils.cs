using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Dinota.Core.Xml
{
    public class XmlSerializationUtils
    {
        static readonly UnicodeEncoding UnicodeEncoding = new UnicodeEncoding(false, false);
        static readonly XmlSerializerNamespaces XmlSerializerNamespaces = new XmlSerializerNamespaces();

        static XmlSerializationUtils()
        {
            XmlSerializerNamespaces.Add("xs", "http://www.w3.org/2001/XMLSchema");
            XmlSerializerNamespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
        }

        public static string ToXml(object @object, Type serializerType)
        {
            var memoryStream = new MemoryStream();
            var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.Unicode);
            (new XmlSerializer(serializerType)).Serialize(xmlTextWriter, @object, XmlSerializerNamespaces);
            return UnicodeEncoding.GetString(memoryStream.ToArray(), 0, (int)memoryStream.Length);
        }

        public static TReturn FromXml<TReturn>(string xml, Type serializerType)
        {
            var memoryStream = new MemoryStream(UnicodeEncoding.GetBytes(xml));
            var xmlTextReader = new XmlTextReader(memoryStream);
            return (TReturn)(new XmlSerializer(serializerType)).Deserialize(xmlTextReader);
        }
    }
}
