using CartApi.Domain.Entities;
using CartApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CartApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        [HttpGet("userId")]
        public async Task<ActionResult<Cart>> GetCart(string userId)
        {
            var cart = await _cartRepository.GetCart(userId);
            if (cart != null)
                return Ok(cart);
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<Cart>> UpdateCart(Cart cart)
        {
            var updatedCart = await _cartRepository.UpdateCart(cart);
            return Ok(updatedCart);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCart(string userId)
        {
            await _cartRepository.DeleteCart(userId);
            return NoContent();
        }
    }
}
