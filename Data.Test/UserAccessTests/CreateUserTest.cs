using Data.Context;
using Data.DataAccess;
using Microsoft.EntityFrameworkCore;
using Models;
using Xunit;
using System.Linq;
using System;

namespace Data.Test.UserAccessTests
{
    public class CreateUserTest : IDisposable
    {
        UserAccess Access;
        DbContextOptions<SocialContext> Options;
        SocialContext context;

        public CreateUserTest()
        {
            Options = new DbContextOptionsBuilder<SocialContext>()
                .UseInMemoryDatabase(databaseName: "CreateUserDB")
                .Options;

            context = new SocialContext(Options);
            context.Database.EnsureCreated();

            context.Users.Add(new User() { Firstname = "Jack", Lastname = "Smith", Email = "jacksmith@example.com", Password = "securepw", Username = "jsmith1" });
            context.SaveChanges();

            Access = new UserAccess(context);
        }

        [Fact]
        public async void CreateUser_AddsUserToDb()
        {
            // arrange
            User user = new User() {Firstname = "John", Lastname = "Jones", Email = "johnjones@example.com", Password = "hunter2", Username = "jj2021rulez"};
            context = new SocialContext(Options);

            // act
            await Access.CreateUser(user);
            User? john = context.Users.AsQueryable()
                .Where(x => x.Firstname == "John")
                .FirstOrDefault();
                

            // assert
            Assert.Equal("John", john.Firstname);
            Assert.Equal("Jones", john.Lastname);
            Assert.Equal("johnjones@example.com", john.Email);
            Assert.Equal("hunter2", john.Password);
            Assert.Equal("jj2021rulez", john.Username);
        }

        [Fact]
        public async void CreateUser_ReturnsTrue_OnSuccess()
        {
            //Arrange
            User user = new User() { Firstname = "John", Lastname = "Jones", Email = "johnjones@example.com", Password = "hunter2", Username = "jj2021rulez" };

            // Act
            bool success = await Access.CreateUser(user);

            // Assert
            Assert.True(success);
        }

        [Fact]
        public async void CreateUser_ReturnsFalse_NullData()
        {
            // Arrange
            User user = new User() { Firstname = null, Lastname = "Jones", Email = "johnjones@example.com", Password = "hunter2", Username = "jj2021rulez" };

            // Act
            bool success = await Access.CreateUser(user);

            // Assert
            Assert.False(success);
        }

        [Fact]
        public async void CreateUser_ReturnsFalse_NotUnique()
        {
            // Arrange
            User user = new User() { Firstname = "John", Lastname = "Jones", Email = "johnjones@example.com", Password = "hunter2", Username = "jj2021rulez" };

            // Act
            bool successTrue = await Access.CreateUser(user);
            bool successFalse = await Access.CreateUser(user);

            // Assert
            Assert.True(successTrue);
            Assert.False(successFalse);

        }

        public void Dispose()
        {
            var context = new SocialContext(Options);

            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}