using Microsoft.AspNetCore.Mvc;
using Veridiano_Midterm_Store.Data;
using Veridiano_Midterm_Store.Models;

namespace Veridiano_Midterm_Store.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Add(int id)
        {
            var product = _db.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            var cartItem = new CartItem();

            cartItem.ProductId = product.Id;
            cartItem.ProductName = product.Name;
            cartItem.Price = product.Price;
            cartItem.Quantity = 1;

            _db.CartItems.Add(cartItem);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var cartItems = _db.CartItems.ToList();

            return View(cartItems);
        }

        [HttpPost]
        public IActionResult Update(int id, int quantity)
        {
            var cartItem = _db.CartItems.Find(id);

            if (cartItem == null)
            {
                return NotFound();
            }

            cartItem.Quantity = quantity;

            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpPost]
public IActionResult Remove(int id)
{
    var cartItem = _db.CartItems.Find(id);

    if (cartItem == null)
    {
        return NotFound();
    }

    _db.CartItems.Remove(cartItem);
    _db.SaveChanges();

    return RedirectToAction("Index");
}
    }
}