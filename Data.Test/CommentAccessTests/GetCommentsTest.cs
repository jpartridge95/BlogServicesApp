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
    public class GetCommentsTest : IDisposable
    {
        CommentAccess Access;
        DbContextOptions<SocialContext> Options;
        SocialContext Context;

        public GetCommentsTest()
        {
            Options = new DbContextOptionsBuilder<SocialContext>()
                .UseInMemoryDatabase(databaseName: "GetCommentDB")
                .Options;

            Context = new SocialContext(Options);

            Context.Comments.Add(new Comment()
            {
                CreatedBy = 1,
                ForPost = 1,
                Content = "Snazzy content",
                CreatedAt = DateTime.Now,
                Likes = 1,
                Dislikes = 1
            });

            Context.Comments.Add(new Comment()
            {
                CreatedBy = 2,
                ForPost = 1,
                Content = "Snazzy wazzy content",
                CreatedAt = DateTime.Now,
                Likes = 1,
                Dislikes = 1
            });

            Context.Comments.Add(new Comment()
            {
                CreatedBy = 1,
                ForPost = 2,
                Content = "Snazzy snazzy content",
                CreatedAt = DateTime.Now,
                Likes = 1,
                Dislikes = 1
            });
            
            Context.Comments.Add(new Comment()
            {
                CreatedBy = 2,
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
        public async void GetByPost_ReturnsCorrectly()
        {
            // Arrange
            List<Comment> comments;
            int postId = 1;

            // Act
            comments = Access.GetByPost(postId);

            // Assert
            Assert.Equal(2, comments.Count);
        }

        [Fact]
        public async void GetByPost_ReturnsNull_WhenNoCommentsFound()
        {
            // Arrange
            List<Comment> comments;
            int postId = 3;

            // Act
            comments = Access.GetByPost(postId);

            // Assert
            Assert.Null(comments);
        }

        [Fact]
        public async void GetByUser_ReturnsCorrectly()
        {
            // Arrange
            List<Comment> comments;
            int postId = 1;

            // Act
            comments = Access.GetByUser(postId);

            // Assert
            Assert.Equal(2, comments.Count);
        }

        [Fact]
        public async void GetByUser_ReturnsNull_WhenNoDataFound()
        {
            List<Comment> comments;
            int postId = 3;

            // Act
            comments = Access.GetByUser(postId);

            // Assert
            Assert.Null(comments);
        }

        public void Dispose()
        {


            Context.Database.EnsureDeleted();

            Context.Dispose();
        }
    }
}

