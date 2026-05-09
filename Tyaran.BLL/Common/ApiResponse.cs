using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tyaran.BLL.Common
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;

        public static ApiResponse Success(string message)
            => new() { IsSuccess = true, Message = message };

        public static ApiResponse Fail(string message)
            => new() { IsSuccess = false, Message = message };
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T? Data { get; set; }
    }
}
