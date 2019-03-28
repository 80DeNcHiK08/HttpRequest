using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HttpRequests.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace HttpRequests.Controllers
{
    public class HomeController : Controller
    {
        private IHttpContextAccessor _accessor;
        public IRequestRepository _requestItems { get; set; }
        protected string datetime = DateTime.Now.ToString();

        public HomeController(IRequestRepository requestItems, IHttpContextAccessor accessor)
        {
            _requestItems = requestItems;
            _accessor = accessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Home/GetAll",
            Name ="getall")]
        public IEnumerable<RequestItem> GetAll()
        {
            var resultpath = string.Join("/", new[]
            {
                Request.Host,
                RouteData.Values["controller"],
                RouteData.Values["action"]
            });
            Logger.Log(datetime, Request.Method, _accessor.HttpContext.Connection.LocalIpAddress.ToString(), resultpath);

            return _requestItems.GetAll();
        }

        [Route("Home/Create",
            Name ="create")]
        public IActionResult Create(RequestItem item)
        {
            var resultpath = string.Join("/", new[]
            {
                Request.Host,
                RouteData.Values["controller"],
                RouteData.Values["action"]
            });
            Logger.Log(datetime, Request.Method, _accessor.HttpContext.Connection.LocalIpAddress.ToString(), resultpath);

            if (item.RequestData == null)
            {
                return BadRequest();
            }
            _requestItems.Add(item);
            return new NoContentResult();
        }

        [Route("Home/Update",
            Name ="update")]
        public IActionResult Update(RequestItem item, string NewKey)
        {
            Request.Method = "PUT";
            var resultpath = string.Join("/", new[]
            {
                Request.Host,
                RouteData.Values["controller"],
                RouteData.Values["action"]
            });
            Logger.Log(datetime, Request.Method, _accessor.HttpContext.Connection.LocalIpAddress.ToString(), resultpath);

            if (item.RequestKey == null || NewKey == null)
            {
                return BadRequest();
            }
            var toUpdate = _requestItems.Find(item.RequestKey);
            if (toUpdate == null)
            {
                return NotFound();
            }

            //item.RequestKey = toUpdate.RequestKey;
            toUpdate.RequestKey = NewKey;

            _requestItems.Update(toUpdate);
            return new NoContentResult();
        }

        [Route("Home/Delete",
            Name ="delete")]
        public IActionResult Delete(string key)
        {
            Request.Method = "DELETE";
            var resultpath = string.Join("/", new[]
            {
                Request.Host,
                RouteData.Values["controller"],
                RouteData.Values["action"]
            });
            Logger.Log(datetime, Request.Method, _accessor.HttpContext.Connection.LocalIpAddress.ToString(), resultpath);

            var todo = _requestItems.Find(key);
            if (todo == null)
            {
                return NotFound();
            }

            _requestItems.Remove(key);
            return new NoContentResult();
        }
    }
}
