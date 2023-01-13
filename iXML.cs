using System.Security.Cryptography;
using System.Xml.Serialization;


namespace TP_APP_CONSOLE
{
    internal interface iXML
    {
        public static void WriteXML(Contact contact,
                                    string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Contact));
            FileStream fileStream = File.Create(path);
            using (Aes aes = Aes.Create())
            {
                ICryptoTransform cryptoTransform = aes.CreateEncryptor();
                using (CryptoStream cryptoStream = new CryptoStream(fileStream, cryptoTransform, CryptoStreamMode.Write))
                {
                    serializer.Serialize(cryptoStream, contact);
                }
            }
        }

        public static Contact ReadXML(string path)
        {
            Contact contact =  new Contact();
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Contact));
                FileStream fileStream = File.Create(path);
                using (Aes aes = Aes.Create())
                {
                    ICryptoTransform decryptoTransform = aes.CreateEncryptor();
                    using (CryptoStream cryptoStream = new CryptoStream(fileStream, decryptoTransform, CryptoStreamMode.Read))
                    {
                        contact = (Contact)xmlSerializer.Deserialize(cryptoStream);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error :" + e.Message);
                Console.WriteLine("File location : " + path);
            }

            return contact;
        }
    }
}
