using FluentAssertions;
using NUnit.Framework;
using Reqres_APITests.controllers;
using System;

namespace Reqres_APITests.tests
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
            Assert.Multiple(() =>
            {
                usersListPageResult.Page.Should().Be(page);
                usersListPageResult.Data.Count.Should().BeGreaterThan(0);
            });          
        }
        
        [Test]
        public void GetExistingUserData()
        {
            var userId = "2";
            var userData = userController.GetUserProfile(userId);
            userData.Id.Should().Be(userId);
        }

        [Test]
        public void FindUserAtTheList()
        {
            var expectedUserId = "7";
            Assert.That(userController.CheckIsUserExistAtTheListOfUsers( expectedUserId));
        }

        [Test]
        public void CreateUserRequestReturnsUserIdAndCreatedAtDate()
        {
            var testStartingTime = DateTime.UtcNow;
            var createdUserData = userController.CreateUser(userName, job);
            var requestComplitedTime = DateTime.UtcNow;
            var userCreatedTime = createdUserData.CreatedAt;
            Assert.Multiple(() =>
            {
                createdUserData.Id.Should().NotBeNullOrEmpty();
                userCreatedTime.Should().BeAfter(testStartingTime).And.BeBefore(requestComplitedTime);
            });
        }

        [Test]
        public void UpdateUserAttributeAndVerifyCareteAndUpdateDate()
        {
            var newJobTitle = "AQA";
            var createdUser = userController.CreateUser(userName, job);
            createdUser.Job = newJobTitle;           
            var updateUserResponseData = userController.UpdateUser(createdUser);
            var userData = userController.GetUserProfile(createdUser.Id);

            Assert.Multiple(() =>
            {
                updateUserResponseData.Job.Should().Be(newJobTitle);
                userData.Job.Should().Be(newJobTitle);
                userData.CreatedAt.Should().Be(createdUser.CreatedAt);
                userData.UpdatedAt.Should().BeAfter(createdUser.UpdatedAt);
            });
        }

       [Test]
        public void DeleteUser() {
            var user = userController.CreateUser(userName, job);
            userController.DeleteUser(user.Id);
            var response = userController.MakeGetUserApiCall(user.Id);
            Assert.That((int)response.StatusCode == 404, $"Unexpected status code for deleted user : '{response.StatusCode}'");
        }

    }
}