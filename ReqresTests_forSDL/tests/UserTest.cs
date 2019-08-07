using FluentAssertions;
using NUnit.Framework;
using ReqresTestProject_SDL.controllers;

namespace Tests
{
    public class UserTests
    {
        readonly UsersController userController = new UsersController();
        readonly string userName = "test user name";
        readonly string job = "QA";

        [Test]
        public void GetListOfUsersOnSpecificPage()
        {
            var page = 2;
            var usersListPageResult = userController.GetListOfUsers(page);
            Assert.AreEqual(usersListPageResult.Page, page);
        }
        [Test]
        public void FindUserAtTheList()
        {
            var page = 2;
            var expectedUserId = "4";
            var usersListPageResult = userController.GetListOfUsers(page);
            var userOnTheList = userController.IsUserAtTheListOfUsers(usersListPageResult, expectedUserId);
            Assert.IsTrue(userOnTheList);
        }

        [Test]
        public void CreateUser_returns_UserId()
        {
            var user = userController.CreateUser(userName, job);
            user.Id.Should().NotBeNullOrEmpty();
            user.CreatedAt.Should().NotBe(user.UpdatedAt);
        }

        [Test]
        public void CreateUser_returns_CreatedAtValue()
        {
            var user = userController.CreateUser(userName, job);
            user.CreatedAt.Should().NotBe(user.UpdatedAt);
        }

        [Test]
        public void UpdateUserAttribute()
        {
            var user = userController.CreateUser(userName, job);
            user.Job = "AQA";
            user = userController.UpdateUser(user);
            Assert.IsTrue(user.Job == "AQA");
        }

        [Test]
        public void UpdateUserAttributeValidateUpdatedAt()
        {
            var user = userController.CreateUser(userName, job);
            user.Job = "AQA";
            user = userController.UpdateUser(user);
            user.UpdatedAt.Should().BeAfter(user.CreatedAt);
        }

       [Test]
        public void DeleteUser() {
            var user = userController.CreateUser(userName, job);
            userController.DeleteUser(user.Id);
        }

    }
}