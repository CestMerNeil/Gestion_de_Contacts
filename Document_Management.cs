﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Net.Security;
using Microsoft.Isam.Esent.Interop.Vista;
using System.Security;

namespace TP_APP_CONSOLE
{
    internal class Document_Management
    {
        private readonly string pathRoot = @"D:\CodeGithub\TP_APP_CONSOLE\ROOT";

        public string GetPathRoot()
        {
            return pathRoot;
        }

        /**
          * @fn      checkRoot
          * @brief   Check the root path and create it if it does not exist.
          */
        public void CheckRoot()
        {
            try
            {
                if (!Directory.Exists(pathRoot))
                {
                    Console.WriteLine("Creating the root directory.");
                    Directory.CreateDirectory(pathRoot);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        /**
          * @fn     createFloder
          * @brief  Create a level 1 folder
          */
        public void CreateFolder(string name)
        {
            try
            {
                StringBuilder newFloder = new StringBuilder();
                newFloder.Append(pathRoot)
                    .Append("\\")
                    .Append(name);
                Directory.CreateDirectory(newFloder.ToString());
                Console.WriteLine(newFloder.ToString() + "has been successfully created.");
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        public void AddContact(Document_Management dm, Contact contact, string folder, string mode)
        {
            StringBuilder pathContact = new StringBuilder();
            pathContact.Append(dm.GetPathRoot());
            pathContact.Append("\\");
            pathContact.Append(folder);
            pathContact.Append("\\");
            pathContact.Append(contact.FirstName);
     
            if (mode == "0")
            {
                pathContact.Append(".xml");
                iXML.WriteXML(contact, pathContact.ToString());
            }
            else 
            {
                pathContact.Append(".json");
                iBinary.WriteBinary(contact, pathContact.ToString());
            }
        }

        public string GetTime(string path)
        {
            DateTime timeCreate = Directory.GetCreationTime(path);
            DateTime timeMotive = Directory.GetLastWriteTime(path);
            string time = "Creation time : " + timeCreate.ToString() +
                ". Modify time : "  + timeMotive.ToString();
            return time;
        }

        public string GetFolder(DirectoryInfo di)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (DirectoryInfo file in di.GetDirectories())
            {
                string location = file.FullName;
                stringBuilder.Append(file.Name + "  (" + GetTime(location) + ")");
                stringBuilder.Append("\n");
                foreach (FileInfo fi in file.GetFiles())
                {
                    string fileType = System.IO.Path.GetExtension(fi.Name);
                    if (fileType == ".xml")
                    {
                        Contact contact = new Contact();
                        contact = iXML.ReadXML(fi.FullName);

                        stringBuilder.Append("\t");
                        stringBuilder.Append(contact.FirstName);
                        stringBuilder.Append(" ");
                        stringBuilder.Append(contact.LastName);
                        stringBuilder.Append("(");
                        stringBuilder.Append(contact.Company);
                        stringBuilder.Append(")");
                        stringBuilder.Append(" Email:");
                        stringBuilder.Append(contact.Email);
                        stringBuilder.Append(" Link:");
                        stringBuilder.Append(contact.Relationship);
                        stringBuilder.Append("\n");
                    }
                    else if (fileType == ".json")
                    {
                        Contact contact = new Contact();
                        contact = iBinary.ReadBinary(fi.FullName);

                        stringBuilder.Append("\t");
                        stringBuilder.Append(contact.FirstName);
                        stringBuilder.Append(" ");
                        stringBuilder.Append(contact.LastName);
                        stringBuilder.Append("(");
                        stringBuilder.Append(contact.Company);
                        stringBuilder.Append(")");
                        stringBuilder.Append(" Email:");
                        stringBuilder.Append(contact.Email);
                        stringBuilder.Append(" Link:");
                        stringBuilder.Append(contact.Relationship);
                        stringBuilder.Append("\n");

                    }
                    else
                    {
                        string filename = fi.Name;
                        stringBuilder.Append("\t");
                        stringBuilder.Append(filename);
                        stringBuilder.Append("\n");
                    }
                }

            }
            return stringBuilder.ToString();
        }

        public void help()
        {
            Console.WriteLine("hello\t\t-> To get a hello");
            Console.WriteLine("displayroot\t-> Show root path");
            Console.WriteLine("display\t\t-> Show all files and folders");
            Console.WriteLine("addFolder\t-> Add a folder");
            Console.WriteLine("addContact\t-> Add a contact");
            Console.WriteLine("exit\t\t-> Exit procedures");
            Console.WriteLine("--help\t\t-> Find a user manual");
        }
    }
}
