﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Net.Security;

namespace TP_APP_CONSOLE
{
    internal class Document_Management
    {
        private readonly string pathRoot = @"D:\\CodeGithub\\TP_APP_CONSOLE\\ROOT";

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

        public void AddContact(string name, string folder)
        {
            
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
            }
            return stringBuilder.ToString();
        }
    }
}
