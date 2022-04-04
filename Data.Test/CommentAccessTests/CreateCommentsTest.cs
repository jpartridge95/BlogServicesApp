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
    public class CreateCommentsTest : IDisposable
    {
        CommentAccess Access;
        DbContextOptions<SocialContext> Options;
        SocialContext Context;

        public CreateCommentsTest()
        {
            Options = new DbContextOptionsBuilder<SocialContext>()
                .UseInMemoryDatabase(databaseName: "CreateCommentDB")
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
        public async void CreateComment_CreatesANewComment()
        {
            // Arrange
            Comment comment = new Comment()
            {
                CreatedBy = "123-123-abc",
                ForPost = 1,
                Content = "More snazzy content",
                CreatedAt = DateTime.Now,
                Likes = 1,
                Dislikes = 1
            };

            // Act
            Access.CreateComment(comment);

            // Assert
            Assert.Equal(2, Context.Comments.Count());
        }

        [Fact]
        public async void CreateComment_ReturnsTrue_OnSuccess()
        {
            // Arrange
            bool success;
            Comment comment = new Comment()
            {
                CreatedBy = "123-123-abc",
                ForPost = 1,
                Content = "More snazzy content",
                CreatedAt = DateTime.Now,
                Likes = 1,
                Dislikes = 1
            };

            // Act
            success = await Access.CreateComment(comment);

            // Assert
            Assert.True(success);
        }

        [Fact]
        public async void CreateComment_ReturnsFalse_OnError()
        {
            // Arrange
            bool success;
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
            success = await Access.CreateComment(comment); 

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

