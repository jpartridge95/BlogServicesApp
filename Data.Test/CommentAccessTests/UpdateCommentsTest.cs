using Data.Context;
using Data.DataAccess;
using Microsoft.EntityFrameworkCore;
using Models;
using Xunit;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Data.Test.CommentAccessTests
{
    public class UpdateCommentsTest : IDisposable
    {
        CommentAccess Access;
        DbContextOptions<SocialContext> Options;
        SocialContext Context;

        public UpdateCommentsTest()
        {
            Options = new DbContextOptionsBuilder<SocialContext>()
                .UseInMemoryDatabase(databaseName: "UpdateCommentDB")
                .Options;

            Context = new SocialContext(Options);

            Context.Comments.Add(new Comment()
            {
                CreatedBy = "123-123-abc",
                ForPost = 1,
                Content = "Snazzy snazzy content",
                CreatedAt = DateTime.Now,
                Likes = 1,
                Dislikes = 1
            });
            Context.SaveChanges();

            Access = new CommentAccess(Context);
        }
        
        [Fact]
        public async void IncrementLikes_IncrementsLikes()
        {
            // Arrange
            int commentId = 1;

            // Act
            Access.IncrementLikes(commentId);

            // Assert
            Assert.Equal(2, Context.Comments.Find(commentId).Likes);
        }

        [Fact]
        public async void IncrementLikes_ReturnsTrue_OnSuccess()
        {
            // Arrange
            int commentId = 1;
            bool success;

            // Act
            success = await Access.IncrementLikes(commentId);

            // Assert
            Assert.True(success);
        }
        
        [Fact]
        public async void IncrementLikes_ReturnsFalse_OnError()
        {
            // Arrange
            int commentId = 2;
            bool success;

            // Act
            success = await Access.IncrementLikes(commentId);

            // Assert
            Assert.False(success);
        }

        [Fact]
        public async void DecrementLikes_DecrementsLikes()
        {
            // Arrange
            int commentId = 1;

            // Act
            Access.DecrementLikes(commentId);

            // Assert
            Assert.Equal(0, Context.Comments.Find(commentId).Likes);
        }

        [Fact]
        public async void DecrementLikes_ReturnsTrue_OnSuccess()
        {
            // Arrange
            int commentId = 1;
            bool success;

            // Act
            success = await Access.DecrementLikes(commentId);

            // Assert
            Assert.True(success);
        }

        [Fact]
        public async void DecrementLikes_ReturnsFalse_OnError()
        {
            // Arrange
            int commentId = 2;
            bool success;

            // Act
            success = await Access.DecrementLikes(commentId);

            // Assert
            Assert.False(success);
        }

        [Fact]
        public async void IncrementDislikes_IncrementsDislikes()
        {
            // Arrange
            int commentId = 1;

            // Act
            Access.IncrementDislikes(commentId);

            // Assert
            Assert.Equal(2, Context.Comments.Find(commentId).Dislikes);
        }

        [Fact]
        public async void IncrementDislikes_ReturnsTrue_OnSuccess()
        {
            // Arrange
            int commentId = 1;
            bool success;

            // Act
            success = await Access.IncrementDislikes(commentId);

            // Assert
            Assert.True(success);
        }

        [Fact]
        public async void IncrementDislikes_ReturnsFalse_OnError()
        {
            // Arrange
            int commentId = 2;
            bool success;

            // Act
            success = await Access.IncrementDislikes(commentId);

            // Assert
            Assert.False(success);
        }

        [Fact]
        public async void DecrementDislikes_DecrementsDislikes()
        {
            // Arrange
            int commentId = 1;

            // Act
            Access.DecrementDislikes(commentId);

            // Assert
            Assert.Equal(0, Context.Comments.Find(commentId).Dislikes);
        }

        [Fact]
        public async void DecrementDislikes_ReturnsTrue_OnSuccess()
        {
            // Arrange
            int commentId = 1;
            bool success;

            // Act
            success = await Access.DecrementDislikes(commentId);

            // Assert
            Assert.True(success);
        }

        [Fact]
        public async void DecrementDislikes_ReturnsFalse_OnError()
        {
            // Arrange
            int commentId = 2;
            bool success;

            // Act
            success = await Access.DecrementDislikes(commentId);

            // Assert
            Assert.False(success);
        }

        [Fact]
        public async void EditComment_EditsComment()
        {
            // Arrange
            int commentId = 1;
            Comment comment = new Comment()
            {
                CreatedBy = "123-123-abc",
                ForPost = 1,
                Content = "Edited snazzy content",
                CreatedAt = DateTime.Now,
                Likes = 1,
                Dislikes = 1
            };

            // Act
            Access.EditCommentContent(1, comment);

            // Assert
            Assert.Equal("Edited snazzy content", Context.Comments.Find(commentId).Content);
        }

        [Fact]
        public async void EditComment_ReturnsTrue_OnSuccess()
        {
            // Arrange
            bool success;
            int commentId = 1;
            Comment comment = new Comment()
            {
                CreatedBy = "123-123-abc",
                ForPost = 1,
                Content = "Edited snazzy content",
                CreatedAt = DateTime.Now,
                Likes = 1,
                Dislikes = 1
            };

            // Act
            success = await Access.EditCommentContent(commentId ,comment);

            // Assert
            Assert.True(success);
        }

        [Fact]
        public async void EditComment_ReturnsFalse_OnNullData()
        {
            // Arrange
            bool success;
            int commentId = 1;
            Comment comment = new Comment()
            {
                CreatedBy = "123-123-abc",
                ForPost = 1,
                Content = null,
                CreatedAt = DateTime.Now,
                Likes = 1,
                Dislikes = 1
            };

            // Act
            success = await Access.EditCommentContent(commentId, comment);

            // Assert
            Assert.False(success);
        }

        [Fact]
        public async void EditComment_ReturnsFalse_OnError()
        {
            // Arrange
            bool success;
            int commentId = 2;
            Comment comment = new Comment()
            {
                CreatedBy = "123-123-abc",
                ForPost = 1,
                Content = "Edited snazzy content",
                CreatedAt = DateTime.Now,
                Likes = 1,
                Dislikes = 1
            };

            // Act
            success = await Access.EditCommentContent(commentId, comment);

            // Assert
            Assert.False(success);
        }

        public void Dispose()
        {


            Context.Database.EnsureDeleted();

            Context.Dispose();
        }
    }
}

