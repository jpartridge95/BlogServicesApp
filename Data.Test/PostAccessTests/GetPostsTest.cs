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
    public class GetPostsTest : IDisposable
    {
        private PostAccess Access;
        private DbContextOptions<SocialContext> DbOptions;
        private SocialContext context;

        public GetPostsTest()
        {
            DbOptions = new DbContextOptionsBuilder<SocialContext>()
                .UseInMemoryDatabase(databaseName: "GetPostDB")
                .Options;

            context = new SocialContext(DbOptions);
            
            context.Database.EnsureCreated();

            context.Posts.Add(new Post()
            {
                Content = "This is a post that is submitted to the db",
                CreatedBy = 1,
                Dislikes = 0,
                Likes = 1,
                Title = "Cool Post Title [F]",
                Created = DateTime.Now,
            });
            context.SaveChanges();

            Access = new PostAccess(context);
            
        }

        [Fact]
        public async void GetAllPosts_ReturnsOnePost_WhereDBContainsOne()
        {
            // Arrange + Act
            List<Post> posts = await Access.GetAllPosts();

            // Assert
            Assert.Single(posts);
        }

        [Fact]
        public async void GetAllPosts_ReturnsTwoPosts_WhereDbContainsTwo()
        {
            // Arrange
            
            context.Posts.Add(new Post()
            {
                Content = "What a snazzy post am I right?",
                CreatedBy = 1,
                Dislikes = 0,
                Likes = 1,
                Title = "Snazzy Post",
                Created = DateTime.Now,
            });
            context.SaveChanges();

            // Act
            List<Post> posts = await Access.GetAllPosts();

            // Assert
            Assert.Equal(2, posts.Count);
        }

        [Fact]
        public async void GetAllPosts_ReturnsEmpty_WhenDbIsEmpty()
        {
            // Arrange
            
            context.Posts.Remove(context.Posts.First());
            context.SaveChanges();


            // Act
            List<Post> posts = await Access.GetAllPosts();

            // Assert
            Assert.Empty(posts);
        }

        [Fact]
        public async void GetPostsFromUser_ReturnsSingle_WhereOneExists()
        {
            // Arrange + Act
            List<Post> onePost = await Access.GetPostsFromUser(1);

            // Assert
            Assert.Single(onePost);
        }

        [Fact]
        public async void GetPostsFromUser_ReturnsEmptyList_WhereNoPostsExist()
        {
            // Arrange
            
            context.Posts.Remove(context.Posts.First());
            context.SaveChanges();

            // Act
            List<Post> noPosts = await Access.GetPostsFromUser(1);

            // Assert
            Assert.Empty(noPosts);
        }

        [Fact]
        public async void GetPostsFromUser_ReturnsTwoResults_WhereTwoResults()
        {
            // Arrange
            
            context.Posts.AddRange(
                new Post()
                {
                    Content = "What a snazzy post am I right?",
                    CreatedBy = 1,
                    Dislikes = 0,
                    Likes = 1,
                    Title = "Snazzy Post",
                    Created = DateTime.Now,
                },
                new Post()
                {
                    Content = "I swear my post is snazzier guys, check it",
                    CreatedBy = 2,
                    Dislikes = 0,
                    Likes = 1,
                    Title = "Snazzier Post",
                    Created = DateTime.Now,
                }
            );
            context.SaveChanges();

            // Act
            List<Post> twoPosts = await Access.GetPostsFromUser(1);

            // Assert
            Assert.Equal(2, twoPosts.Count);
        }

        [Fact]
        public async void GetSinglePost_ReturnsPost_WherePostExists()
        {
            // Arrange
            Post post = new Post();

            // Act
            post = await Access.GetSinglePost(1);

            // Assert
            Assert.Equal("Cool Post Title [F]", post.Title);
        }

        [Fact]
        public async Task GetSinglePost_ThrowsException_WhereNoPost()
        {
            await Assert.ThrowsAsync<IdNotFoundException>(() => Access.GetSinglePost(2));
        }

        public void Dispose()
        {
            

            context.Database.EnsureDeleted();

            

        }
    }
}