using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReqresTests_forSDL;
using ReqresTests_forSDL.dtos;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReqresTestProject_SDL.controllers
{
    class UsersController : BaseController
    {
        string endpoint;
        public UsersController()
        {
            endpoint = "/api/users";
        }
        
        public UsersListPageResult GetListOfUsers(int page) {
            var response = MyRestClient.GET(endpoint+ "?page=" + page.ToString());
            VerifyStatusCode(response, 200);
            return GetDtoFromResponse<UsersListPageResult>(response);
        }

        public bool IsUserAtTheListOfUsers(UsersListPageResult page, String id) {
            try
            {
                var user = page.Data.First(s => s.Id == id);
                return true;
            }
            catch { new InvalidOperationException("Sequence contains no matching element");
                return false;
            }
            
        }
 
        public UserProfileDto CreateUser(String name, String job)
        {
            var shortDto = new UserProfileDto
            {
                Name = name,
                Job = job
            };
            var response = MyRestClient.POST(endpoint, shortDto);
            VerifyStatusCode(response, 201);
            return GetDtoFromResponse<UserProfileDto>(response);
        }

        public UserProfileDto UpdateUser(UserProfileDto userDto)
        {
            var response = MyRestClient.PUT(endpoint+userDto.Id, userDto);
            VerifyStatusCode(response, 200);
            return GetDtoFromResponse<UserProfileDto>(response);
        }
        public UserProfileDto PatchUser(UserProfileDto userDto)
        {
            var response = MyRestClient.PATCH(endpoint + userDto.Id, userDto);
            VerifyStatusCode(response, 200);
            return GetDtoFromResponse<UserProfileDto>(response);
        }
        public void DeleteUser(String userId)
        {
            var response = MyRestClient.DELETE(endpoint + userId);
            VerifyStatusCode(response, 204);

        }




        //public static IList<FullUserProfile> GetFullUserProfileFromResponse(IRestResponse response) 
        //{
        //    var jsonResponse = JObject.Parse(response.Content);
        //    var UsersList = JsonConvert.DeserializeObject<List<FullUserProfile>>(jsonResponse["data"].ToString());

        //    return UsersList;
        //}

        


        // Patch update /api/users/2
    }
}
