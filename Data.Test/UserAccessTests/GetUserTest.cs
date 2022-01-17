using Data.Context;
using Data.DataAccess;
using Microsoft.EntityFrameworkCore;
using Models;
using Xunit;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Data.Test.UserAccessTests
{
    public class GetUserTest : IDisposable
    {
        UserAccess Access;
        DbContextOptions<SocialContext> Options;
        SocialContext context;

        public GetUserTest()
        {
            Options = new DbContextOptionsBuilder<SocialContext>()
                .UseInMemoryDatabase(databaseName: "GetUserDB")
                .Options;

            context = new SocialContext(Options);
            context.Database.EnsureCreated();

            context.Users.Add(new User() { Firstname = "Jack", Lastname = "Smith", Email = "jacksmith@example.com", Password = "securepw", Username = "jsmith1" });
            context.SaveChanges();

            Access = new UserAccess(context);
        }

        [Fact]
        public async void GetById_ReturnsUser()
        {
            // Arrange + act
            User jack = await Access.GetByID(1);
            
            // Assert
            Assert.Equal("Jack", jack.Firstname);
            Assert.Equal(1, jack.Id);
        }

        [Fact]
        public async void GetById_ReturnsCorrectUser()
        {
            // Arrange
            
            context.Users.Add(new User() { Firstname = "John", Lastname = "Jones", Email = "johnjones@example.com", Password = "hunter2", Username = "jj2021rulez" });
            context.SaveChanges();

            // Act
            User jack = await Access.GetByID(1);

            // Assert
            Assert.Equal("Jack", jack.Firstname);
            Assert.Equal(1, jack.Id);
        }

        [Fact]
        public async void GetByID_ReturnsNull_IDNotFound()
        {
            // Arrange
            int userId = 0;

            // Act
            User user = await Access.GetByID(userId);

            // Assert
            
            Assert.Null(user);
        }

        [Fact]
        public async void GetAll_ReturnsOneUser_WhenOneInDb()
        {
            // Arrange + act
            List<User> users = await Access.GetAllUsers();

            // Assert
            Assert.Single(users);
        }

        [Fact]
        public async void GetAll_ReturnsNull_WhenDbEmpty()
        {
            // Arrange
            
            var user = context.Users.Find(1);
            context.Users.Remove(user);
            context.SaveChanges();

            // Act
            var users = await Access.GetAllUsers();

            // Assert
            Assert.Null(users);
        }

        [Fact]
        public async void GetAll_ReturnsMultiple_WhenMultipleInDb()
        {
            // Arrange
            
            context.Users.Add(new User { Firstname = "John", Lastname = "Jones", Email = "johnjones@example.com", Password = "hunter2", Username = "jj2021rulez" });
            context.SaveChanges();

            // Act
            List<User> users = await Access.GetAllUsers();

            // Assert
            Assert.Equal(2, users.Count);
        }

        [Fact]
        public async void GetByEmail_ReturnsSingleResult_OnSuccess()
        {
            // Arrange
            string email = "jacksmith@example.com";

            // Act
            User user = await Access.GetByEmail(email);

            // Assert
            Assert.IsType<User>(user);
        }

        [Fact]
        public async void GetByEmail_ReturnsResult_WithCorrectEmail()
        {
            // Arrange
            string email = "jacksmith@example.com";

            // Act
            User user = await Access.GetByEmail(email);

            // Assert
            Assert.Equal(email, user.Email);
        }

        [Fact]
        public async void GetByEmail_ReturnsNull_OnNotFound()
        {
            // Arrange
            string email = "test";

            // Act
            User user = await Access.GetByEmail(email);

            // Assert
            Assert.Null(user);
        }

        public void Dispose()
        {
            var context = new SocialContext(Options);

            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
