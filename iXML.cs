using System;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Linq;
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
            serializer.Serialize(fileStream, contact);
            fileStream.Close();
        }

        public static Contact ReadXML(string path)
        {
            Contact contact =  new Contact();
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Contact));
                FileStream fileStream = File.Create(path);
                contact = (Contact)xmlSerializer.Deserialize(fileStream);
                fileStream.Close();
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Error :" + e.Message);
                Console.WriteLine("File location : " + path);
            }

            return contact;
        }
        
        private static byte[] GetEncryptionKey(string password)
        {
            var salt = new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000);
            return pbkdf2.GetBytes(16);
        }

        public static void WriteXML(Contact data, string filePath, string password)
        {
            var serializer = new XmlSerializer(typeof(Contact));
            var encryptionKey = GetEncryptionKey(password);

            using (var fs = new FileStream(filePath, FileMode.Create))
            using (var encryptor = new AesCryptoServiceProvider().CreateEncryptor(encryptionKey, encryptionKey))
            using (var cryptoStream = new CryptoStream(fs, encryptor, CryptoStreamMode.Write))
            {
                serializer.Serialize(cryptoStream, data);
            }
        }

        public static Contact ReadXML(string filePath, string password)
        {
            Contact contact = new Contact();
            var serializer = new XmlSerializer(typeof(Contact));
            var encryptionKey = GetEncryptionKey(password);
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Open))
                using (var decryptor = new AesCryptoServiceProvider().CreateDecryptor(encryptionKey, encryptionKey))
                using (var cryptoStream = new CryptoStream(fs, decryptor, CryptoStreamMode.Read))
                {
                    contact = (Contact)serializer.Deserialize(cryptoStream);
                }
                return contact;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error :" + e.Message);
                Console.WriteLine("File location : " + filePath);
            }
            return contact;
        }
    }
}
