using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shopping_cart_backend.Models;

namespace shopping_cart_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly ShoppingCartDbContext Context;

        public ShopController(ShoppingCartDbContext context)
        {
            this.Context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductsTbl>>> GetStudentsAync()
        {
            var data=await Context.ProductsTbls.ToListAsync();
            return Ok(data);
        }
        [HttpPost]
        public async Task<ActionResult<ProductsTbl>> AddProduct(Cart c)
        {
           // var cartItem = new Cart {ProductId=p.Id };
            await Context.Carts.AddAsync(c);
            await Context.SaveChangesAsync();
            return Ok(c);
        }
        [HttpGet]
        [Route("ProductList")]
        public async Task<ActionResult<List<ProductsTbl>>> ProductList()
        {
            var cartProductIds = await Context.Carts
        .Select(c => c.ProductId) // This selects only the ProductId column
        .Distinct() // This removes duplicate ProductIds if any
        .ToListAsync();

            var products = await Context.ProductsTbls
                .Where(p => cartProductIds.Contains(p.Id)) // This filters products that are in the cart
                .ToListAsync();

            return products;
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCart(int id)
        {
            // Assuming ProductId is a nullable foreign key, filter carts where ProductId has a value and matches the provided ID.
            var cartItems = await Context.Carts
                                         .Where(c => c.ProductId.HasValue && c.ProductId.Value == id)
                                         .ToListAsync();

            if (cartItems == null || !cartItems.Any())
            {
                return NotFound();
            }

            Context.Carts.RemoveRange(cartItems);
            await Context.SaveChangesAsync();

            return Ok("Data is Deleted");
        }


    }
}
