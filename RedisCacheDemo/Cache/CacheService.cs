
using StackExchange.Redis;

namespace RedisCacheDemo.Cache
{
    public class CacheService : ICacheService
    {
        private IDatabase _db;
        public CacheService()
        {
            _db = ConnectionHelper.Connection.GetDatabase();
        }
        public T GetData<T>(string key)
        {
            var value = _db.StringGet(key);
            if(!value.IsNull)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
            }
            return default;
        }
        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            var iSet = _db.StringSet(key, Newtonsoft.Json.JsonConvert.SerializeObject(value), expiryTime);
            return iSet;
        }
        public object RemoveData(string key)
        {
            bool isExist = _db.KeyExists(key);
            if (isExist)
            {
                return _db.KeyDelete(key);
            }
            return false;

        }
    }
}
