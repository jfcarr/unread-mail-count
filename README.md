# unread-mail-count

Requires: .NET 8

## Usage

Inside `unread-mail-count.json`, update the following:

`DisplayZeroCounts` can be **true** or **false**, and indicates whether you want information displayed about accounts with no unread messages. For example, if you're checking the me@gmail.com account, and DisplayZeroCounts is true, and you have no unread email, you'll see this:

```
0 : me@gmail.com
```

But, if DisplayZeroCounts is false, you won't see anything unless you actually have unread mail.

`OutputDirectory` indicates where you want the **unread_mail_count_simple.txt** file to be generated. This is a plain text file containing information about the results of the latest mail check, and can be easily read by other programs.

`Email` contains information about the **IMAP URL**, **username**, and **password** for account(s) you want to check.  For example, let's say you have two accounts, **me@gmail.com** and **me@outlook.com**.  Your Email entries will look similar to this:

```json
"Email": [
    {
        "HostUrl": "imap.gmail.com",
        "UserName": "me@gmail.com",
        "Password": "my_gmail_password"
    },
    {
        "HostUrl": "outlook.office365.com",
        "UserName": "me@outlook.com",
        "Password": "my_outlook_password"
    }
]
```

After you've updated your app settings, you can run the application with `dotnet run`.  If you'd like to have a standalone application, build it with this:

```bash
dotnet publish -c Release -r linux-x64 --self-contained true /p:PublishSingleFile=true
```

Update the **-r** argument with [a value appropriate for your target OS](https://learn.microsoft.com/en-us/dotnet/core/rid-catalog).
