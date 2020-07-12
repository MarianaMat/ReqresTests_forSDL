using Reqres_APITests.dtos;
using RestSharp;
using System;
using System.Linq;

namespace Reqres_APITests.controllers
{
    class UsersController : BaseController
    {
        readonly string endpoint;
        public UsersController()
        {
            endpoint = "/api/users";
        }

        public UsersListPageResult GetListOfUsers(int page = 1)
        {
            var response = MyRestClient.GET(endpoint + "?page=" + page.ToString());
            VerifyStatusCode(response, 200);
            return GetDtoFromResponse<UsersListPageResult>(response);
        }

        public bool CheckIsUserExistAtTheListOfUsers(string userId)
        {
            var currentPage = 1;
            UsersListPageResult usersListPageResult;
            int? pages = null;
            do
            {
                usersListPageResult = GetListOfUsers(currentPage);
                if (usersListPageResult.Data.Any(s => s.Id == userId)) { return true; }
                pages = usersListPageResult.Total_pages;
                currentPage += 1;
            }
            while (currentPage == pages);

            return false;
        }

        public UserProfileDto CreateUser(string name, string job, int expectedStatusCode = 201)
        {
            var shortDto = new UserProfileDto
            {
                Name = name,
                Job = job
            };
            var response = MyRestClient.POST(endpoint, shortDto);
            VerifyStatusCode(response, expectedStatusCode);
            return GetDtoFromResponse<UserProfileDto>(response);
        }
        public IRestResponse MakeGetUserApiCall(string userId)
        {
            return MyRestClient.GET(endpoint + userId);
        }

        public UserProfileDto GetUserProfile(string userId)
        {
            var response = MakeGetUserApiCall(userId);
            return GetDtoFromResponse<UserProfileDto>(response);
        }

        public UserProfileDto UpdateUser(UserProfileDto userDto, int expectedStatusCode = 200)
        {
            var response = MyRestClient.PUT(endpoint + userDto.Id, userDto);
            VerifyStatusCode(response, expectedStatusCode);
            return GetDtoFromResponse<UserProfileDto>(response);
        }
        public UserProfileDto PatchUser(UserProfileDto userDto, int expectedStatusCode = 200)
        {
            var response = MyRestClient.PATCH(endpoint + userDto.Id, userDto);
            VerifyStatusCode(response, expectedStatusCode);
            return GetDtoFromResponse<UserProfileDto>(response);
        }
        public void DeleteUser(String userId, int expectedStatusCode = 204)
        {
            var response = MyRestClient.DELETE(endpoint + userId);
            VerifyStatusCode(response, expectedStatusCode);
        }

    }
}
