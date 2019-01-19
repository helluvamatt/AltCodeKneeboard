using System;
using System.ComponentModel;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace AltCodeKneeboard.Models
{
    public class AltCodesXmlParser
    {
        public static AltCodeData LoadFromFile(string path)
        {
            using (var reader = new XmlTextReader(new System.IO.StreamReader(path)))
            {
                return Parse(reader);
            }
        }

        private static AltCodeData Parse(XmlReader reader)
        {
            var serializer = new XmlSerializer(typeof(AltCodeData), "http://schneenet.com/AltCodes.xsd");
            return serializer.Deserialize(reader) as AltCodeData;
        }

        public static void SaveToFile(string path, AltCodeData collection)
        {
            using (var writer = new XmlTextWriter(new System.IO.StreamWriter(path)) { Formatting = Formatting.Indented, Indentation = 2, IndentChar = ' ' })
            {
                var serializer = new XmlSerializer(typeof(AltCodeData), "http://schneenet.com/AltCodes.xsd");
                serializer.Serialize(writer, collection);
            }
        }
    }

    [Serializable]
    [XmlType(AnonymousType=true, Namespace="http://schneenet.com/AltCodes.xsd")]
    [XmlRoot(Namespace="http://schneenet.com/AltCodes.xsd", IsNullable=false, ElementName = "altcodedata")]
    public class AltCodeData
    {
        [XmlElement("groups")]
        public GroupsCollection Groups { get; set; }

        [XmlElement("altcodes")]
        public AltCodesCollection AltCodes { get; set; }
    }
    
    [Serializable]
    [XmlType(AnonymousType=true, Namespace="http://schneenet.com/AltCodes.xsd")]
    public class GroupsCollection
    {
        [XmlElement("group")]
        public Group[] Groups { get; set; }
    }
    
    
    [Serializable]
    [XmlType(AnonymousType=true, Namespace="http://schneenet.com/AltCodes.xsd")]
    public class Group
    {
        [XmlAttribute("id")]
        public int ID { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("iconChar")]
        [DefaultValue("A")]
        public string IconChar { get; set; } = "A";
    }
    
    [Serializable]
    [XmlType(AnonymousType=true, Namespace="http://schneenet.com/AltCodes.xsd")]
    public class AltCodesCollection
    {
        [XmlElement("altcode")]
        public AltCode[] AltCodes { get; set; }
    }
    
    [Serializable]
    [XmlType(AnonymousType=true, Namespace="http://schneenet.com/AltCodes.xsd")]
    public class AltCode
    {
        [XmlIgnore]
        public ushort Unicode { get; set; }

        [XmlAttribute("unicode")]
        public string UnicodeStr
        {
            get => string.Format("0x{0:x4}", Unicode);
            set
            {
                if (value.ToLower().StartsWith("0x")) value = value.Substring(2);
                Unicode = ushort.Parse(value, System.Globalization.NumberStyles.HexNumber);
            }
        }

        [XmlAttribute("code")]
        public string Code { get; set; }

        [XmlAttribute("code0")]
        public string Code0 { get; set; }

        [XmlAttribute("description")]
        public string Description { get; set; }

        [XmlIgnore]
        public int[] Groups { get; set; }

        [XmlAttribute("groups")]
        public string GroupsStr
        {
            get => string.Join(",", Groups.Select(g => g.ToString()));
            set => Groups = value.Split(',').Select(s => int.Parse(s)).ToArray();
        }
    }
}
