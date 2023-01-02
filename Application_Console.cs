using System;
using System.Collections.Generic;
using System.Linq;
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
            Document_Management dm = new();
            dm.CheckRoot();

            Console.WriteLine("Hello, welcome to our system!");
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
                    case "display":
                        string adress = dm.GetPathRoot();
                        Console.WriteLine(adress);
                        Console.WriteLine(dm.GetTime(adress));
                        break;
                    case "addFloder":
                        Console.WriteLine("Please enter the folder name:");
                        string name = Console.ReadLine();
                        dm.CreateFloder(name);
                        break;
                    case "exit":
                        Console.WriteLine("Thank you for using this programme.");
                        state = false;
                        break;
                    default:
                        Console.WriteLine("Unknown operator.");
                        break;
                }
            }
        }
    }
}
