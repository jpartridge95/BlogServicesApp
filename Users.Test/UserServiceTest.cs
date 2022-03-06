using Models;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Users.Services;
using Xunit;

namespace Users.Test
{
    public class UserServiceTest
    {
        
        Mock<HttpMessageHandler> messageHandler 
            = new Mock<HttpMessageHandler>(MockBehavior.Strict);

        public UserServiceTest()
        {
            Environment.SetEnvironmentVariable("DATA_BASE_URL", "https://test.com/");
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

            var userService = new UserService(client);

            // Act
            var result = await userService.CreateUser(user);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void CreateUser_ReturnsFalse_OnError()
        {
            // Arrange
            User user = new();
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);
            messageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var client = new HttpClient(messageHandler.Object);

            var userService = new UserService(client);

            // Act
            var result = await userService.CreateUser(user);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void DeleteUser_ReturnsTrue_OnSuccess()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.NoContent);
            messageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var client = new HttpClient(messageHandler.Object);

            var userService = new UserService(client);

            // Act
            var result = await userService.DeleteUser(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void DeleteUser_ReturnsFalse_OnError()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);
            messageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var client = new HttpClient(messageHandler.Object);

            var userService = new UserService(client);

            // Act
            var result = await userService.DeleteUser(1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void GetAllUsers_ReturnsListOfUsers_OnSuccess()
        {
            // Arrange
            string users = JsonSerializer.Serialize<List<User>>(new List<User>()
            {
                new User(),
                new User(),
                new User()
            });

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(users)
            };

            messageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            HttpClient client = new(messageHandler.Object);

            UserService userService = new(client);

            // Act
            var success = await userService.GetAllUsers();

            // Assert
            Assert.IsType<List<User>>(success);
        }

        [Fact]
        public async void GetAllUsers_ReturnsNull_OnError()
        {
            // Arrange
            string users = JsonSerializer.Serialize<List<User>>(null);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(users)
            };

            messageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            HttpClient client = new(messageHandler.Object);

            UserService userService = new(client);

            // Act
            var success = await userService.GetAllUsers();

            // Assert
            Assert.Null(success);
        }

        [Fact]
        public async void GetAllUsers_ReturnsNull_OnEmptyList()
        {
            // Arrange
            string users = JsonSerializer.Serialize<List<User>>(new List<User>());

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(users)
            };

            messageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            HttpClient client = new(messageHandler.Object);

            UserService userService = new(client);

            // Act
            var success = await userService.GetAllUsers();

            // Assert
            Assert.Null(success);
        }

        [Fact]
        public async void GetByEmail_ReturnsUser_OnSuccess()
        {
            // Arrange
            string user = JsonSerializer.Serialize<User>(new User());

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(user)
            };

            messageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            HttpClient client = new(messageHandler.Object);

            UserService userService = new(client);

            // Act
            var success = await userService.GetByEmail("email@email.com");

            // Assert
            Assert.IsType<User>(success);
        }

        [Fact]
        public async void GetByEmail_ReturnsNull_OnNotFound()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);

            messageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            HttpClient client = new(messageHandler.Object);

            UserService userService = new(client);

            // Act
            var success = await userService.GetByEmail("email@email.com");

            // Assert
            Assert.Null(success);
        }

        [Fact]
        public async void GetById_ReturnsUser_OnSuccess()
        {
            // Arrange
            string user = JsonSerializer.Serialize<User>(new User());

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(user)
            };

            messageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            HttpClient client = new(messageHandler.Object);

            UserService userService = new(client);

            // Act
            var success = await userService.GetByID(3);

            // Assert
            Assert.IsType<User>(success);
        }

        [Fact]
        public async void GetByID_ReturnsNull_OnError()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);

            messageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            HttpClient client = new(messageHandler.Object);

            UserService userService = new(client);

            // Act
            var success = await userService.GetByID(1);

            // Assert
            Assert.Null(success);
        }

        [Fact]
        public async void UpdateUser_ReturnsTrue_OnSuccess()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.NoContent);
            messageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var client = new HttpClient(messageHandler.Object);

            var userService = new UserService(client);

            // Act
            var result = await userService.UpdateUser(1, new User());

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void UpdateUser_ReturnsFalse_OnError()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);
            messageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var client = new HttpClient(messageHandler.Object);

            var userService = new UserService(client);

            // Act
            var result = await userService.UpdateUser(1, new User());

            // Assert
            Assert.False(result);
        }
    }
}