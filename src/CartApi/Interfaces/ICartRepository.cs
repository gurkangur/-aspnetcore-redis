using CartApi.Domain.Entities;
using System.Threading.Tasks;

namespace CartApi.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> GetCart(string userId);
        Task<Cart> UpdateCart(Cart cart);
        Task<bool> DeleteCart(string userName);
    }
}
