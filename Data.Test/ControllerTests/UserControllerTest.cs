using Models;
using Xunit;
using System;
using System.Collections.Generic;
using Moq;
using Data.Controllers;
using Data.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace Data.Test.ControllerTests
{
    public class UserControllerTest
    {
        [Fact]
        public async void GetById_ReturnsSingleResult()
        {
            // Arrange
            var user = new User()
            {
                Email = "test@test.com",
                Username =  "test",
                Password = "test",
                Id = 1,
                Firstname = "John",
                Lastname = "Smith"
            };
            var access = new Mock<UserAccess>(null);
            access.Setup(a => a.GetByID(It.IsAny<int>()))
                .ReturnsAsync(user);

            var controller = new UsersController(null, access.Object);

            // Act
            var result = await controller.GetById(1);

            // Assert
            Assert.Equal(user, result.Value);
        }

        [Fact]
        public async void GetById_Returns404_OnError()
        {
            // Arrange
            User user = null;
            var access = new Mock<UserAccess>(null);
            access.Setup(a => a.GetByID(It.IsAny<int>()))
                .ReturnsAsync(user);

            var controller = new UsersController(null, access.Object);

            // Act
            var result = await controller.GetById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void SecretHiddenEndpoint_ReturnsCorrectly()
        {
            // Arrange
            var users = new List<User>()
            {
                new User()
                {
                    Email = "test@test.com",
                    Username =  "test",
                    Password = "test",
                    Id = 1,
                    Firstname = "John",
                    Lastname = "Smith"
                },
                new User()
                {
                    Email = "test@test.com",
                    Username =  "test1",
                    Password = "test1",
                    Id = 2,
                    Firstname = "John",
                    Lastname = "Smith"
                }
            };
            var access = new Mock<UserAccess>(null);
            access.Setup(a => a.GetAllUsers())
                .ReturnsAsync(users);

            var controller = new UsersController(null, access.Object);

            // Act
            var result = await controller.SecretHiddenEndpoint();

            // Assert
            Assert.Equal(2, result.Value.Count);
        }

        [Fact]
        public async void SecretHiddenEndpoint_Returns404_OnNoData()
        {
            // Arrange
            List<User> users = null;
            var access = new Mock<UserAccess>(null);
            access.Setup(a => a.GetAllUsers())
                .ReturnsAsync(users);

            var controller = new UsersController(null, access.Object);

            // Act
            var result = await controller.SecretHiddenEndpoint();

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void DeleteById_Returns204_OnSuccess()
        {
            // Arrange
            bool success = true;
            var access = new Mock<UserAccess>(null);
            access.Setup(a => a.DeleteUser(It.IsAny<int>()))
                .ReturnsAsync(success);

            var controller = new UsersController(null, access.Object);

            // Act
            var result = await controller.DeleteById(1);

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async void DeleteById_Returns404_OnError()
        {
            // Arrange
            bool success = false;
            var access = new Mock<UserAccess>(null);
            access.Setup(a => a.DeleteUser(It.IsAny<int>()))
                .ReturnsAsync(success);

            var controller = new UsersController(null, access.Object);

            // Act
            var result = await controller.DeleteById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void CreateUser_Returns204_OnSuccess()
        {
            // Arrange
            User user = new User();
            bool success = true;
            var access = new Mock<UserAccess>(null);
            access.Setup(a => a.CreateUser(It.IsAny<User>()))
                .ReturnsAsync(success);

            var controller = new UsersController(null, access.Object);

            // Act
            var result = await controller.CreateUser(user);

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async void CreateUser_Returns404_OnError()
        {
            // Arrange
            User user = new User();
            bool success = false;
            var access = new Mock<UserAccess>(null);
            access.Setup(a => a.CreateUser(It.IsAny<User>()))
                .ReturnsAsync(success);

            var controller = new UsersController(null, access.Object);

            // Act
            var result = await controller.CreateUser(user);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void EditUser_Returns204_OnSuccess()
        {
            // Arrange
            User user = new User();
            bool success = true;
            var access = new Mock<UserAccess>(null);
            access.Setup(a => a.UpdateUser(It.IsAny<int>(), It.IsAny<User>()))
                .ReturnsAsync(success);

            var controller = new UsersController(null, access.Object);

            // Act
            var result = await controller.EditUserInfo(1, user);

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async void EditUser_Returns404_OnError()
        {
            // Arrange
            User user = new User();
            bool success = false;
            var access = new Mock<UserAccess>(null);
            access.Setup(a => a.UpdateUser(It.IsAny<int>(), It.IsAny<User>()))
                .ReturnsAsync(success);

            var controller = new UsersController(null, access.Object);

            // Act
            var result = await controller.EditUserInfo(1, user);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void GetByEmail_ReturnsUser_OnSuccess()
        {
            // Arrange
            User user = new User();
            
            var access = new Mock<UserAccess>(null);
            access.Setup(a => a.GetByEmail(It.IsAny<string>()))
                .ReturnsAsync(user);

            var controller = new UsersController(null, access.Object);

            // Act
            var result = await controller.GetByEmail("1");

            // Assert
            Assert.IsType<User>(result.Value);
        }

        [Fact]
        public async void GetByEmail_Returns404_OnError()
        {
            // Arrange
            User user = null;

            var access = new Mock<UserAccess>(null);
            access.Setup(a => a.GetByEmail(It.IsAny<string>()))
                .ReturnsAsync(user);

            var controller = new UsersController(null, access.Object);

            // Act
            var result = await controller.GetByEmail("1");

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
