using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HttpRequests.Models;
using Microsoft.AspNetCore.Http;

namespace HttpRequests.Controllers
{

    public class HomeController : Controller
    {
        private IHttpContextAccessor _accessor;
        public IRequestRepository _requestItems { get; set; }

        public HomeController(IRequestRepository requestItems, IHttpContextAccessor accessor)
        {
            _requestItems = requestItems;
            _accessor = accessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("request/getall")]
        public IEnumerable<RequestItem> GetAll()
        {
            var resultpath = string.Join("/", new[]
            {
                Request.Host,
                RouteData.Values["controller"],
                RouteData.Values["action"]
            });
            Logger.Log(Request.Method, _accessor.HttpContext.Connection.LocalIpAddress.ToString(), resultpath);
            return _requestItems.GetAll();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(string id)
        {
            var resultpath = string.Join("/", new[]
            {
                Request.Host,
                RouteData.Values["controller"],
                RouteData.Values["action"]
            });
            Logger.Log(Request.Method, _accessor.HttpContext.Connection.LocalIpAddress.ToString(), resultpath);
            var item = _requestItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] RequestItem item)
        {
            var resultpath = string.Join("/", new[]
            {
                Request.Host,
                RouteData.Values["controller"],
                RouteData.Values["action"]
            });
            Logger.Log(Request.Method, _accessor.HttpContext.Connection.LocalIpAddress.ToString(), resultpath);
            if (item == null)
            {
                return BadRequest();
            }
            _requestItems.Add(item);
            return CreatedAtRoute(Request.Method, new { id = item.RequestKey }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] RequestItem item)
        {
            var resultpath = string.Join("/", new[]
            {
                Request.Host,
                RouteData.Values["controller"],
                RouteData.Values["action"]
            });
            Logger.Log(Request.Method, _accessor.HttpContext.Connection.LocalIpAddress.ToString(), resultpath);
            if (item == null || item.RequestKey != id)
            {
                return BadRequest();
            }

            var todo = _requestItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _requestItems.Update(item);
            return new NoContentResult();
        }

        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] RequestItem item, string id)
        {
            var resultpath = string.Join("/", new[]
            {
                Request.Host,
                RouteData.Values["controller"],
                RouteData.Values["action"]
            });
            Logger.Log(Request.Method, _accessor.HttpContext.Connection.LocalIpAddress.ToString(), resultpath);
            if (item == null)
            {
                return BadRequest();
            }

            var todo = _requestItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            item.RequestKey = todo.RequestKey;

            _requestItems.Update(item);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var resultpath = string.Join("/", new[]
            {
                Request.Host,
                RouteData.Values["controller"],
                RouteData.Values["action"]
            });
            Logger.Log(Request.Method, _accessor.HttpContext.Connection.LocalIpAddress.ToString(), resultpath);
            var todo = _requestItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _requestItems.Remove(id);
            return new NoContentResult();
        }
    }
}
