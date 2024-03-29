﻿using DataAccess.Abstract;
using Entities.Concrete.Dto_s;
using Entities.Concrete.MailConfiguration;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class MailDal : IMailDal
    {
        //private readonly MailSettings _mailSettings;


        //public MailDal(IOptions<MailSettings> settings)
        //{
        //    _mailSettings = settings.Value;
        //}

        //public async Task<bool> SendAsync(MailDataDTO data, CancellationToken ct)
        //{
        //    try
        //    {
        //        var mail = new MimeMessage();

        //        mail.From.Add(new MailboxAddress(data.DisplayName ?? _mailSettings.DisplayName, data.From ?? _mailSettings.From));

        //        mail.Sender = new MailboxAddress(data.DisplayName ?? _mailSettings.DisplayName, data.From ?? _mailSettings.From);

        //        foreach (string mailAdress in data.To)
        //            mail.To.Add(MailboxAddress.Parse(mailAdress));


        //        if (!string.IsNullOrEmpty(data.ReplyTo))
        //            mail.ReplyTo.Add(new MailboxAddress(data.ReplyToName, data.ReplyTo));

        //        if (data.Bcc != null)
        //        {
        //            foreach (string mailAdress in data.Bcc.Where(x => !string.IsNullOrWhiteSpace(x)))
        //                mail.Bcc.Add(MailboxAddress.Parse(mailAdress.Trim()));
        //        }

        //        if (data.Cc != null)
        //        {
        //            foreach (var mailAdress in data.Cc.Where(x => !string.IsNullOrWhiteSpace(x)))
        //                mail.Cc.Add(MailboxAddress.Parse(mailAdress.Trim()));
        //        }


        //        var body = new BodyBuilder();
        //        mail.Subject = data.Subject;
        //        body.HtmlBody = data.Body;
        //        mail.Body = body.ToMessageBody();


        //        using var smtp = new SmtpClient();

        //        if (_mailSettings.UseSSL)
        //        {
        //            await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.SslOnConnect, ct);
        //        }

        //        else if (_mailSettings.UseStartTls)
        //        {
        //            await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls, ct);
        //        }

        //        await smtp.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password, ct);

        //        await smtp.SendAsync(mail, ct);

        //        await smtp.DisconnectAsync(true, ct);

        //        return true;

        //    }
        //    catch (Exception)
        //    {

        //        return false;
        //    }



        //}

        private IConnection _connection;
        private IConnection connection => _connection ?? (_connection = CreateConnetion());


        public IModel _channel;
        public IModel channel => _channel ?? (_channel = CreateChannel());


        string createMail = "CreateMail";

        string responseMail = "ResponseMail";

        string directExchange = "DirectExchange";



        private IConnection? CreateConnetion()
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };

            return factory.CreateConnection();
        }




        private IModel CreateChannel()
        {
            return connection.CreateModel();
        }
        



        public async Task CreateRabbitMQSenderAsync(MailDataDTO mailData)
        {
            var message = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(mailData));

            channel.BasicPublish(directExchange, createMail, null, message);
        }


        string? to = null;
        public string RabbitMQReceivedAsync()
        {

            var result = new EventingBasicConsumer(channel);

            result.Received += (sender, args) =>
            {
                var message = JsonConvert.DeserializeObject<string>(Encoding.UTF8.GetString(args.Body.ToArray()));

                to = message;
            };

            channel.BasicConsume(responseMail, true, result);

            return to;
        }
    }
}
