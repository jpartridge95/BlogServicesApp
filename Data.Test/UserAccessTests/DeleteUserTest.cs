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
    public class DeleteUserTest : IDisposable
    {
        UserAccess Access;
        DbContextOptions<SocialContext> Options;
        SocialContext context;

        public DeleteUserTest()
        {
            Options = new DbContextOptionsBuilder<SocialContext>()
                .UseInMemoryDatabase(databaseName: "DeleteUserDB")
                .Options;

            context = new SocialContext(Options);
            context.Database.EnsureCreated();

            context.Users.Add(new User() { Firstname = "Jack", Lastname = "Smith", Email = "jacksmith@example.com", Password = "securepw", Username = "jsmith1" });
            context.SaveChanges();

            Access = new UserAccess(context);
        }

        [Fact]
        public async void DeleteUser_DeletesUser()
        {
            

            // Act
            await Access.DeleteUser(1);
            List<User> users = context.Users.ToList();

            // Assert
            Assert.Empty(users);
        }

        [Fact]
        public async void DeleteUser_ReturnsTrue_OnSuccessfulDelete()
        {
            // Arrange + Act
            bool success = await Access.DeleteUser(1);

            // Assert
            Assert.True(success);
        }

        [Fact]
        public async void DeleteUser_ReturnsFalse_OnFailedDelete()
        {
            // Arrange + Act
            bool success = await Access.DeleteUser(2);

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
