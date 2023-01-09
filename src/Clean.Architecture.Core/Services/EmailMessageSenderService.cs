using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Interfaces;

namespace Clean.Architecture.Core.Services;
public class EmailMessageSenderService : IMessageSender
{
  public void SendGuestbookNotificationEmail(string toAddress, string messageBody)
  {
    var message = new MailMessage();
    message.To.Add(new MailAddress(toAddress));
    message.From = new MailAddress("donotreply@guestbook.com");
    message.Subject = "New guestbook entry added";
    message.Body = messageBody;
    using (var client = new SmtpClient("localhost", 25))
    {
      client.Send(message);
    }
  }
}
