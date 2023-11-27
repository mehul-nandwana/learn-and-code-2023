using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;
using Solution;
using Solution.Models;

class CarbonEmission
{

    public static void Main(String[] args)
    {
        CarbonFootprint carbonFootprint = new CarbonFootprint();
        Console.WriteLine("Enter the Entity Type Email or Server");
        string entityType=Console.ReadLine();
        if (entityType.ToLower().Equals("email"))
        {
            CarbonEmission carbonEmission = new CarbonEmission();
            carbonEmission.DisplayCarbonEmmissionForEmail();
        }
        else if(entityType.ToLower().Equals("server"))
        {
            CarbonEmission carbonEmission = new CarbonEmission();
            carbonEmission.DisplayCarbonEmmissionForServer();
        }
        else
        {
            Console.WriteLine("Enter Correct entity type");
        }
    }

     private int CountOfMessages(string username, string password, string folderName)
     {
        using (var client = new ImapClient())
        {          
                client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                client.Authenticate(username, password);
                var personalNamespace = client.PersonalNamespaces[0]; 
                var folders = client.GetFolders(personalNamespace);
                var selectedFolder = GetFolderByFullName(folders, folderName);
                selectedFolder.Open(FolderAccess.ReadOnly);
                return selectedFolder.Count;
        }
     }

     private IMailFolder GetFolderByFullName(IEnumerable<IMailFolder> folders, string folderName)
     {
        foreach (var folder in folders)
        {
            if (string.Equals(folder.FullName, folderName, StringComparison.OrdinalIgnoreCase))
            {
                return folder;
            }
        }
        return null;
     }

    private void DisplayCarbonEmmissionForEmail()
    {
        CarbonFootprint carbonFootprint = new CarbonFootprint();
        Email email = new Email();
        CarbonEmission carbonEmission = new CarbonEmission();
        string dummyEmail = "kunalvaishnavdav@gmail.com";
        string dummyEmailPassword = "onjzedcbzvjwlfaw";
        string spamFolderPath = "[Gmail]/Spam";
        string sentMailFolderPath = "[Gmail]/Sent Mail";
        string inboxFolderPath = "INBOX";
        email.source = dummyEmail;
        email.password = dummyEmailPassword;
        email.countOfSpamMail = carbonEmission.CountOfMessages(email.source, email.password, spamFolderPath);
        email.countOfSendMail = carbonEmission.CountOfMessages(email.source, email.password, sentMailFolderPath);
        email.countOfInboxMail = carbonEmission.CountOfMessages(email.source, email.password, inboxFolderPath);
        double totalCarbonEmission = carbonFootprint.CalculateCarbonEmissionForEmail(email.countOfInboxMail, email.countOfSpamMail, email.countOfSendMail);
        Console.WriteLine("Carbon Emission is " + totalCarbonEmission + " gram");
    }

    private void DisplayCarbonEmmissionForServer()
    {
        CarbonFootprint carbonFootprint = new CarbonFootprint();
        Server server = new Server();
        Console.WriteLine("Enter the duration of Usage in hours");
        server.duration = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter the power consumption in Kilo watts");
        server.powerConsumption = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter the efficiency");
        server.efficiency = Convert.ToDouble(Console.ReadLine());
        double totalCarbonEmission = carbonFootprint.CalculateCarbonEmissionForServer(server.duration, server.powerConsumption, server.efficiency);
        Console.WriteLine("Carbon Emission is " + totalCarbonEmission + " gram");
    }
}
