using Models;
using Xunit;
using System;
using System.Collections.Generic;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Posts.Service;
using Posts.Controllers;
using System.Net.Http;

namespace Posts.Test
{
    public class PostsControllerTest
    {
        private HttpClient client;

        public PostsControllerTest()
        {
            Environment.SetEnvironmentVariable("DATA_BASE_URL", "https://test.com/");
            client = new HttpClient();
        }

        [Fact]
        public async void DeleteByUser_Returns204_OnSuccess()
        {
            // Arrange
            bool success = true;
            var postService = new Mock<PostsAccessService>(client);

            postService
                .Setup(x => x.DeleteByUser(It.IsAny<string>()))
                .ReturnsAsync(success);

            var controller = new PostsController(postService.Object);

            // Act
            var result = await controller.DeleteByUser("123-123-abc");

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void DeleteByUser_Returns404_OnFailure()
        {
            // Arrange
            bool success = false;
            var postService = new Mock<PostsAccessService>(client);

            postService
                .Setup(x => x.DeleteByUser(It.IsAny<string>()))
                .ReturnsAsync(success);

            var controller = new PostsController(postService.Object);

            // Act
            var result = await controller.DeleteByUser("123-123-abc");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void DeleteByPost_Returns204_OnSuccess()
        {
            // Arrange
            bool success = true;
            var postService = new Mock<PostsAccessService>(client);

            postService
                .Setup(x => x.DeletePost(It.IsAny<int>()))
                .ReturnsAsync(success);

            var controller = new PostsController(postService.Object);

            // Act
            var result = await controller.DeleteByPost(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void DeleteByPost_Returns404_OnError()
        {
            // Arrange
            bool success = false;
            var postService = new Mock<PostsAccessService>(client);

            postService
                .Setup(x => x.DeletePost(It.IsAny<int>()))
                .ReturnsAsync(success);

            var controller = new PostsController(postService.Object);

            // Act
            var result = await controller.DeleteByPost(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void CreatePost_Returns204_OnSuccess()
        {
            // Arrange
            bool success = true;
            var postService = new Mock<PostsAccessService>(client);

            postService
                .Setup(x => x.CreatePost(It.IsAny<Post>()))
                .ReturnsAsync(success);

            var controller = new PostsController(postService.Object);

            // Act
            var result = await controller.CreatePost(new Post());

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void CreatePost_Returns404_OnFailure()
        {
            // Arrange
            bool success = false;
            var postService = new Mock<PostsAccessService>(client);

            postService
                .Setup(x => x.CreatePost(It.IsAny<Post>()))
                .ReturnsAsync(success);

            var controller = new PostsController(postService.Object);

            // Act
            var result = await controller.CreatePost(new Post());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void DecrementDislikes_Returns204_OnSuccess()
        {
            // Arrange
            bool success = true;
            var postService = new Mock<PostsAccessService>(client);

            postService
                .Setup(x => x.DecrementDislikes(It.IsAny<int>()))
                .ReturnsAsync(success);

            var controller = new PostsController(postService.Object);

            // Act
            var result = await controller.DecrementDislikes(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void DecrementDislikes_Returns404_OnFailure()
        {
            // Arrange
            bool success = false;
            var postService = new Mock<PostsAccessService>(client);

            postService
                .Setup(x => x.DecrementDislikes(It.IsAny<int>()))
                .ReturnsAsync(success);

            var controller = new PostsController(postService.Object);

            // Act
            var result = await controller.DecrementDislikes(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void DecrementLikes_Returns204_OnSuccess()
        {
            // Arrange
            bool success = true;
            var postService = new Mock<PostsAccessService>(client);

            postService
                .Setup(x => x.DecrementLikes(It.IsAny<int>()))
                .ReturnsAsync(success);

            var controller = new PostsController(postService.Object);

            // Act
            var result = await controller.DecrementLikes(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void DecrementLikes_Returns404_OnFailure()
        {
            // Arrange
            bool success = false;
            var postService = new Mock<PostsAccessService>(client);

            postService
                .Setup(x => x.DecrementLikes(It.IsAny<int>()))
                .ReturnsAsync(success);

            var controller = new PostsController(postService.Object);

            // Act
            var result = await controller.DecrementLikes(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void EditPost_Returns204_OnSuccess()
        {
            // Arrange
            bool success = true;
            var postService = new Mock<PostsAccessService>(client);

            postService
                .Setup(x => x.EditPost(It.IsAny<int>(), It.IsAny<Post>()))
                .ReturnsAsync(success);

            var controller = new PostsController(postService.Object);

            // Act
            var result = await controller.EditPost(1, new Post());

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void EditPost_Returns404_OnFailure()
        {
            // Arrange
            bool success = false;
            var postService = new Mock<PostsAccessService>(client);

            postService
                .Setup(x => x.EditPost(It.IsAny<int>(), It.IsAny<Post>()))
                .ReturnsAsync(success);

            var controller = new PostsController(postService.Object);

            // Act
            var result = await controller.EditPost(1, new Post());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void GetAllPosts_ReturnsPost_OnSuccess()
        {
            // Arrange
            List<Post> posts = new List<Post>()
            {
                new Post(),
                new Post()
            };
            var postService = new Mock<PostsAccessService>(client);

            postService
                .Setup(x => x.GetAllPosts())
                .ReturnsAsync(posts);

            var controller = new PostsController(postService.Object);

            // Act
            var result = await controller.GetAllPosts();

            // Assert
            Assert.IsType<List<Post>>(result.Value);
        }

        [Fact]
        public async void GetAllPosts_Returns404_OnFailure()
        {
            // Arrange
            List<Post> posts = null;
            var postService = new Mock<PostsAccessService>(client);

            postService
                .Setup(x => x.GetAllPosts())
                .ReturnsAsync(posts);

            var controller = new PostsController(postService.Object);

            // Act
            var result = await controller.GetAllPosts();

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void GetPostFromUser_ReturnsPosts_OnSuccess()
        {
            // Arrange
            List<Post> posts = new List<Post>()
            {
                new Post(),
                new Post()
            };
            var postService = new Mock<PostsAccessService>(client);

            postService
                .Setup(x => x.GetPostsFromUser(It.IsAny<string>()))
                .ReturnsAsync(posts);

            var controller = new PostsController(postService.Object);

            // Act
            var result = await controller.GetPostsFromUser("123-123-abc");

            // Assert
            Assert.IsType<List<Post>>(result.Value);
        }

        [Fact]
        public async void GetPostsFromUser_Returns404_OnFailure()
        {
            // Arrange
            List<Post> posts = null;
            var postService = new Mock<PostsAccessService>(client);

            postService
                .Setup(x => x.GetPostsFromUser(It.IsAny<string>()))
                .ReturnsAsync(posts);

            var controller = new PostsController(postService.Object);

            // Act
            var result = await controller.GetPostsFromUser("123-123-abc");

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void GetSinglePost_ReturnsSinglePost_OnSuccess()
        {
            // Arrange
            Post post = new Post();
            var postService = new Mock<PostsAccessService>(client);

            postService
                .Setup(x => x.GetSinglePost(It.IsAny<int>()))
                .ReturnsAsync(post);

            var controller = new PostsController(postService.Object);

            // Act
            var result = await controller.GetSinglePost(1);

            // Assert
            Assert.IsType<Post>(result.Value);
        }

        [Fact]
        public async void GetSinglePost_Returns404_OnError()
        {
            // Arrange
            Post post = null;
            var postService = new Mock<PostsAccessService>(client);

            postService
                .Setup(x => x.GetSinglePost(It.IsAny<int>()))
                .ReturnsAsync(post);

            var controller = new PostsController(postService.Object);

            // Act
            var result = await controller.GetSinglePost(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void IncrementDislikes_Returns204_OnSuccess()
        {
            // Arrange
            bool success = true;
            var postService = new Mock<PostsAccessService>(client);

            postService
                .Setup(x => x.IncrementDislikes(It.IsAny<int>()))
                .ReturnsAsync(success);

            var controller = new PostsController(postService.Object);

            // Act
            var result = await controller.IncrementDislikes(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void IncrementDislikes_Returns404_OnFailure()
        {
            // Arrange
            bool success = false;
            var postService = new Mock<PostsAccessService>(client);

            postService
                .Setup(x => x.IncrementDislikes(It.IsAny<int>()))
                .ReturnsAsync(success);

            var controller = new PostsController(postService.Object);

            // Act
            var result = await controller.IncrementDislikes(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void IncrementLikes_Returns204_OnSuccess()
        {
            // Arrange
            bool success = true;
            var postService = new Mock<PostsAccessService>(client);

            postService
                .Setup(x => x.IncrementLikes(It.IsAny<int>()))
                .ReturnsAsync(success);

            var controller = new PostsController(postService.Object);

            // Act
            var result = await controller.IncrementLikes(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void IncrementLikes_Returns404_OnFailure()
        {
            // Arrange
            bool success = false;
            var postService = new Mock<PostsAccessService>(client);

            postService
                .Setup(x => x.IncrementLikes(It.IsAny<int>()))
                .ReturnsAsync(success);

            var controller = new PostsController(postService.Object);

            // Act
            var result = await controller.IncrementLikes(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
