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
    public class UpdatePostsTest : IDisposable
    {
        PostAccess Access;
        DbContextOptions<SocialContext> Options;
        SocialContext Context;

        public UpdatePostsTest()
        {
            Options = new DbContextOptionsBuilder<SocialContext>()
                .UseInMemoryDatabase(databaseName: "UpdatePostDB")
                .Options;

            Context = new SocialContext(Options);

            Context.Posts.Add(new Post()
            {
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
        public async void IncrementLikes_IncrementsLikeOnPost()
        {
            // Arrange
            int postId = 1;

            // Act
            Access.IncrementLikes(postId);

            // Assert
            Assert.Equal(2, Context.Posts.Find(1).Likes);
        }

        [Fact]
        public async void IncrementLikes_ReturnsTrue_OnSuccess()
        {
            // Arrange
            bool success;

            // Act
            success = await Access.IncrementLikes(1);

            // Assert
            Assert.True(success);
        }

        [Fact]
        public async void IncrementLikes_ReturnsFalse_OnError()
        {
            // Arrange
            bool success;

            // Act
            success = await Access.IncrementLikes(2);

            // Assert
            Assert.False(success);
        }

        [Fact]
        public async void DecrementLikes_DecrementsLikeCount()
        {
            // Arrange
            int postId = 1;

            // Act
            Access.DecrementLikes(postId);

            // Assert
            Assert.Equal(0, Context.Posts.Find(1).Likes);
        }

        [Fact]
        public async void DecrementLikes_ReturnsTrue_OnSuccess()
        {
            // Arrange
            bool success;

            // Act
            success = await Access.DecrementLikes(1);

            // Assert
            Assert.True(success);
        }

        [Fact]
        public async void DecrementLikes_ReturnsFalse_OnError()
        {
            // Arrange
            bool success;

            // Act
            success = await Access.DecrementLikes(2);

            // Assert
            Assert.False(success);
        }

        [Fact]
        public async void IncrementDislikes_IncrementsDislikes()
        {
            // Arrange
            int postId = 1;

            // Act
            Access.IncrementDislikes(postId);

            // Assert
            Assert.Equal(1, Context.Posts.Find(1).Dislikes);
        }

        [Fact]
        public async void IncrementDislikes_ReturnsTrue_OnSuccess()
        {
            // Arrange
            bool success;

            // Act
            success = await Access.IncrementDislikes(1);

            // Assert
            Assert.True(success);
        }

        [Fact]
        public async void IncrementDislikes_ReturnsFalse_OnError()
        {
            // Arrange
            bool success;

            // Act
            success = await Access.IncrementDislikes(2);

            // Assert
            Assert.False(success);
        }

        [Fact]
        public async void DecrementDislikes_DecrementsDislikes()
        {
            // Arrange
            int postId = 1;

            // Act
            Access.DecrementDislikes(postId);

            // Assert
            Assert.Equal(-1, Context.Posts.Find(1).Dislikes);
        }

        [Fact]
        public async void DecrementDislikes_ReturnsTrue_OnSuccess()
        {
            // Arrange
            bool success;

            // Act
            success = await Access.DecrementDislikes(1);

            // Assert
            Assert.True(success);
        }

        [Fact]
        public async void DecrementDislikes_ReturnsFalse_OnError()
        {
            // Arrange
            bool success;

            // Act
            success = await Access.DecrementDislikes(2);

            // Assert
            Assert.False(success);
        }

        [Fact]
        public async void UpdatePost_CorrectlyChangesPost()
        {
            // Arrange
            Post post = new Post()
            {
                Content = "New content, who dis?",
                CreatedBy = 1,
                Dislikes = 0,
                Likes = 1,
                Title = "Cool Post Title [F]"
            };

            // Act
            Access.EditPost(1, post);

            // Assert
            Assert.Equal("New content, who dis?", Context.Posts.Find(1).Content);
        }

        [Fact]
        public async void UpdatePost_ReturnsTrue_OnSuccess()
        {
            // Arrange
            Post post = new Post()
            {
                Content = "New content, who dis?",
                CreatedBy = 1,
                Dislikes = 0,
                Likes = 1,
                Title = "Cool Post Title [F]"
            };
            bool success;

            // Act
            success = await Access.EditPost(1, post);

            // Assert
            Assert.True(success);
        }

        [Fact]
        public async void UpdatePost_ReturnsFalse_OnPostNotFound()
        {
            // Arrange
            Post post = new Post()
            {
                Content = "New content, who dis?",
                CreatedBy = 1,
                Dislikes = 0,
                Likes = 1,
                Title = "Cool Post Title [F]"
            };
            bool success;

            // Act
            success = await Access.EditPost(2, post);

            // Assert
            Assert.False(success);
        }

        [Fact]
        public async void UpdatePost_ReturnsFalse_OnIncorrectData()
        {
            // Arrange
            Post post = new Post()
            {
                Content = null,
                CreatedBy = 1,
                Dislikes = 0,
                Likes = 1,
                Title = null
            };
            bool success;

            // Act
            success = await Access.EditPost(1, post);

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

