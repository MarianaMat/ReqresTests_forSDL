using System.Collections.Generic;

namespace Reqres_APITests.dtos
{
    class UsersListPageResult
    {
        public int Page { get; set; }
        public int Per_page { get; set; }
        public int Total { get; set; }
        public int Total_pages { get; set; }
        public List<UserListDto> Data { get; set; }
    }
}
