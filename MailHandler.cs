using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;

namespace UnreadMailCount
{
    public class MailHandler
    {
        private bool _includeZeroCounts;
        private string? _outputDirectory;
        private string? _outputSimpleText;

        public string? simpleList;

        public MailHandler(bool includeZeroCounts, string outputDirectory)
        {
            _includeZeroCounts = includeZeroCounts;
            _outputDirectory = outputDirectory;

            _outputSimpleText = Path.Combine(_outputDirectory, "unread_mail_count_simple.txt");
        }

        public async Task CheckMail(string hostUrl, string userName, string password)
        {
            try
            {
                using (ImapClient client = new())
                {
                    client.Connect(hostUrl, 993, SecureSocketOptions.SslOnConnect);

                    await client.AuthenticateAsync(userName, password);

                    await client.Inbox.OpenAsync(FolderAccess.ReadOnly);

                    IList<UniqueId> uids = await client.Inbox.SearchAsync(SearchQuery.NotSeen);

                    if (uids.Count > 0 || _includeZeroCounts)
                    {
                        Console.WriteLine($"{uids.Count} : {userName}");

                        simpleList += $"[ {userName}({uids.Count}) ]";
                    }

                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{userName}] {ex.Message}");
            }
        }

        public void OutputSimpleText()
        {
            if (!string.IsNullOrEmpty(_outputSimpleText))
                File.WriteAllText(_outputSimpleText, string.IsNullOrEmpty(simpleList) ? "[ No unread mail ]" : simpleList);
        }
    }
}