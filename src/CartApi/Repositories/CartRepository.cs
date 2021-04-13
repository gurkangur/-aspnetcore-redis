using CartApi.ContractResolvers;
using CartApi.Data;
using CartApi.Domain.Entities;
using CartApi.Interfaces;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace CartApi.Repositories
{
    public class CartRepository : ICartRepository
    {
        private static JsonSerializerSettings _serializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
            ContractResolver = new PrivateSetterContractResolver()
        };
        private readonly CartContext _context;
        public CartRepository(CartContext context)
        {
            _context = context;
        }

        public async Task<Cart> GetCart(string userId)
        {
            var cart = await _context.Redis.StringGetAsync(userId);
            return cart.IsNullOrEmpty ? null : await DeserializeJson<Cart>(cart);
        }

        public async Task<Cart> UpdateCart(Cart cart)
        {
            var serialized = await SerializeToJson(cart);
            var cartEdit = await _context.Redis.StringSetAsync(cart.UserId, serialized);
            if (!cartEdit) return null;

            return await GetCart(cart.UserId);
        }
        public async Task<bool> DeleteCart(string userId)
        {
            return await _context.Redis.KeyDeleteAsync(userId);
        }
        private async Task<string> SerializeToJson(object o)
        {
            return await Task.Factory.StartNew(() => JsonConvert.SerializeObject(o, _serializerSettings));
        }

        private async Task<T> DeserializeJson<T>(string json)
        {
            if (json == null)
                return default(T);

            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(json, typeof(T));
            }

            var obj = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(json, _serializerSettings));
            return obj;
        }
    }
}
