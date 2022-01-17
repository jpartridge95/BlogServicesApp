using Data.Context;
using Data.DataAccess;
using Microsoft.EntityFrameworkCore;
using Models;
using Xunit;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Data.Test.PostAccessTests
{
    public class CreatePostsTest : IDisposable
    {
        PostAccess Access;
        DbContextOptions<SocialContext> Options;
        SocialContext Context;

        public CreatePostsTest()
        {
            Options = new DbContextOptionsBuilder<SocialContext>()
                .UseInMemoryDatabase(databaseName: "CreatePostDB")
                .Options;

            Context = new SocialContext(Options);

            Context.Posts.Add(new Post() {
                Content = "This is a post that is submitted to the db",
                CreatedBy = 1,
                Dislikes = 0,
                Likes = 1,
                Title = "Cool Post Title [F]"
            });
            Context.SaveChanges();

            Access = new PostAccess(Context);
        }

        [Fact]
        public async void CreatePost_CreatesPost()
        {
            // Arrange
            Post post = new Post()
            {
                Content = "Just some sample text",
                CreatedBy = 2,
                Dislikes = 0,
                Likes = 1,
                Title = "Test Title"
            };

            // Act 
            await Access.CreatePost(post);
            Post returned = await Context.Posts.FindAsync(2);

            // Assert
            Assert.Equal("Test Title", returned.Title);
        }

        [Fact]
        public async void CreatePost_ReturnsTrue_OnSuccess()
        {
            // Arrange
            Post post = new Post()
            {
                Content = "Just some sample text",
                CreatedBy = 2,
                Dislikes = 0,
                Likes = 1,
                Title = "Test Title"
            };

            // Act
            bool success = await Access.CreatePost(post);

            // Assert
            Assert.True(success);
        }

        [Fact]
        public async void CreatePost_ReturnsFalse_OnNullValue()
        {
            // Arrange
            Post post = new Post()
            {
                Content = null,
                CreatedBy = 2,
                Dislikes = 0,
                Likes = 1,
                Title = "Test Title"
            };

            // Act
            bool success = await Access.CreatePost(post);

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

