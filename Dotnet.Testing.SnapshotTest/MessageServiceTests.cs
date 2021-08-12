using System;
using System.Threading.Tasks;
using Dotnet.Testing.Web.Services;
using VerifyXunit;
using Xunit;

namespace Dotnet.Testing.SnapshotTest
{
    [UsesVerify]
    public class MessageServiceTests
    {
        [Fact]
        public Task GetById_ShouldReturnMessage()
        {
            // Arrange
            var messageService = new MessageService();
            
            // Act
            var message = messageService.GetMessageById(1);
            
            // Assert
            return Verifier.Verify(message);
        }
        
        [Fact]
        public Task GetAllMessage_ShouldReturnMessageList()
        {
            // Arrange
            var messageService = new MessageService();
            
            // Act
            var messages = messageService.GetMessages();
            
            // Assert
            return Verifier.Verify(messages);
        }
    }
}