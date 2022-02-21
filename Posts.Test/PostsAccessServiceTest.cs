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
    public class PostsAccessServiceTest
    {
        public PostsAccessServiceTest()
        {
            Environment.SetEnvironmentVariable("DATA_BASE_URL", "https://test.com/");
        }

        [Fact]
        public async void CreatePost_ReturnsTrue_On204Success()
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

            var postAccessService = new PostsAccessService(client);

            // Act
            var result = await postAccessService.CreatePost(new Post());

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void CreatePost_ReturnsFalse_On404Error()
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

            PostsAccessService postAccessService = new PostsAccessService(client);

            // Act
            var result = await postAccessService.CreatePost(new Post());

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void DecrementDislikes_ReturnsTrue_On204Success()
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

            PostsAccessService postAccessService = new PostsAccessService(client);

            // Act
            var result = await postAccessService.DecrementDislikes(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void DecrementDislikes_ReturnsFalse_On404NotFound()
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

            PostsAccessService postAccessService = new PostsAccessService(client);

            // Act
            var result = await postAccessService.DecrementDislikes(1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void DecrementLikes_ReturnsTrue_On204Success()
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

            PostsAccessService postAccessService = new PostsAccessService(client);

            // Act
            var result = await postAccessService.DecrementLikes(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void DecrementLikes_ReturnsFalse_On404NotFound()
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

            PostsAccessService postAccessService = new PostsAccessService(client);

            // Act
            var result = await postAccessService.DecrementLikes(1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void DeleteByUser_ReturnsTrue_On204NotFound()
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

            PostsAccessService postAccessService = new PostsAccessService(client);

            // Act
            var result = await postAccessService.DeleteByUser(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void DeleteByUser_ReturnsFalse_On404NotFound()
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

            PostsAccessService postAccessService = new PostsAccessService(client);

            // Act
            var result = await postAccessService.DeleteByUser(1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void DeletePost_ReturnsTrue_On204Success()
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

            PostsAccessService postAccessService = new PostsAccessService(client);

            // Act
            var result = await postAccessService.DeletePost(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void DeletePost_ReturnsFalse_On404NNotFound()
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

            PostsAccessService postAccessService = new PostsAccessService(client);

            // Act
            var result = await postAccessService.DeletePost(1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void EditPost_ReturnsTrue_On204NoContent()
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

            PostsAccessService postAccessService = new PostsAccessService(client);

            // Act
            var result = await postAccessService.EditPost(1, new Post());

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void EditPost_ReturnsFalse_On404NotFound()
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

            PostsAccessService postAccessService = new PostsAccessService(client);

            // Act
            var result = await postAccessService.EditPost(1, new Post());

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void GetAllPosts_ReturnsListOfPosts_OnSuccess()
        {
            // Arrange
            string asJson = JsonSerializer.Serialize<List<Post>>(new List<Post>() 
            {
                new Post(),
                new Post()
            });

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(asJson)
            };

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

            PostsAccessService postAccessService = new PostsAccessService(client);

            // Act
            var result = await postAccessService.GetAllPosts();

            // Assert
            Assert.IsType<List<Post>>(result);
        }

        [Fact]
        public async void GetAllPosts_ReturnsNull_OnEmptyList()
        {
            // Arrange
            var asJson = JsonSerializer.Serialize<List<Post>>(new List<Post>());

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(asJson)
            };

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

            PostsAccessService postAccessService = new PostsAccessService(client);

            // Act
            var result = await postAccessService.GetAllPosts();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void GetAllPosts_ReturnsNull_OnNullReturn()
        {
            // Arrange
            var asJson = JsonSerializer.Serialize<List<Post>>(new List<Post>());

            HttpResponseMessage response = null;

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

            PostsAccessService postAccessService = new PostsAccessService(client);

            // Act
            var result = await postAccessService.GetAllPosts();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void GetPostsFromUser_ReturnsList_OnSuccess()
        {
            // Arrange
            string asJson = JsonSerializer.Serialize<List<Post>>(new List<Post>()
            {
                new Post(),
                new Post()
            });

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(asJson)
            };

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

            PostsAccessService postAccessService = new PostsAccessService(client);

            // Act
            var result = await postAccessService.GetPostsFromUser(1);

            // Assert
            Assert.IsType<List<Post>>(result);
        }

        [Fact]
        public async void GetPostsFromUser_ReturnsNull_OnEmptyList()
        {
            // Arrange
            string asJson = JsonSerializer.Serialize<List<Post>>(new List<Post>());

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(asJson)
            };

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

            PostsAccessService postAccessService = new PostsAccessService(client);

            // Act
            var result = await postAccessService.GetPostsFromUser(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void GetPostsFromUser_ReturnsNull_OnNullReturn()
        {
            // Arrange
            string asJson = JsonSerializer.Serialize<List<Post>>(new List<Post>());

            HttpResponseMessage response = null;

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

            PostsAccessService postAccessService = new PostsAccessService(client);

            // Act
            var result = await postAccessService.GetPostsFromUser(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void GetSinglePost_ReturnsPost_OnSuccess()
        {
            // Arrange
            string asJson = JsonSerializer.Serialize<Post>(new Post());

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(asJson)
            };

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

            PostsAccessService postAccessService = new PostsAccessService(client);

            // Act
            var result = await postAccessService.GetSinglePost(1);

            // Assert
            Assert.IsType<Post>(result);
        }

        [Fact]
        public async void GetSinglePost_ReturnsNull_OnNotFound()
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

            PostsAccessService postAccessService = new PostsAccessService(client);

            // Act
            var result = await postAccessService.GetSinglePost(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void IncrementDislikes_ReturnsTrue_On204Success()
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

            PostsAccessService postAccessService = new PostsAccessService(client);

            // Act
            var result = await postAccessService.IncrementDislikes(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void IncrementDislikes_ReturnsFalse_On404NotFound()
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

            PostsAccessService postAccessService = new PostsAccessService(client);

            // Act
            var result = await postAccessService.IncrementDislikes(1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void IncrementLikes_ReturnsTrue_On204NoContent()
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

            PostsAccessService postAccessService = new PostsAccessService(client);

            // Act
            var result = await postAccessService.IncrementLikes(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void IncrementLikes_ReturnsFalse_On404NotFound()
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

            PostsAccessService postAccessService = new PostsAccessService(client);

            // Act
            var result = await postAccessService.IncrementLikes(1);

            // Assert
            Assert.False(result);
        }
    }
}