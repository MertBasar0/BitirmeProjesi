using Entities.Concrete.Dto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IMailDal
    {
        //Task<bool> SendAsync(MailDataDTO data, CancellationToken ct);

        Task CreateRabbitMQSenderAsync(MailDataDTO mailData);

        string RabbitMQReceivedAsync();
    }
}
