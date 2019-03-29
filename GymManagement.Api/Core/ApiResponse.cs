using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymManagement.Api.Core
{
    public class ApiResponse : ActionResult
    {
        public object Data { get; set; }
        public string Message { get; set; }

        public static ApiResponse Success(object data)
        {
            return new ApiResponse { Data = data };
        }

        public static ApiResponse Fail(string message)
        {
            return new ApiResponse { Message = message };
        }
    }
}
