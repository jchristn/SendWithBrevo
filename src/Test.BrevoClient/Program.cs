using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GetSomeInput;
using SendWithBrevo;

namespace Test.BrevoClient
{
    public static class Program
    {
        private static SendWithBrevo.BrevoClient _Brevo = null;
        private static bool _RunForever = true;

        public static async Task Main(string[] args)
        {
            string apiKey = Inputty.GetString("API key:", null, false);
            _Brevo = new SendWithBrevo.BrevoClient(apiKey);
            _Brevo.Logger = Console.WriteLine;
            _Brevo.LogRequests = true;
            _Brevo.LogResponses = true;

            while (_RunForever)
            {
                string userInput = Inputty.GetString("Command [?/help]:", null, false);

                switch (userInput)
                {
                    case "q":
                        _RunForever = false;
                        break;
                    case "c":
                    case "cls":
                        Console.Clear();
                        break;
                    case "email":
                        await SendEmail();
                        break;
                    case "?":
                        Console.WriteLine("");
                        Console.WriteLine("Available commands:");
                        Console.WriteLine("q         quit, exit the application");
                        Console.WriteLine("cls       clear the screen");
                        Console.WriteLine("?         help, this menu");
                        Console.WriteLine("email     send an email message");
                        Console.WriteLine("");
                        break;
                }
            }
        }

        private static async Task SendEmail()
        {
            Sender sender = BuildSender("Please provide sender details.");
            if (sender == null) return;

            Sender replyTo = BuildSender("Please provide reply-to details, or just press ENTER to use sender.");
            if (replyTo == null) replyTo = sender;

            List<Recipient> recipients = BuildRecipients("Please provide recipients details.");
            if (recipients.Count < 1) return;

            string subject = Inputty.GetString("Subject:", null, true);
            if (String.IsNullOrEmpty(subject)) return;

            string body = Inputty.GetString("Body   :", null, true);
            if (String.IsNullOrEmpty(body)) return;

            bool isHtml = Inputty.GetBoolean("HTML   :", false);

            List<Recipient> cc = BuildRecipients("Please provide details for those on the CC: line.");
            List<Recipient> bcc = BuildRecipients("Please provide details for those on the BCC: line.");

            Dictionary<string, string> headers = BuildDictionary("Please provide additional headers (optional).");
            Dictionary<string, string> parameters = BuildDictionary("Please provide additional parameters (optional).");
            List<Attachment> attachments = BuildAttachments("Please provide attachment details (optional).");

            bool result = await _Brevo.SendAsync(
                sender,
                recipients,
                subject,
                body,
                isHtml,
                cc,
                bcc,
                (replyTo != null ? replyTo : sender),
                headers,
                parameters,
                attachments);
            Console.WriteLine("Result: " + result);
        }

        private static Sender BuildSender(string prompt)
        {
            Console.WriteLine(Environment.NewLine + prompt);
            Console.WriteLine("Press ENTER on an empty line to end");

            string name = Inputty.GetString("Name :", null, true);
            if (String.IsNullOrEmpty(name)) return null;

            string email = Inputty.GetString("Email:", null, true);
            if (String.IsNullOrEmpty(email)) return null;

            return new Sender(name, email);
        }

        private static List<Recipient> BuildRecipients(string prompt)
        {
            Console.WriteLine(Environment.NewLine + prompt);
            Console.WriteLine("Press ENTER on an empty line to end");
            List<Recipient> recipients = new List<Recipient>();

            while (true)
            {
                string name = Inputty.GetString("Name :", null, true);
                if (String.IsNullOrEmpty(name)) break;

                string email = Inputty.GetString("Email:", null, true);
                if (String.IsNullOrEmpty(email)) break;

                recipients.Add(new Recipient(name, email));
            }

            return recipients;
        }

        private static Dictionary<string, string> BuildDictionary(string prompt)
        {
            Console.WriteLine(Environment.NewLine + prompt);
            Console.WriteLine("Press ENTER on an empty line to end");
            Dictionary<string, string> ret = new Dictionary<string, string>();

            while (true)
            {
                string key = Inputty.GetString("Key  :", null, true);
                if (String.IsNullOrEmpty(key)) break;

                string val = Inputty.GetString("Value:", null, true);

                ret.Add(key, val);
            }

            return ret;
        }

        private static List<Attachment> BuildAttachments(string prompt)
        {
            Console.WriteLine(Environment.NewLine + prompt);
            Console.WriteLine("Press ENTER on an empty line to end");
            List<Attachment> attachments = new List<Attachment>();

            while (true)
            {
                string filename = Inputty.GetString("Filename:", null, true);
                if (String.IsNullOrEmpty(filename)) break;

                string url = Inputty.GetString("URL     :", null, true);
                string content = Inputty.GetString("Content :", null, true);


                attachments.Add(new Attachment(filename, content, url));
            }

            return attachments;
        }
    }
}