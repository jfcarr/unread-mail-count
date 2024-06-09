using Microsoft.Extensions.Configuration;
using UnreadMailCount;
using UnreadMailCount.Models;

internal class Program
{
    private static async Task Main(string[] args)
    {
        try
        {
            string appSettingsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "unread-mail-count.json");

            IConfiguration config = new ConfigurationBuilder()
              .AddJsonFile(appSettingsFile)
              .AddEnvironmentVariables()
              .Build();

            List<MailEntry>? mailEntries = config.GetSection("Email").Get<List<MailEntry>>();
            bool displayZeroCounts = config.GetValue<bool>("DisplayZeroCounts");
            string? outputDirectory = config.GetValue<string>("OutputDirectory") ?? ".";

            Console.WriteLine(outputDirectory);

            if (mailEntries != null)
            {
                MailHandler mailHandler = new(displayZeroCounts, outputDirectory);

                List<Task> tasks = new();

                foreach (MailEntry mailEntry in mailEntries)
                {
                    tasks.Add(
                        mailHandler.CheckMail(
                            mailEntry.HostUrl ?? "",
                            mailEntry.UserName ?? "",
                            mailEntry.Password ?? ""
                        )
                    );
                }

                await Task.WhenAll(tasks);

                mailHandler.OutputSimpleText();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
