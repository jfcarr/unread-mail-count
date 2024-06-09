namespace UnreadMailCount.Models
{
    public class MailEntry
    {
        public MailEntry(string hostUrl, string userName, string password)
        {
            HostUrl = hostUrl;
            UserName = userName;
            Password = password;
        }

        public string? HostUrl { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}