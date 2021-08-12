using System.Collections.Generic;
using System.Linq;
using Dotnet.Testing.Web.Data.Models;

namespace Dotnet.Testing.Web.Services
{
    public class MessageService
    {
        private static List<Message> Messages = new List<Message>(){
            new Message()
            {
                Id = 1,
                Text = "Something Awesome"
            },
            new Message()
            {
                Id = 2,
                Text = "Something awful :("
            }
        };

        public Message GetMessageById(int id)
        {
            return Messages.FirstOrDefault(x => x.Id == id);
        }

        public List<Message> GetMessages()
        {
            return Messages;
        }

        public void AddMessage(string message)
        {
            Messages.Add(new Message
            {
                Id = Messages.Max(x => x.Id)+1,
                Text = message
            });
        }
    }
}