using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Net.Security;

namespace TP_APP_CONSOLE
{
    internal interface iXML
    {
        public static void WriteXML(Contact contact, string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Contact));
            FileStream file = File.Create(path);

            serializer.Serialize(file, contact);
            file.Close();
        }

        public static Contact ReadXML(string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Contact));
            StreamReader file = new StreamReader(path);
            Contact contact = (Contact)xmlSerializer.Deserialize(file);
            file.Close();

            return contact;
        }
    }
}
