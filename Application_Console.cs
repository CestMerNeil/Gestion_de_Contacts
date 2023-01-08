using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TP_APP_CONSOLE
{
    internal class Application_Console
    {
        static void Main(string[] args)
        {
            bool state = true;
            Document_Management documentMangement = new Document_Management();
            DirectoryInfo directoryInfo = new DirectoryInfo(documentMangement.GetPathRoot());
            documentMangement.CheckRoot();

            Console.WriteLine("Hello, welcome!");
            while (state)
            {
                Console.WriteLine("Please input your operation.");
                //Console.WriteLine("displayRoot: to get the path of root floder");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "hello":
                        Console.WriteLine("hello");
                        break;
                    case "displayroot":
                        string adress = documentMangement.GetPathRoot();
                        Console.WriteLine(adress + " " + documentMangement.GetTime(adress));
                        break;
                    case "display":
                        //documentMangement.GetFolder(directoryInfo);
                        Console.WriteLine(documentMangement.GetFolder(directoryInfo));
                        break;
                    case "addFolder":
                        Console.WriteLine("Please enter the folder name:");
                        string name;
                        name = Console.ReadLine();
                        if (name == "")     // If entered as empty, name it "New Folder"
                        {
                            name = "New Folder"; 
                        }
                        documentMangement.CreateFolder(name);
                        break;
                    case "addContact":
                        Console.WriteLine("Please enter the folder you will be using:");
                        Contact contact = new Contact();
                        string addC2F = Console.ReadLine();
                        Console.WriteLine("First Name Please!");
                        contact.FirstName = Console.ReadLine();
                        Console.WriteLine("Last Name Please!");
                        contact.LastName = Console.ReadLine();
                        Console.WriteLine("Email Please!");
                        string email = Console.ReadLine();
                        while (!contact.IsValidEmail(email))
                        {
                            Console.WriteLine("Wrong Adress,");
                            email = Console.ReadLine();
                        }
                        contact.Email = email;
                        Console.WriteLine("Company Please!");
                        contact.Company = Console.ReadLine();
                        Console.WriteLine("Relationship Please!");
                        contact.Relationship = Console.ReadLine();
                        Console.WriteLine("Write mode, please! 0 -> xml 1 -> binary");
                        var mode = Console.ReadLine();
                        documentMangement.AddContact(documentMangement, contact, addC2F, mode);
                        break;
                    case "exit":
                        Console.WriteLine("Thank you for using this programme.");
                        state = false;
                        break;
                    case "--help":
                        documentMangement.help();
                        break;
                    default:
                        Console.WriteLine("Unknown operator. See <--help>");
                        break;
                }
            }
        }
    }
}
