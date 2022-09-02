using Entities.Concrete.Dto_s;
using Entities.Concrete.MailConfiguration;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

IConnection connection;

IModel _channel = CreateChannel();  



string createMail = "CreateMail";

string responseMail = "ResponseMail";

string directExchange = "DirectExchange";

declares();


await listenAndExecute();






async Task Response(string? mail = null)
{
    if (mail != null)
    {
        //var response = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject($"{mail} adresine bilgilendirme maili gönderildi.."));

        //_channel.BasicPublish(directExchange, responseMail, null, response);$"{}"

        Console.WriteLine($"{mail} adresine bilgilendirme maili gönderildi..\n---------------------------------------");
    }
   
}




async Task listenAndExecute()
{
    var listen = new EventingBasicConsumer(_channel);
    Console.WriteLine("Q listening");

    listen.Received += async (sender, args) =>
    {
        var message = JsonConvert.DeserializeObject<MailDataDTO>(Encoding.UTF8.GetString(args.Body.ToArray()));

        if (message != null)
        {
            Console.WriteLine($"Message Catched..");

            await SendMail(message ,new CancellationToken());

            await Response(message.To.FirstOrDefault());

        }

    };


    _channel.BasicConsume(createMail, true, listen);

    Console.ReadLine();
}




async Task SendMail(MailDataDTO data, CancellationToken ct)
{

    if (data != null)
    {

            var mail = new MimeMessage();

            MailSettings _mailSettings = ConfMail();

            mail.From.Add(new MailboxAddress(data.DisplayName ?? _mailSettings.DisplayName, data.From ?? _mailSettings.From));

            mail.Sender = new MailboxAddress(data.DisplayName ?? _mailSettings.DisplayName, data.From ?? _mailSettings.From);

            foreach (string mailAdress in data.To)
                mail.To.Add(MailboxAddress.Parse(mailAdress));


            if (!string.IsNullOrEmpty(data.ReplyTo))
                mail.ReplyTo.Add(new MailboxAddress(data.ReplyToName, data.ReplyTo));

            if (data.Bcc != null)
            {
                foreach (string mailAdress in data.Bcc.Where(x => !string.IsNullOrWhiteSpace(x)))
                    mail.Bcc.Add(MailboxAddress.Parse(mailAdress.Trim()));
            }

            if (data.Cc != null)
            {
                foreach (var mailAdress in data.Cc.Where(x => !string.IsNullOrWhiteSpace(x)))
                    mail.Cc.Add(MailboxAddress.Parse(mailAdress.Trim()));
            }


            var body = new BodyBuilder();
            mail.Subject = data.Subject;
            body.HtmlBody = data.Body;
            mail.Body = body.ToMessageBody();


            using var smtp = new SmtpClient();

            if (_mailSettings.UseSSL)
            {
                await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.SslOnConnect, ct);
            }

            else if (_mailSettings.UseStartTls)
            {
                await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls, ct);
            }

            await smtp.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password, ct);

            var solution =  await smtp.SendAsync(mail, ct);

            await smtp.DisconnectAsync(true, ct);

            Console.WriteLine($"Mail Sended..");

    }
}






MailSettings ConfMail()
{
    MailSettings mailSettings = new MailSettings()
    {
        Host = "smtp.sendgrid.net",
        Port = 587,
        UserName = "apikey",
        Password = "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",
        DisplayName = "Mert Basar",
        UseSSL = false,
        UseStartTls = true,
        From = "mertbasar0@hotmail.com"
    };

    return mailSettings;
}


#region BasicMethods


void declares()
{
    _channel.QueueDeclare(createMail, false, false, false);
    _channel.QueueDeclare(responseMail, false, false, false);
    _channel.ExchangeDeclare(directExchange, "direct");
    _channel.QueueBind(createMail, directExchange, createMail);
    _channel.QueueBind(responseMail,directExchange, responseMail);

    Console.WriteLine("Declare success..");
}



IModel CreateChannel()
{
    connection = MyCreateConnection();
    var model = connection.CreateModel();

    return model;

}



IConnection MyCreateConnection()
{
    ConnectionFactory factory = new ConnectionFactory()
    {
        Uri = new Uri("amqp://guest:guest@localhost:5672", UriKind.RelativeOrAbsolute)
    };

    return factory.CreateConnection();
}

#endregion

