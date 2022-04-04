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
using Comments.Controllers;

namespace Comments.Test
{
    public class CommentsServiceControllerTest
    {

        public CommentsServiceControllerTest()
        {
            Environment.SetEnvironmentVariable("DATA_BASEURL", "https://localhost:7293/comments/");
        }

        [Fact]
        public async void GetByUser_ReturnsListOfComments_OnSuccess()
        {
            // Arrange
            var comments = new List<Comment>()
            {
                new Comment(), 
                new Comment(),
            };
            
            var dataService = new Mock<DataService>(new HttpClient());
            dataService.Setup(d => d.GetByUser(It.IsAny<string>()))
                .Returns(comments);

            var controller = new CommentsServiceController(dataService.Object);

            // Act
            var response = await controller.GetByUser("123-123-abc");

            // Assert
            Assert.IsType<List<Comment>>(response.Value);
        }

        [Fact]
        public async void GetByUser_Returns404Code_OnNoneFound()
        {
            // Arrange
            List<Comment> comments = null;
            var dataService = new Mock<DataService>(new HttpClient());
            dataService.Setup(d => d.GetByUser(It.IsAny<string>()))
                .Returns(comments);

            var controller = new CommentsServiceController(dataService.Object);

            // Act
            var response = await controller.GetByUser("123-123-abc");

            // Assert
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public async void GetByPost_ReturnsListOfComments_OnSuccess()
        {
            // Arrange
            var comments = new List<Comment>()
            {
                new Comment(),
                new Comment(),
            };
            var dataService = new Mock<DataService>(new HttpClient());
            dataService.Setup(d => d.GetByPost(It.IsAny<int>()))
                .Returns(comments);

            var controller = new CommentsServiceController(dataService.Object);

            // Act
            var response = await controller.GetByPost(0);

            // Assert
            Assert.IsType<List<Comment>>(response.Value);
        }

        [Fact]
        public async void GetByPost_Returns404_OnError()
        {
            // Arrange
            List<Comment> returnValue = null;
            var dataService = new Mock<DataService>(new HttpClient());
            dataService.Setup(d => d.GetByPost(It.IsAny<int>()))
                .Returns(returnValue);

            var controller = new CommentsServiceController(dataService.Object);

            // Act
            var response = await controller.GetByPost(0);

            // Assert
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public async void CreateComment_Returns204_OnSuccess()
        {
            // Arrange
            var success = true;
            var dataService = new Mock<DataService>(new HttpClient());
            dataService.Setup(d => d.CreateComment(It.IsAny<Comment>()))
                .ReturnsAsync(success);

            var controller = new CommentsServiceController(dataService.Object);

            // Act
            var response = await controller.CreateComment(new Comment());

            // Assert
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public async void CreateComment_Returns404_OnError()
        {
            // Arrange
            var success = false;
            var dataService = new Mock<DataService>(new HttpClient());
            dataService.Setup(d => d.CreateComment(It.IsAny<Comment>()))
                .ReturnsAsync(success);

            var controller = new CommentsServiceController(dataService.Object);

            // Act
            var response = await controller.CreateComment(new Comment());

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async void DecrementDislikes_Returns204_OnSuccess()
        {
            // Arrange
            var success = true;
            var dataService = new Mock<DataService>(new HttpClient());
            dataService.Setup(d => d.DecrementDislikes(It.IsAny<int>()))
                .ReturnsAsync(success);

            var controller = new CommentsServiceController(dataService.Object);

            // Act
            var response = await controller.DecrementDislikes(0);

            // Assert
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public async void DecrementDislikes_Returns404_OnError()
        {
            // Arrange
            var success = false;
            var dataService = new Mock<DataService>(new HttpClient());
            dataService.Setup(d => d.DecrementDislikes(It.IsAny<int>()))
                .ReturnsAsync(success);

            var controller = new CommentsServiceController(dataService.Object);

            // Act
            var response = await controller.DecrementDislikes(0);

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async void DecrementLikes_Returns204_OnSuccess()
        {
            // Arrange
            var success = true;
            var dataService = new Mock<DataService>(new HttpClient());
            dataService.Setup(d => d.DecrementLikes(It.IsAny<int>()))
                .ReturnsAsync(success);

            var controller = new CommentsServiceController(dataService.Object);

            // Act
            var response = await controller.DecrementLikes(0);

            // Assert
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public async void DecrementLikes_Returns404_OnError()
        {
            // Arrange
            var success = false;
            var dataService = new Mock<DataService>(new HttpClient());
            dataService.Setup(d => d.DecrementLikes(It.IsAny<int>()))
                .ReturnsAsync(success);

            var controller = new CommentsServiceController(dataService.Object);

            // Act
            var response = await controller.DecrementLikes(0);

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async void DeleteByPost_Returns204_OnSuccess()
        {
            // Arrange
            var success = true;
            var dataService = new Mock<DataService>(new HttpClient());
            dataService.Setup(d => d.DeleteByPost(It.IsAny<int>()))
                .ReturnsAsync(success);

            var controller = new CommentsServiceController(dataService.Object);

            // Act
            var response = await controller.DeleteByPost(0);

            // Assert
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public async void DeleteByPost_Returns404_OnError()
        {
            // Arrange
            var success = false;
            var dataService = new Mock<DataService>(new HttpClient());
            dataService.Setup(d => d.DeleteByPost(It.IsAny<int>()))
                .ReturnsAsync(success);

            var controller = new CommentsServiceController(dataService.Object);

            // Act
            var response = await controller.DeleteByPost(0);

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async void DeleteByUser_Returns204_OnSuccess()
        {
            // Arrange
            var success = true;
            var dataService = new Mock<DataService>(new HttpClient());
            dataService.Setup(d => d.DeleteByUser(It.IsAny<string>()))
                .ReturnsAsync(success);

            var controller = new CommentsServiceController(dataService.Object);

            // Act
            var response = await controller.DeleteByUser("123-123-abc");

            // Assert
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public async void DeleteByUser_Returns404_OnError()
        {
            // Arrange
            var success = false;
            var dataService = new Mock<DataService>(new HttpClient());
            dataService.Setup(d => d.DeleteByUser(It.IsAny<string>()))
                .ReturnsAsync(success);

            var controller = new CommentsServiceController(dataService.Object);

            // Act
            var response = await controller.DeleteByUser("123-123-abc");

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async void DeleteComment_Returns204_OnSuccess()
        {
            // Arrange
            var success = true;
            var dataService = new Mock<DataService>(new HttpClient());
            dataService.Setup(d => d.DeleteComment(It.IsAny<int>()))
                .ReturnsAsync(success);

            var controller = new CommentsServiceController(dataService.Object);

            // Act
            var response = await controller.DeleteComment(0);

            // Assert
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public async void DeleteComment_Returns404_OnError()
        {
            // Arrange
            var success = false;
            var dataService = new Mock<DataService>(new HttpClient());
            dataService.Setup(d => d.DeleteComment(It.IsAny<int>()))
                .ReturnsAsync(success);

            var controller = new CommentsServiceController(dataService.Object);

            // Act
            var response = await controller.DeleteComment(0);

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async void EditCommentContent_Returns204_OnSuccess()
        {
            // Arrange
            var success = true;
            var dataService = new Mock<DataService>(new HttpClient());
            dataService.Setup(d => d.EditCommentContent(It.IsAny<int>(), It.IsAny<Comment>()))
                .ReturnsAsync(success);

            var controller = new CommentsServiceController(dataService.Object);

            // Act
            var response = await controller.EditCommentContent(0, new Comment());

            // Assert
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public async void EditCommentContent_Returns404_OnError()
        {
            // Arrange
            var success = false;
            var dataService = new Mock<DataService>(new HttpClient());
            dataService.Setup(d => d.EditCommentContent(It.IsAny<int>(), It.IsAny<Comment>()))
                .ReturnsAsync(success);

            var controller = new CommentsServiceController(dataService.Object);

            // Act
            var response = await controller.EditCommentContent(0, new Comment());

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async void IncrementDislikes_Returns204_OnSuccess()
        {
            // Arrange
            var success = true;
            var dataService = new Mock<DataService>(new HttpClient());
            dataService.Setup(d => d.IncrementDislikes(It.IsAny<int>()))
                .ReturnsAsync(success);

            var controller = new CommentsServiceController(dataService.Object);

            // Act
            var response = await controller.IncrementDislikes(0);

            // Assert
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public async void IncrementDislikes_Returns404_OnError()
        {
            // Arrange
            var success = false;
            var dataService = new Mock<DataService>(new HttpClient());
            dataService.Setup(d => d.IncrementDislikes(It.IsAny<int>()))
                .ReturnsAsync(success);

            var controller = new CommentsServiceController(dataService.Object);

            // Act
            var response = await controller.IncrementDislikes(0);

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async void IncrementLikes_Returns204_OnSuccess()
        {
            // Arrange
            var success = true;
            var dataService = new Mock<DataService>(new HttpClient());
            dataService.Setup(d => d.IncrementLikes(It.IsAny<int>()))
                .ReturnsAsync(success);

            var controller = new CommentsServiceController(dataService.Object);

            // Act
            var response = await controller.IncrementLikes(0);

            // Assert
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public async void IncrementLikes_Returns404_OnError()
        {
            // Arrange
            var success = false;
            var dataService = new Mock<DataService>(new HttpClient());
            dataService.Setup(d => d.IncrementLikes(It.IsAny<int>()))
                .ReturnsAsync(success);

            var controller = new CommentsServiceController(dataService.Object);

            // Act
            var response = await controller.IncrementLikes(0);

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }
    }
}
