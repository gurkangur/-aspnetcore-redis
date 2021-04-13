using StackExchange.Redis;

namespace CartApi.Data
{
    public class CartContext
    {
        private readonly ConnectionMultiplexer _redis;
        public CartContext(ConnectionMultiplexer redis)
        {
            _redis = redis;
            Redis = redis.GetDatabase();
        }

        public IDatabase Redis { get; }
    }
}
