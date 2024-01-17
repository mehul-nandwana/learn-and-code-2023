using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;
using Solution.Models;

namespace Solution.Services
{
    public class CarbonFootprintAPIService
    {
        public int GetCountOfMessages(EmailEntries email, string folderName)
        {
            using (var client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                client.Authenticate(email.username, email.password);
                var personalNamespace = client.PersonalNamespaces[0];
                var folders = client.GetFolders(personalNamespace);
                var selectedFolder = GetFolderByFullName(folders, folderName);
                selectedFolder.Open(FolderAccess.ReadOnly);
                return selectedFolder.Count;
            }
        }

        public IMailFolder GetFolderByFullName(IEnumerable<IMailFolder> folders, string folderName)
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
    }
}
