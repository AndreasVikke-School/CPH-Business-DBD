using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace WebAPI.Managers
{
    public class ClientIpManager
    {
        public static string GetClientIp(HttpRequest request) {
              return request.HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }
}