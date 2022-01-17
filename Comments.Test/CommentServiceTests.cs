using Models;
using Xunit;
using System;
using System.Collections.Generic;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Net;
using Comments.Services;
using Moq.Protected;
using System.Threading;
using System.Threading.Tasks;

namespace Comments.Test
{
    public class CommentServiceTests
    {
        [Fact]
        public async void GetByPost_ReturnsListOfComments_OnSuccess()
        {
            // Arrange
            string asJson = JsonSerializer.Serialize<List<Comment>>(new List<Comment>()
            {
                new Comment(), 
                new Comment()
            });
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(asJson)
            };

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(response);

            var client = new HttpClient(handler.Object)
            {
                BaseAddress = new Uri("https://test.com/")
            };
            var dataAccessService = new DataService(client);

            // Act
            var result = dataAccessService.GetByPost(1);

            // Assert
            Assert.IsType<List<Comment>>(result);
        }

        [Fact]
        public async void GetByPost_ReturnsNull_OnError()
        {
            // Arrange
            string asJson = JsonSerializer.Serialize<Comment>(new Comment());
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(response);

            var client = new HttpClient(handler.Object)
            {
                BaseAddress = new Uri("https://test.com/")
            };
            var dataAccessService = new DataService(client);

            // Act
            var result = dataAccessService.GetByPost(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void GetByUser_ReturnsListOfComments_OnSuccess()
        {
            // Arrange
            string asJson = JsonSerializer.Serialize<List<Comment>>(new List<Comment>()
            {
                new Comment(),
                new Comment()
            });
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(asJson)
            };

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(response);

            var client = new HttpClient(handler.Object)
            {
                BaseAddress = new Uri("https://test.com/")
            };
            var dataAccessService = new DataService(client);

            // Act
            var result = dataAccessService.GetByUser(1);

            // Assert
            Assert.IsType<List<Comment>>(result);
        }

        [Fact]
        public async void GetByUser_ReturnsNull_OnError()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(response);

            var client = new HttpClient(handler.Object)
            {
                BaseAddress = new Uri("https://test.com/")
            };
            var dataAccessService = new DataService(client);

            // Act
            var result = dataAccessService.GetByUser(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void IncrementLikes_ReturnsTrue_OnSuccess()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.NoContent);

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(response);

            var client = new HttpClient(handler.Object)
            {
                BaseAddress = new Uri("https://test.com/")
            };
            var dataAccessService = new DataService(client);

            // Act
            var result = await dataAccessService.IncrementLikes(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void IncrementLikes_ReturnsFalse_OnError()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(response);

            var client = new HttpClient(handler.Object)
            {
                BaseAddress = new Uri("https://test.com/")
            };
            var dataAccessService = new DataService(client);

            // Act
            var result = await dataAccessService.IncrementLikes(1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void DecrementLikes_ReturnsTrue_OnSuccess()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.NoContent);

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(response);

            var client = new HttpClient(handler.Object)
            {
                BaseAddress = new Uri("https://test.com/")
            };
            var dataAccessService = new DataService(client);

            // Act
            var result = await dataAccessService.DecrementLikes(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void DecrementLikes_ReturnsFalse_OnError()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(response);

            var client = new HttpClient(handler.Object)
            {
                BaseAddress = new Uri("https://test.com/")
            };
            var dataAccessService = new DataService(client);

            // Act
            var result = await dataAccessService.DecrementLikes(1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void IncrementDislikes_ReturnsTrue_OnSuccess()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.NoContent);

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(response);

            var client = new HttpClient(handler.Object)
            {
                BaseAddress = new Uri("https://test.com/")
            };
            var dataAccessService = new DataService(client);

            // Act
            var result = await dataAccessService.IncrementDislikes(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void IncrementDislikes_ReturnsFalse_OnError()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(response);

            var client = new HttpClient(handler.Object)
            {
                BaseAddress = new Uri("https://test.com/")
            };
            var dataAccessService = new DataService(client);

            // Act
            var result = await dataAccessService.IncrementDislikes(1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void DecrementDislikes_ReturnsTrue_OnSuccess()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.NoContent);

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(response);

            var client = new HttpClient(handler.Object)
            {
                BaseAddress = new Uri("https://test.com/")
            };
            var dataAccessService = new DataService(client);

            // Act
            var result = await dataAccessService.DecrementDislikes(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void DecrementDislikes_ReturnsFalse_OnError()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(response);

            var client = new HttpClient(handler.Object)
            {
                BaseAddress = new Uri("https://test.com/")
            };
            var dataAccessService = new DataService(client);

            // Act
            var result = await dataAccessService.DecrementDislikes(1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void DeleteByUser_ReturnsTrue_OnSuccess()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.NoContent);

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(response);

            var client = new HttpClient(handler.Object)
            {
                BaseAddress = new Uri("https://test.com/")
            };
            var dataAccessService = new DataService(client);

            // Act
            var result = await dataAccessService.DeleteByUser(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void DeleteByUser_ReturnsFalse_OnError()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(response);

            var client = new HttpClient(handler.Object)
            {
                BaseAddress = new Uri("https://test.com/")
            };
            var dataAccessService = new DataService(client);

            // Act
            var result = await dataAccessService.DeleteByUser(1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void DeleteByPost_ReturnsTrue_OnSuccess()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.NoContent);

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(response);

            var client = new HttpClient(handler.Object)
            {
                BaseAddress = new Uri("https://test.com/")
            };
            var dataAccessService = new DataService(client);

            // Act
            var result = await dataAccessService.DeleteByPost(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void DeleteByPost_ReturnsFalse_OnSuccess()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(response);

            var client = new HttpClient(handler.Object)
            {
                BaseAddress = new Uri("https://test.com/")
            };
            var dataAccessService = new DataService(client);

            // Act
            var result = await dataAccessService.DeleteByPost(1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void DeleteComment_ReturnsTrue_OnSuccess()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.NoContent);

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(response);

            var client = new HttpClient(handler.Object)
            {
                BaseAddress = new Uri("https://test.com/")
            };
            var dataAccessService = new DataService(client);

            // Act
            var result = await dataAccessService.DeleteComment(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void DeleteComment_ReturnsFalse_OnError()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(response);

            var client = new HttpClient(handler.Object)
            {
                BaseAddress = new Uri("https://test.com/")
            };
            var dataAccessService = new DataService(client);

            // Act
            var result = await dataAccessService.DeleteComment(1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void CreateComment_ReturnsTrue_On200StatusCode()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(response);

            var client = new HttpClient(handler.Object)
            {
                BaseAddress = new Uri("https://test.com/")
            };
            var dataAccessService = new DataService(client);

            // Act
            var result = await dataAccessService.CreateComment(new Comment());

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void CreateComment_ReturnsFalse_On400StatusCode()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(response);

            var client = new HttpClient(handler.Object)
            {
                BaseAddress = new Uri("https://test.com/")
            };
            var dataAccessService = new DataService(client);

            // Act
            var result = await dataAccessService.CreateComment(new Comment());

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void EditCommentContent_ReturnsTrue_On204StatusCode()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.NoContent);

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(response);

            var client = new HttpClient(handler.Object)
            {
                BaseAddress = new Uri("https://test.com/")
            };
            var dataAccessService = new DataService(client);

            // Act
            var result = await dataAccessService.EditCommentContent(1, new Comment());

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void EditCommentContent_ReturnsFalse_On404StatusCode()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(response);

            var client = new HttpClient(handler.Object)
            {
                BaseAddress = new Uri("https://test.com/")
            };
            var dataAccessService = new DataService(client);

            // Act
            var result = await dataAccessService.EditCommentContent(1, new Comment());

            // Assert
            Assert.False(result);
        }
    }
}