using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;
using System.Collections.Generic;
using Users.Controllers;
using Xunit;

namespace Users.Test
{
    public class UsersControllerTest
    {
        [Fact]
        public async void GetById_ReturnsUser_OnSuccess()
        {
            // Arrange
            User user = new User();
            Mock<IUserActions> service = new Mock<IUserActions>();
            service
                .Setup(x => x.GetByID(It.IsAny<int>()))
                .ReturnsAsync(user);

            UsersController sut = new UsersController(service.Object);

            // Act
            var response = await sut.GetByID(1);

            // Assert
            Assert.IsType<User>(response.Value);
        }

        [Fact]
        public async void GetById_Returns404_OnError()
        {
            // Arrange
            User user = null;
            Mock<IUserActions> service = new Mock<IUserActions>();
            service
                .Setup(x => x.GetByID(It.IsAny<int>()))
                .ReturnsAsync(user);

            UsersController sut = new UsersController(service.Object);

            // Act
            var response = await sut.GetByID(1);

            // Assert
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public async void GetAllUsers_ReturnsListOfUsers_OnSuccess()
        {
            // Arrange
            List<User> users = new List<User>()
            {
                new User(),
                new User(),
            };
            Mock<IUserActions> service = new Mock<IUserActions>();
            service
                .Setup(x => x.GetAllUsers())
                .ReturnsAsync(users);

            UsersController sut = new UsersController(service.Object);

            // Act
            var response = await sut.GetAllUsers();

            // Assert
            Assert.IsType<List<User>>(response.Value);
        }

        [Fact]
        public async void GetAllUsers_Returns404_OnError()
        {
            // Arrange
            List<User> users = null;
            Mock<IUserActions> service = new Mock<IUserActions>();
            service
                .Setup(x => x.GetAllUsers())
                .ReturnsAsync(users);

            UsersController sut = new UsersController(service.Object);

            // Act
            var response = await sut.GetByID(1);

            // Assert
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public async void GetByEmail_ReturnsUser_OnSuccess()
        {
            // Arrange
            User user = new User();
            Mock<IUserActions> service = new Mock<IUserActions>();
            service
                .Setup(x => x.GetByEmail(It.IsAny<string>()))
                .ReturnsAsync(user);

            UsersController sut = new UsersController(service.Object);

            // Act
            var response = await sut.GetByEmail("string");

            // Assert
            Assert.IsType<User>(response.Value);
        }

        [Fact]
        public async void GetByEmail_Returns404_OnError()
        {
            // Arrange
            User user = null;
            Mock<IUserActions> service = new Mock<IUserActions>();
            service
                .Setup(x => x.GetByEmail(It.IsAny<string>()))
                .ReturnsAsync(user);

            UsersController sut = new UsersController(service.Object);

            // Act
            var response = await sut.GetByEmail("string");

            // Assert
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public async void CreateUser_Returns204_OnSuccess() 
        {
            // Arrange
            bool success = true;
            Mock<IUserActions> service = new();
            service
                .Setup(x => x.CreateUser(It.IsAny<User>()))
                .ReturnsAsync(success);

            UsersController sut = new(service.Object);

            // Act
            var response = await sut.CreateUser(new User());

            // Assert
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public async void CreateUser_Returns404_OnError()
        {
            // Arrange
            bool success = false;
            Mock<IUserActions> service = new();
            service
                .Setup(x => x.CreateUser(It.IsAny<User>()))
                .ReturnsAsync(success);

            UsersController sut = new(service.Object);

            // Act
            var response = await sut.CreateUser(new User());

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async void DeleteUser_Returns204_OnSuccess()
        {
            // Arrange
            bool success = true;
            Mock<IUserActions> service = new();
            service
                .Setup(x => x.DeleteUser(It.IsAny<int>()))
                .ReturnsAsync(success);

            UsersController sut = new(service.Object);

            // Act
            var response = await sut.DeleteUser(1);

            // Assert
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public async void DeleteUser_Returns404_OnError()
        {
            // Arrange
            bool success = false;
            Mock<IUserActions> service = new();
            service
                .Setup(x => x.DeleteUser(It.IsAny<int>()))
                .ReturnsAsync(success);

            UsersController sut = new(service.Object);

            // Act
            var response = await sut.DeleteUser(1);

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async void UpdateUser_Returns204_OnSuccess()
        {
            // Arrange
            bool success = true;
            User user = new User();
            Mock<IUserActions> service = new();
            service
                .Setup(x => x.UpdateUser(It.IsAny<int>(), It.IsAny<User>()))
                .ReturnsAsync(success);

            UsersController sut = new(service.Object);

            // Act
            var response = await sut.UpdateUser(1, user);

            // Assert
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public async void UpdateUser_Returns404_OnError()
        {
            // Arrange
            bool success = false;
            User user = new User();
            Mock<IUserActions> service = new();
            service
                .Setup(x => x.UpdateUser(It.IsAny<int>(), It.IsAny<User>()))
                .ReturnsAsync(success);

            UsersController sut = new(service.Object);

            // Act
            var response = await sut.UpdateUser(1, user);

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }
    }
}
