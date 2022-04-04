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
    public class DeletePostsTest : IDisposable
    {
        PostAccess Access;
        DbContextOptions<SocialContext> Options;
        SocialContext Context;

        public DeletePostsTest()
        {
            Options = new DbContextOptionsBuilder<SocialContext>()
                .UseInMemoryDatabase(databaseName: "DeletePostDB")
                .Options;

            Context = new SocialContext(Options);

            Context.Posts.Add(new Post()
            {
                Content = "This is a post that is submitted to the db",
                CreatedBy = "123-123-abc",
                Dislikes = 0,
                Likes = 1,
                Title = "Cool Post Title [F]"
            });
            Context.SaveChanges();

            Access = new PostAccess(Context);
        }

        [Fact]
        public async void DeletePost_DeletesPost()
        {
            // Arrange + Act
            await Access.DeletePost(1);
            List<Post> posts = Context.Posts.ToList();

            // Assert
            Assert.Empty(posts);
        }

        [Fact]
        public async void DeletePost_ReturnsTrue_OnSuccess()
        {
            //Arrange
            bool success;

            //Act
            success = await Access.DeletePost(1);

            //Assert
            Assert.True(success);
        }

        [Fact]
        public async void DeletePost_ReturnsFalse_OnFailure()
        {
            //Arrange
            bool success;

            //Act
            success = await Access.DeletePost(5);

            //Assert
            Assert.False(success);
        }

        [Fact]
        public async void DeletePostByUser_DeletesAllPostsByUser()
        {
            // Arrange
            Post post = new Post()
            {
                Content = "A simple test post",
                CreatedBy = "123-123-abc",
                Dislikes = 0,
                Likes = 1,
                Title = "New post title"
            };
            Context.Posts.Add(post);
            Context.SaveChanges();

            // Act
            await Access.DeleteByUser("123-123-abc");

            // Assert
            Assert.Empty(Context.Posts.ToList());
        }

        [Fact]
        public async void DeletePostByUser_ReturnsTrue_OnSuccess()
        {
            // Arrange
            bool success;

            // Act
            success = await Access.DeleteByUser("123-123-abc");

            // Assert
            Assert.True(success);
        }

        [Fact]
        public async void DeletePostByUser_ReturnsFalse_OnFailure()
        {
            // Arrange
            bool success;

            // Act
            success = await Access.DeleteByUser("123-123-abc5");

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

