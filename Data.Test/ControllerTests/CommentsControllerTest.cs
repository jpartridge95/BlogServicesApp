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
    public class CommentsControllerTest
    {
        [Fact]
        public async void GetCommentsByPost_Returns404_WhenNotFound()
        {
            // Arrange
            List<Comment> comments = null;
            Mock<CommentAccess> access = new Mock<CommentAccess>(null);
            access.Setup(a => a.GetByPost(It.IsAny<int>()))
                .Returns(comments);

            CommentsController controller = new CommentsController(null, access.Object);

            // Act
            var result = await controller.GetCommentsByPost(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void GetCommentsByPost_ReturnsValues_Correctly()
        {
            // Arrange
            List<Comment> comments = new List<Comment>()
            {
                new Comment()
                {
                    Id = 1,
                    Content = "TestPost",
                    Likes = 1,
                    Dislikes = 1,
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1
                },
                new Comment()
                {
                    Id = 2,
                    Content = "TestPost",
                    Likes = 1,
                    Dislikes = 1,
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1
                }
            };
            Mock<CommentAccess> access = new Mock<CommentAccess>(null);
            access.Setup(a => a.GetByPost(It.IsAny<int>()))
                .Returns(comments);

            CommentsController controller = new CommentsController(null, access.Object);

            // Act
            var results = await controller.GetCommentsByPost(1);

            // Assert
            Assert.Equal(2, results.Value.Count);
        }

        [Fact]
        public async void GetCommentsByUser_Returns404_OnNotFound()
        {
            // Arrange
            List<Comment> comments = null;
            Mock<CommentAccess> access = new Mock<CommentAccess>(null);
            access.Setup(a => a.GetByUser(It.IsAny<int>()))
                .Returns(comments);

            CommentsController controller = new CommentsController(null, access.Object);

            // Act
            var results = await controller.GetCommentsByUser(1);

            // Assert
            Assert.IsType<NotFoundResult>(results.Result);
        }

        [Fact]
        public async void GetCommentsByUser_ReturnsContent_OnSuccess()
        {
            // Arrange
            List<Comment> comments = new List<Comment>()
            {
                new Comment()
                {
                    Id = 1,
                    Content = "TestPost",
                    Likes = 1,
                    Dislikes = 1,
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1
                },
                new Comment()
                {
                    Id = 2,
                    Content = "TestPost",
                    Likes = 1,
                    Dislikes = 1,
                    CreatedAt = DateTime.Now,
                    CreatedBy = 1
                }
            };
            Mock<CommentAccess> access = new Mock<CommentAccess>(null);
            access.Setup(a => a.GetByUser(It.IsAny<int>()))
                .Returns(comments);

            CommentsController controller = new CommentsController(null, access.Object);

            // Act
            var results = await controller.GetCommentsByUser(1);

            // Assert
            Assert.Equal(2, results.Value.Count);
        }

        [Fact]
        public async void DeleteSingle_Returns204NoContent_OnSuccessfulDelete()
        {
            // Arrange
            var success = true;
            Mock<CommentAccess> access = new Mock<CommentAccess>(null);
            access.Setup(a => a.DeleteComment(It.IsAny<int>()))
                .ReturnsAsync(success);

            CommentsController controller = new CommentsController(null, access.Object);

            // Act
            var results = await controller.DeleteSingleComment(1);

            // Assert
            Assert.IsType<NoContentResult>(results.Result);
        }

        [Fact]
        public async void DeleteSingle_Returns404_OnIdNotFound()
        {
            // Arrange
            var success = false;
            Mock<CommentAccess> access = new Mock<CommentAccess>(null);
            access.Setup(a => a.DeleteComment(It.IsAny<int>()))
                .ReturnsAsync(success);

            CommentsController controller = new CommentsController(null, access.Object);

            // Act
            var results = await controller.DeleteSingleComment(1);

            // Assert
            Assert.IsType<NotFoundResult>(results.Result);
        }

        [Fact]
        public async void DeleteByUser_Returns204_OnSuccess()
        {
            // Arrange
            var success = true;
            Mock<CommentAccess> access = new Mock<CommentAccess>(null);
            access.Setup(a => a.DeleteByUser(It.IsAny<int>()))
                .ReturnsAsync(success);

            CommentsController controller = new CommentsController(null, access.Object);

            // Act
            var results = await controller.DeleteByUser(1);

            // Assert
            Assert.IsType<NoContentResult>(results.Result);
        }

        [Fact]
        public async void DeleteByUser_Returns404_OnNotFound()
        {
            // Arrange
            var success = false;
            Mock<CommentAccess> access = new Mock<CommentAccess>(null);
            access.Setup(a => a.DeleteByUser(It.IsAny<int>()))
                .ReturnsAsync(success);

            CommentsController controller = new CommentsController(null, access.Object);

            // Act
            var results = await controller.DeleteByUser(1);

            // Assert
            Assert.IsType<NotFoundResult>(results.Result);
        }

        [Fact]
        public async void DeleteByPost_Returns204_OnSuccess()
        {
            // Arrange
            var success = true;
            Mock<CommentAccess> access = new Mock<CommentAccess>(null);
            access.Setup(a => a.DeleteByPost(It.IsAny<int>()))
                .ReturnsAsync(success);

            CommentsController controller = new CommentsController(null, access.Object);

            // Act
            var results = await controller.DeleteByPost(1);

            // Assert
            Assert.IsType<NoContentResult>(results.Result);
        }

        [Fact]
        public async void DeleteByPost_Returns404_OnNotFound()
        {
            // Arrange
            var success = false;
            Mock<CommentAccess> access = new Mock<CommentAccess>(null);
            access.Setup(a => a.DeleteByPost(It.IsAny<int>()))
                .ReturnsAsync(success);

            CommentsController controller = new CommentsController(null, access.Object);

            // Act
            var results = await controller.DeleteByPost(1);

            // Assert
            Assert.IsType<NotFoundResult>(results.Result);
        }

        [Fact]
        public async void IncrementLikes_Returns204_OnSuccess()
        {
            // Arrange
            var success = true;
            Mock<CommentAccess> access = new Mock<CommentAccess>(null);
            access.Setup(a => a.IncrementLikes(It.IsAny<int>()))
                .ReturnsAsync(success);

            CommentsController controller = new CommentsController(null, access.Object);

            // Act
            var results = await controller.IncrementLikes(1);

            // Assert
            Assert.IsType<NoContentResult>(results.Result);
        }

        [Fact]
        public async void IncrementLikes_Returns404_OnNotFound()
        {
            // Arrange
            var success = false;
            Mock<CommentAccess> access = new Mock<CommentAccess>(null);
            access.Setup(a => a.IncrementLikes(It.IsAny<int>()))
                .ReturnsAsync(success);

            CommentsController controller = new CommentsController(null, access.Object);

            // Act
            var results = await controller.IncrementLikes(1);

            // Assert
            Assert.IsType<NotFoundResult>(results.Result);
        }

        [Fact]
        public async void DecrementLikes_Returns204_OnSuccess()
        {
            // Arrange
            var success = true;
            Mock<CommentAccess> access = new Mock<CommentAccess>(null);
            access.Setup(a => a.DecrementLikes(It.IsAny<int>()))
                .ReturnsAsync(success);

            CommentsController controller = new CommentsController(null, access.Object);

            // Act
            var results = await controller.DecrementLikes(1);

            // Assert
            Assert.IsType<NoContentResult>(results.Result);
        }

        [Fact]
        public async void DecrementLikes_Returns404_OnNotFound()
        {
            // Arrange
            var success = false;
            Mock<CommentAccess> access = new Mock<CommentAccess>(null);
            access.Setup(a => a.DecrementLikes(It.IsAny<int>()))
                .ReturnsAsync(success);

            CommentsController controller = new CommentsController(null, access.Object);

            // Act
            var results = await controller.DecrementLikes(1);

            // Assert
            Assert.IsType<NotFoundResult>(results.Result);
        }

        [Fact]
        public async void IncrementDislikes_Returns204_OnSuccess()
        {
            // Arrange
            var success = true;
            Mock<CommentAccess> access = new Mock<CommentAccess>(null);
            access.Setup(a => a.IncrementDislikes(It.IsAny<int>()))
                .ReturnsAsync(success);

            CommentsController controller = new CommentsController(null, access.Object);

            // Act
            var results = await controller.IncrementDislikes(1);

            // Assert
            Assert.IsType<NoContentResult>(results.Result);
        }

        [Fact]
        public async void IncrementDislikes_Returns404_OnNotFound()
        {
            // Arrange
            var success = false;
            Mock<CommentAccess> access = new Mock<CommentAccess>(null);
            access.Setup(a => a.IncrementDislikes(It.IsAny<int>()))
                .ReturnsAsync(success);

            CommentsController controller = new CommentsController(null, access.Object);

            // Act
            var results = await controller.IncrementDislikes(1);

            // Assert
            Assert.IsType<NotFoundResult>(results.Result);
        }

        [Fact]
        public async void DecrementDislikes_Returns204_OnSuccess()
        {
            // Arrange
            var success = true;
            Mock<CommentAccess> access = new Mock<CommentAccess>(null);
            access.Setup(a => a.DecrementDislikes(It.IsAny<int>()))
                .ReturnsAsync(success);

            CommentsController controller = new CommentsController(null, access.Object);

            // Act
            var results = await controller.DecrementDislikes(1);

            // Assert
            Assert.IsType<NoContentResult>(results.Result);
        }

        [Fact]
        public async void DecrementDislikes_Returns404_OnNotFound()
        {
            // Arrange
            var success = false;
            Mock<CommentAccess> access = new Mock<CommentAccess>(null);
            access.Setup(a => a.DecrementDislikes(It.IsAny<int>()))
                .ReturnsAsync(success);

            CommentsController controller = new CommentsController(null, access.Object);

            // Act
            var results = await controller.DecrementDislikes(1);

            // Assert
            Assert.IsType<NotFoundResult>(results.Result);
        }

        [Fact]
        public async void EditComment_Returns204_OnSuccess()
        {
            // Arrange
            var success = true;
            Mock<CommentAccess> access = new Mock<CommentAccess>(null);
            access.Setup(a => a.EditCommentContent(It.IsAny<int>(), It.IsAny<Comment>()))
                .ReturnsAsync(success);

            CommentsController controller = new CommentsController(null, access.Object);

            // Act
            var results = await controller.EditComment(1, new Comment());

            // Assert
            Assert.IsType<NoContentResult>(results.Result);
        }

        [Fact]
        public async void EditComment_Returns404_OnNotFound()
        {
            // Arrange
            var success = false;
            Mock<CommentAccess> access = new Mock<CommentAccess>(null);
            access.Setup(a => a.EditCommentContent(It.IsAny<int>(), It.IsAny<Comment>()))
                .ReturnsAsync(success);

            CommentsController controller = new CommentsController(null, access.Object);

            // Act
            var results = await controller.EditComment(1, new Comment());

            // Assert
            Assert.IsType<NotFoundResult>(results.Result);
        }

        [Fact]
        public async void CreateComment_Returns200_OnSuccess()
        {
            // Arrange
            var success = true;
            Mock<CommentAccess> access = new Mock<CommentAccess>(null);
            access.Setup(a => a.CreateComment(It.IsAny<Comment>()))
                .ReturnsAsync(success);

            CommentsController controller = new CommentsController(null, access.Object);

            // Act
            var results = await controller.CreateComment(new Comment());

            // Assert
            Assert.IsType<OkResult>(results.Result);
        }

        [Fact]
        public async void CreateComment_Returns400_OnError()
        {
            // Arrange
            var success = false;
            Mock<CommentAccess> access = new Mock<CommentAccess>(null);
            access.Setup(a => a.CreateComment(It.IsAny<Comment>()))
                .ReturnsAsync(success);

            CommentsController controller = new CommentsController(null, access.Object);

            // Act
            var results = await controller.CreateComment(new Comment());

            // Assert
            Assert.IsType<BadRequestResult>(results.Result);
        }
    }
}
