﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using TestApp.EntityModels;

namespace TestApp.Controllers
{
    public class HomeController : Controller
    {
        private IHttpContextAccessor _httpContextAccessor;
        private sqldbContext _context;
        private IDistributedCache _cache;

        public HomeController(IHttpContextAccessor httpContextAccessor, sqldbContext sqldbContext, IDistributedCache distributedCache)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = sqldbContext;
            _cache = distributedCache;
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
        public async Task<ActionResult> Test()
        {
            var data = await _context.Companies.ToListAsync();
            return Json(data);
        }
        public async Task<ActionResult> Test2()
        {

            return Json("kursundan beterdi o son sozleri");
        }
        public async Task<ActionResult> Test3()
        {

            return Json("bu gece karakolluk olabilirim");
        }

        public async Task<ActionResult> Test4()
        {

            return Json("ilk fırsatta sana gelmek istedim");
        }

        public async Task<ActionResult> Redis()
        {
            await _cache.SetStringAsync("name","aliveli", options:new DistributedCacheEntryOptions
            { 
                SlidingExpiration = TimeSpan.FromSeconds(50)
            });
            return Json(await _cache.GetStringAsync("name"));
        }
    }
}
