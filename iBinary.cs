using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections;
using System.Reflection.Metadata.Ecma335;

namespace TP_APP_CONSOLE
{
    internal interface iBinary
    {
        public static void WriteBinary(Contact contact, string path)
        {
            using (var stream = File.Open(path, FileMode.Create))
            {
                using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
                {
                    writer.Write(contact.FirstName);
                    writer.Write(contact.LastName);
                    writer.Write(contact.Email);
                    writer.Write(contact.Company);
                    writer.Write(contact.Relationship);
                }
            }
        }

        public static Contact ReadBinary(string path)
        {
            Contact contact = new Contact();
            if (File.Exists(path))
            {
                using (var stream = File.Open(path, FileMode.Open))
                {
                    try
                    {
                        using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                        {
                            contact.FirstName = reader.ReadString();
                            contact.LastName = reader.ReadString();
                            contact.Email = reader.ReadString();
                            contact.Company = reader.ReadString();
                            contact.Relationship = reader.ReadString();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error : " + e.Message);
                        Console.WriteLine("File location : " + path);
                    }
                }
            }
            return contact;
        }
    }
}
