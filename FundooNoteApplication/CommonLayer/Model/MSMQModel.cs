using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace CommonLayer.Model
{
    public class MSMQModel
    {
        MessageQueue objMessageQueue = new MessageQueue();

        public void sendData2Queue(string token)
        {
            objMessageQueue.Path = @".\private$\FundooNote";
            if (!MessageQueue.Exists(objMessageQueue.Path))
            {
                MessageQueue.Create(objMessageQueue.Path);
            }
            objMessageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            objMessageQueue.ReceiveCompleted += ObjMessageQueue_ReceiveCompleted;
            objMessageQueue.Send(token);
            objMessageQueue.BeginReceive();
            objMessageQueue.Close();
        }

        private void ObjMessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var message = objMessageQueue.EndReceive(e.AsyncResult);
            string token = message.Body.ToString();
            string subject = "Testing the api and sending the mail";
            string body = token;
            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("piyush.nimje103@gmail.com", "fvtzpojtitbtzbpz"),
                EnableSsl = true,
            };
            smtp.Send("piyush.nimje103@gmail.com", "piyush.nimje103@gmail.com", subject, body);
            objMessageQueue.BeginReceive();
        }
    }
}
