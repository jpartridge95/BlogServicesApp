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
    public class DeleteCommentsTest : IDisposable
    {
        CommentAccess Access;
        DbContextOptions<SocialContext> Options;
        SocialContext Context;

        public DeleteCommentsTest()
        {
            Options = new DbContextOptionsBuilder<SocialContext>()
                .UseInMemoryDatabase(databaseName: "DeleteCommentDB")
                .Options;

            Context = new SocialContext(Options);

            Context.Comments.Add(new Comment()
            {
                CreatedBy = "123-123-abc",
                ForPost = 1,
                Content = "Snazzy content",
                CreatedAt = DateTime.Now,
                Likes = 1,
                Dislikes = 1
            });

            Context.Comments.Add(new Comment()
            {
                CreatedBy = "123-123-abc2",
                ForPost = 1,
                Content = "Snazzy wazzy content",
                CreatedAt = DateTime.Now,
                Likes = 1,
                Dislikes = 1
            });

            Context.Comments.Add(new Comment()
            {
                CreatedBy = "123-123-abc",
                ForPost = 2,
                Content = "Snazzy snazzy content",
                CreatedAt = DateTime.Now,
                Likes = 1,
                Dislikes = 1
            });

            Context.Comments.Add(new Comment()
            {
                CreatedBy = "123-123-abc2",
                ForPost = 2,
                Content = "Snazzy snazzy snazzy content",
                CreatedAt = DateTime.Now,
                Likes = 1,
                Dislikes = 1
            });
            Context.SaveChanges();

            Access = new CommentAccess(Context);
        }
        
        [Fact]
        public async void DeleteComment_DeletesCorrectComment()
        {
            // Arrange
            int commentId = 1;

            // Act
            Access.DeleteComment(commentId);

            // Assert
            Assert.Null(Context.Comments.Find(commentId));
        }

        [Fact]
        public async void DeleteComment_ReturnsTrue_OnDelete()
        {
            // Arrange
            int commentId = 1;
            bool success;

            // Act
            success = await Access.DeleteComment(commentId);

            // Assert
            Assert.True(success);
        }

        [Fact]
        public async void DeleteComment_ReturnsFalse_OnNoDelete()
        {
            // Arrange
            int commentId = 10;
            bool success;

            // Act
            success = await Access.DeleteComment(commentId);

            // Assert
            Assert.False(success);
        }

        [Fact]
        public async void DeleteByUser_DeletesCorrectly()
        {
            // Arrange
            string userId = "123-123-abc";

            // Act
            Access.DeleteByUser(userId);

            // Assert
            Assert.Equal(2, Context.Comments.ToList().Count);
        }

        [Fact]
        public async void DeleteByUser_ReturnsTrue_OnDelete()
        {
            // Arrange
            string userId = "123-123-abc";
            bool success;

            // Act
            success = await Access.DeleteByUser(userId);

            // Assert
            Assert.True(success);
        }

        [Fact]
        public async void DeleteByUser_ReturnsFalse_OnNoDelete()
        {
            // Arrange
            string userId = "haha";
            bool success;

            // Act
            success = await Access.DeleteByUser(userId);

            // Assert
            Assert.False(success);
        }

        [Fact]
        public async void DeleteByPost_DeletesCorrectly()
        {
            // Arrange
            int postId = 1;

            // Act
            Access.DeleteByPost(postId);

            // Assert
            Assert.Equal(2, Context.Comments.ToList().Count);
        }

        [Fact]
        public async void DeleteByPost_ReturnsTrue_OnDelete()
        {
            // Arrange
            int postId = 1;
            bool success;

            // Act
            success = await Access.DeleteByPost(postId);

            // Assert
            Assert.True(success);
        }

        [Fact]
        public async void DeleteByPost_ReturnsFalse_OnNoDelete()
        {
            // Arrange
            int postId = 10;
            bool success;

            // Act
            success = await Access.DeleteByPost(postId);

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

