using Models;
using Moq;
using Moq.Protected;
using Posts.Service;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Posts.Test
{
    public class CommentsInteractionServiceTest
    {
        public CommentsInteractionServiceTest()
        {
            Environment.SetEnvironmentVariable("COMMENTS_BASE_URL", "https://localhost:7261/CommentsService/");
        }

        [Fact]
        public async void DeleteCommentsById_ReturnsTrue_On204Success()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.NoContent);

            var http = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            http
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(response);

            var client = new HttpClient(http.Object)
            {
                BaseAddress = new Uri("https://test.com/")
            };

            var postAccessService = new CommentsInteractionService(client);

            // Act
            var result = await postAccessService.DeleteCommentsById("123-123-abc");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void DeleteCommentsById_ReturnsFalse_On404NotFound()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);

            var http = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            http
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(response);

            var client = new HttpClient(http.Object)
            {
                BaseAddress = new Uri("https://test.com/")
            };

            var postAccessService = new CommentsInteractionService(client);

            // Act
            var result = await postAccessService.DeleteCommentsById("123-123-abc");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void DeleteCommentsByPost_ReturnsTrue_On204Success()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.NoContent);

            var http = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            http
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(response);

            var client = new HttpClient(http.Object)
            {
                BaseAddress = new Uri("https://test.com/")
            };

            var postAccessService = new CommentsInteractionService(client);

            // Act
            var result = await postAccessService.DeleteCommentsByPost(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void DeleteCommentsByPost_ReturnsFalse_On404NotFound()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);

            var http = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            http
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(response);

            var client = new HttpClient(http.Object)
            {
                BaseAddress = new Uri("https://test.com/")
            };

            var postAccessService = new CommentsInteractionService(client);

            // Act
            var result = await postAccessService.DeleteCommentsByPost(1);

            // Assert
            Assert.False(result);
        }
    }
}
