using System;
using System.Collections.Generic;
using System.Text;

namespace ReqresTests_forSDL.dtos
{
    class UsersListPageResult
    {
        public int Page { get; set; }
        public int Per_page { get; set; }
        public int Total { get; set; }
        public int Total_pages { get; set; }
        public List<UserFromListDto> Data { get; set; }
    }
}
