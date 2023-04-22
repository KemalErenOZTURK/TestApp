using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace TestApp.Controllers
{
    public class HomeController : Controller
    {
        private IHttpContextAccessor _httpContextAccessor;

        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            var ip = _httpContextAccessor.HttpContext.Request.Headers["X-Real-IP"].ToString();

            //string _ipAddress = string.Empty;
            //IPAddress? remoteIpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
            //if (remoteIpAddress != null && remoteIpAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            //    _ipAddress = remoteIpAddress.ToString();
            //else
            //{
            //    if (remoteIpAddress != null && remoteIpAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
            //    {
            //        _ipAddress = System.Net.Dns.GetHostEntry(remoteIpAddress).AddressList.First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();
            //    }
            //    else
            //    {
            //        var xForwardedForHeader = _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"].ToString();

            //        if (!string.IsNullOrEmpty(xForwardedForHeader))
            //        {
            //            var ipAddresses = xForwardedForHeader.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            //            if (ipAddresses.Length > 0)
            //            {
            //                foreach (var ipAddress in ipAddresses)
            //                {
            //                    if (IPAddress.TryParse(ipAddress.Trim(), out remoteIpAddress))
            //                    {
            //                        if (remoteIpAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            //                        {
            //                            _ipAddress = remoteIpAddress.ToString();
            //                            break;
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            return Json(ip);
        }
    }
}
