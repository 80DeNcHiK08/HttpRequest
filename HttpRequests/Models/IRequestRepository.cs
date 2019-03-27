using System.Collections.Generic;

namespace HttpRequests.Models
{
    public interface IRequestRepository
    {
        void Add(RequestItem item);
        IEnumerable<RequestItem> GetAll();
        RequestItem Find(string key);
        RequestItem Remove(string key);
        void Update(RequestItem item);
    }
}
