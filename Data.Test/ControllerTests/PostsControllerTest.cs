using Models;
using Xunit;
using System;
using System.Collections.Generic;
using Moq;
using Data.Controllers;
using Data.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace Data.Test.ControllerTests
{
    public class PostsControllerTest
    {
        [Fact]
        public async void GetAllPosts_ReturnsPosts()
        {
            // Arrange
            var posts = new List<Post>()
            {
                new Post()
                {
                    Id = 1,
                    Content = "test post",
                    Created = DateTime.Now,
                    CreatedBy = "123-123-abc",
                    Title = "test title",
                    Likes = 1,
                    Dislikes = 1,
                },
                new Post()
                {
                    Id = 2,
                    Content = "test post",
                    Created = DateTime.Now,
                    CreatedBy = "123-123-abc",
                    Title = "test title",
                    Likes = 1,
                    Dislikes = 1,
                }
            };
            var access = new Mock<PostAccess>(null);
            access.Setup(a => a.GetAllPosts())
                .ReturnsAsync(posts);
            var controller = new PostsController(null, access.Object);

            // Act
            var result = await controller.GetAllPosts();

            // Assert
            Assert.Equal(2, result.Value.Count);
        }

        [Fact]
        public async void GetAllPosts_Returns404_WhenNoPostsFound()
        {
            // Arrange
            var emptyPosts = new List<Post>();
            var access = new Mock<PostAccess>(null);
            access.Setup(a => a.GetAllPosts())
                .ReturnsAsync(emptyPosts);
            var controller = new PostsController(null, access.Object);

            // Act
            var result = await controller.GetAllPosts();

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void GetPostsByUser_ReturnsPosts()
        {
            // Arrange
            var posts = new List<Post>()
            {
                new Post()
                {
                    Id = 1,
                    Content = "test post",
                    Created = DateTime.Now,
                    CreatedBy = "123-123-abc",
                    Title = "test title",
                    Likes = 1,
                    Dislikes = 1,
                },
                new Post()
                {
                    Id = 2,
                    Content = "test post",
                    Created = DateTime.Now,
                    CreatedBy = "123-123-abc",
                    Title = "test title",
                    Likes = 1,
                    Dislikes = 1,
                }
            };
            var access = new Mock<PostAccess>(null);
            access.Setup(a => a.GetPostsFromUser(It.IsAny<string>()))
                .ReturnsAsync(posts);
            var controller = new PostsController(null, access.Object);

            // Act
            var result = await controller.GetPostsByUser("123-123-abc");

            // Assert
            Assert.Equal(2, result.Value.Count);
        }

        [Fact]
        public async void GetPostsByUser_Returns404_OnPostsNotFound()
        {
            // Arrange
            var emptyPosts = new List<Post>();
            var access = new Mock<PostAccess>(null);
            access.Setup(a => a.GetPostsFromUser(It.IsAny<string>()))
                .ReturnsAsync(emptyPosts);
            var controller = new PostsController(null, access.Object);

            // Act
            var result = await controller.GetPostsByUser("123-123-abc");

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void GetSinglePost_ReturnsSinglePost()
        {
            // Arrange
            var post = new Post()
            {
                    Id = 1,
                    Content = "test post",
                    Created = DateTime.Now,
                    CreatedBy = "123-123-abc",
                    Title = "test title",
                    Likes = 1,
                    Dislikes = 1,
                
            };
            var access = new Mock<PostAccess>(null);
            access.Setup(a => a.GetSinglePost(It.IsAny<int>()))
                .ReturnsAsync(post);
            var controller = new PostsController(null, access.Object);

            // Act
            var result = await controller.GetPostById(1);

            // Assert
            Assert.IsType<Post>(result.Value);
        }

        [Fact]
        public async void GetSinglePost_Returns404_OnPostNotFound()
        {
            // Arrange
            var emptyPosts = new Post();
            var access = new Mock<PostAccess>(null);
            access.Setup(a => a.GetSinglePost(It.IsAny<int>()))
                .ThrowsAsync(new IdNotFoundException(1));
            var controller = new PostsController(null, access.Object);

            // Act
            var result = await controller.GetPostById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void DeletePost_Returns204_OnSuccess()
        {
            // Arrange
            bool success = true;
            var access = new Mock<PostAccess>(null);
            access.Setup(a => a.DeletePost(It.IsAny<int>()))
                .ReturnsAsync(success);
            var controller = new PostsController(null, access.Object);

            // Act
            var result = await controller.DeleteByPostId(1);

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async void DeletePost_Returns404_OnNotFound()
        {
            // Arrange
            bool success = false;
            var access = new Mock<PostAccess>(null);
            access.Setup(a => a.DeletePost(It.IsAny<int>()))
                .ReturnsAsync(success);
            var controller = new PostsController(null, access.Object);

            // Act
            var result = await controller.DeleteByPostId(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void DeleteByUser_Returns204_OnSuccess()
        {
            // Arrange
            bool success = true;
            var access = new Mock<PostAccess>(null);
            access.Setup(a => a.DeleteByUser(It.IsAny<string>()))
                .ReturnsAsync(success);
            var controller = new PostsController(null, access.Object);

            // Act
            var result = await controller.DeletePostsFromUser("123-123-abc");

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async void DeleteByUser_Returns404_OnError()
        {
            // Arrange
            bool success = false;
            var access = new Mock<PostAccess>(null);
            access.Setup(a => a.DeleteByUser(It.IsAny<string>()))
                .ReturnsAsync(success);
            var controller = new PostsController(null, access.Object);

            // Act
            var result = await controller.DeletePostsFromUser("123-123-abc");

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void CreatePost_Returns204_OnCreated()
        {
            // Arrange
            bool success = true;
            var access = new Mock<PostAccess>(null);
            access.Setup(a => a.CreatePost(It.IsAny<Post>()))
                .ReturnsAsync(success);
            var controller = new PostsController(null, access.Object);

            // Act
            var result = await controller.CreateNewPost(new Post());

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async void CreatePost_Returns404_OnError()
        {
            // Arrange
            bool success = false;
            var access = new Mock<PostAccess>(null);
            access.Setup(a => a.CreatePost(It.IsAny<Post>()))
                .ReturnsAsync(success);
            var controller = new PostsController(null, access.Object);

            // Act
            var result = await controller.CreateNewPost(new Post());

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void IncrementLikes_Returns204_OnSuccess()
        {
            // Arrange
            bool success = true;
            var access = new Mock<PostAccess>(null);
            access.Setup(a => a.IncrementLikes(It.IsAny<int>()))
                .ReturnsAsync(success);
            var controller = new PostsController(null, access.Object);

            // Act
            var result = await controller.IncrementLikes(1);

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async void IncrementLikes_Returns404_OnError()
        {
            // Arrange
            bool success = false;
            var access = new Mock<PostAccess>(null);
            access.Setup(a => a.IncrementLikes(It.IsAny<int>()))
                .ReturnsAsync(success);
            var controller = new PostsController(null, access.Object);

            // Act
            var result = await controller.IncrementLikes(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void DecrementLikes_Returns204_OnSuccess()
        {
            // Arrange
            bool success = true;
            var access = new Mock<PostAccess>(null);
            access.Setup(a => a.DecrementLikes(It.IsAny<int>()))
                .ReturnsAsync(success);
            var controller = new PostsController(null, access.Object);

            // Act
            var result = await controller.DecrementLikes(1);

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async void DecrementLikes_Returns404_OnError()
        {
            // Arrange
            bool success = false;
            var access = new Mock<PostAccess>(null);
            access.Setup(a => a.DecrementLikes(It.IsAny<int>()))
                .ReturnsAsync(success);
            var controller = new PostsController(null, access.Object);

            // Act
            var result = await controller.DecrementLikes(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void IncrementDislikes_Returns204_OnSuccess()
        {
            // Arrange
            bool success = true;
            var access = new Mock<PostAccess>(null);
            access.Setup(a => a.IncrementDislikes(It.IsAny<int>()))
                .ReturnsAsync(success);
            var controller = new PostsController(null, access.Object);

            // Act
            var result = await controller.IncrementDislikes(1);

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async void IncrementDislikes_Returns404_OnError()
        {
            // Arrange
            bool success = false;
            var access = new Mock<PostAccess>(null);
            access.Setup(a => a.IncrementDislikes(It.IsAny<int>()))
                .ReturnsAsync(success);
            var controller = new PostsController(null, access.Object);

            // Act
            var result = await controller.IncrementDislikes(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void DecrementDislikes_Returns204_OnSuccess()
        {
            // Arrange
            bool success = true;
            var access = new Mock<PostAccess>(null);
            access.Setup(a => a.DecrementDislikes(It.IsAny<int>()))
                .ReturnsAsync(success);
            var controller = new PostsController(null, access.Object);

            // Act
            var result = await controller.DecrementDislikes(1);

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async void DecrementDislikes_Returns404_OnError()
        {
            // Arrange
            bool success = false;
            var access = new Mock<PostAccess>(null);
            access.Setup(a => a.DecrementDislikes(It.IsAny<int>()))
                .ReturnsAsync(success);
            var controller = new PostsController(null, access.Object);

            // Act
            var result = await controller.DecrementDislikes(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void EditPost_Returns204_OnSuccess()
        {
            // Arrange
            bool success = true;
            var access = new Mock<PostAccess>(null);
            access.Setup(a => a.EditPost(It.IsAny<int>(), It.IsAny<Post>()))
                .ReturnsAsync(success);
            var controller = new PostsController(null, access.Object);

            // Act
            var result = await controller.EditPost(1, new Post());

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async void EditPost_Returns404_OnError()
        {
            // Arrange
            bool success = false;
            var access = new Mock<PostAccess>(null);
            access.Setup(a => a.EditPost(It.IsAny<int>(), It.IsAny<Post>()))
                .ReturnsAsync(success);
            var controller = new PostsController(null, access.Object);

            // Act
            var result = await controller.EditPost(1, new Post());

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
