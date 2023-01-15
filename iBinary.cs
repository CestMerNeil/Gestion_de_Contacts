using System.Runtime.ExceptionServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace TP_APP_CONSOLE
{
    internal interface iBinary
    {
        public static void WriteBinary(Contact contact,
                                       string path)
        {
            using var stream = File.Open(path, FileMode.Create);
            using var writer = new BinaryWriter(stream, Encoding.UTF8, false);
            writer.Write(contact.FirstName);
            writer.Write(contact.LastName);
            writer.Write(contact.Email);
            writer.Write(contact.Company);
            writer.Write(contact.Relationship);
        }

        public static Contact ReadBinary(string path)
        {
            Contact contact = new Contact();
            if (File.Exists(path))
            {
                using var stream = File.Open(path, FileMode.Open);
                try
                {
                    using var reader = new BinaryReader(stream, Encoding.UTF8, false);
                    contact.FirstName = reader.ReadString();
                    contact.LastName = reader.ReadString();
                    contact.Email = reader.ReadString();
                    contact.Company = reader.ReadString();
                    contact.Relationship = reader.ReadString();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error : " + e.Message);
                    Console.WriteLine("File location : " + path);
                }
            }
            return contact;
        }

        private static byte[] GetEncryptionKey(string password)
        {
            var salt = new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000);
            return pbkdf2.GetBytes(16);
        }

        public static void WriteBinary(Contact data, string filePath, string password)
        {
            var formatter = new BinaryFormatter();
            var encryptionKey = GetEncryptionKey(password);

            using (var fs = new FileStream(filePath, FileMode.Create))
            using (var encryptor = new AesCryptoServiceProvider().CreateEncryptor(encryptionKey, encryptionKey))
            using (var cryptoStream = new CryptoStream(fs, encryptor, CryptoStreamMode.Write))
            {
                formatter.Serialize(cryptoStream, data);
            }
        }

        public static Contact ReadBinary(string filePath, string password)
        {
            var formatter = new BinaryFormatter();
            var encryptionKey = GetEncryptionKey(password);

            using (var fs = new FileStream(filePath, FileMode.Open))
            using (var decryptor = new AesCryptoServiceProvider().CreateDecryptor(encryptionKey, encryptionKey))
            using (var cryptoStream = new CryptoStream(fs, decryptor, CryptoStreamMode.Read))
            {
                return (Contact)formatter.Deserialize(cryptoStream);
            }
        }
    }
}
