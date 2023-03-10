using System.Text;
using System.Security.Cryptography;


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
            System.Security.Principal.WindowsIdentity currentUser = System.Security.Principal.WindowsIdentity.GetCurrent();
            string password = currentUser.User.ToString();

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
                        Console.WriteLine(documentMangement.GetFolder(directoryInfo, password));
                        break;
                    case "addFolder":
                        Console.WriteLine("Please enter the folder name:");
                        string name;
                        name = Console.ReadLine();
                        if (name == "")     // If entered as empty, name it "New Folder"
                        {
                            name = "New_Folder"; 
                        }
                        documentMangement.CreateFolder(name);
                        break;
                    case "addContact":
                        Contact contact = new Contact();
                        string addC2F = "Neil";
                        bool folder = true;
                        while (folder)
                        {
                            Console.WriteLine("Please enter the folder will be using:");
                            addC2F = Console.ReadLine();
                            StringBuilder sb = new StringBuilder();
                            sb.Append(documentMangement.GetPathRoot())
                                .Append("\\")
                                .Append(addC2F);
                            if (!Directory.Exists(sb.ToString()))
                            {
                                Console.WriteLine("Folder wrong !");
                            }
                            else
                                folder = false;
                        }
                            
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
                        documentMangement.AddContact(documentMangement, contact, addC2F, mode, password);
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
