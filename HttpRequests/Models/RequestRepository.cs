using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace HttpRequests.Models
{
    public class RequestRepository : IRequestRepository
    {
        private static ConcurrentDictionary<string, RequestItem> _rqItems =
              new ConcurrentDictionary<string, RequestItem>();
        public Random random = new Random();

        public RequestRepository()
        {
            Add(new RequestItem { RequestData = "Initialized item"});
        }

        public IEnumerable<RequestItem> GetAll()
        {
            return _rqItems.Values;
        }

        public void Add(RequestItem item)
        {
            item.RequestKey = random.Next(0, 100).ToString();
            //item.RequestKey = Guid.NewGuid().ToString();
            _rqItems[item.RequestKey] = item;
        }

        public RequestItem Find(string key)
        {
            RequestItem item;
            _rqItems.TryGetValue(key, out item);
            return item;
        }

        public RequestItem Remove(string key)
        {
            RequestItem item;
            _rqItems.TryRemove(key, out item);
            return item;
        }

        public void Update(RequestItem item)
        {
            _rqItems[item.RequestKey] = item;
        }
    }
}
