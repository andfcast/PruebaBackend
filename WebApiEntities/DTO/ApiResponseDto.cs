using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiEntities.DTO
{
    public class ApiResponseDto
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string StatusName { get; set; }
        public Boolean IsSuccess { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public Object Data { get; set; }
    }
}
