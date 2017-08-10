using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Caching.Redis
{
    public interface IRedisCachingService : ICachingService
    {
        //List<T> GetList<T>(string key);
        //bool SetOne<T>(string key, T value);
        //bool SetOne<T>(string key, T value, TimeSpan timeOut);
    }
}
