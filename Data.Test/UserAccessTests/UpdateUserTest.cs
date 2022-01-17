using Data.Context;
using Data.DataAccess;
using Microsoft.EntityFrameworkCore;
using Models;
using Xunit;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Data.Test.UserAccessTests
{
    public class UpdateUserTest : IDisposable
    {
        UserAccess Access;
        DbContextOptions<SocialContext> Options;
        SocialContext context;

        public UpdateUserTest()
        {
            Options = new DbContextOptionsBuilder<SocialContext>()
                .UseInMemoryDatabase(databaseName: "UpdateUserDB")
                .Options;

            context = new SocialContext(Options);
            context.Database.EnsureCreated();

            context.Users.Add(new User() { Firstname = "Jack", Lastname = "Smith", Email = "jacksmith@example.com", Password = "securepw", Username = "jsmith1" });
            context.SaveChanges();

            Access = new UserAccess(context);
        }

        [Fact]
        public async void UpdateUser_SuccessfullyUpdatesUser()
        {
            // Arrange
            User user = new User() { Firstname = "Jack", Lastname = "Smith", Email = "jacksmith@example.com", Password = "newpwhowdy", Username = "jsmith2" };
            

            // Act
            await Access.UpdateUser(1, user);
            User returned = context.Users.Find(1);

            // Assert
            Assert.Equal("newpwhowdy", returned.Password);
            Assert.Equal("jsmith2", returned.Username);
        }

        [Fact]
        public async void UpdateUser_ReturnsTrue_OnSuccess()
        {
            // Arrange
            User user = new User() { Firstname = "Jack", Lastname = "Smith", Email = "jacksmith@example.com", Password = "newpwhowdy", Username = "jsmith1" };

            // Act
            bool success = await Access.UpdateUser(1, user);

            // Assert
            Assert.True(success);
        }

        [Fact]
        public async void UpdateUser_ReturnsFalse_OnNullInput()
        {
            // Arrange
            User? user = null;

            // Act
            bool success = await Access.UpdateUser(1, user);

            // Assert
            Assert.False(success);
        }

        [Fact]
        public async void UpdateUser_ReturnsFalse_WhenIDNotFound()
        {
            // Arrange
            User user = new User() { Firstname = "Jack", Lastname = "Smith", Email = "jacksmith@example.com", Password = "newpwhowdy", Username = "jsmith1" };

            // Act
            bool success = await Access.UpdateUser(10, user);

            // Assert
            Assert.False(success);
        }

        public void Dispose()
        {
            var context = new SocialContext(Options);

            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}