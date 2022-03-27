using BlazorFrontend.Services;
using Models;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BlazorFrontEnd.Test
{
    public class UsersTest
    {
        Mock<HttpMessageHandler> messageHandler
            = new Mock<HttpMessageHandler>(MockBehavior.Strict);

        public UsersTest()
        {
            System.Environment.SetEnvironmentVariable("USERS_URI", "https://test.com");
        }

        [Fact]
        public async void CreateUser_ReturnsTrue_OnSuccess()
        {
            // Arrange
            User user = new();
            var response = new HttpResponseMessage(HttpStatusCode.NoContent);
            messageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var client = new HttpClient(messageHandler.Object);

            var userService = new UsersService(client);

            // Act
            var result = await userService.CreateUser(user);

            // Assert
            Assert.True(result);
        }
    }
}