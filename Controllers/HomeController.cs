using Microsoft.AspNetCore.Mvc;
using Shop_API_Baltaio.Models;
using Shop_API_Baltaio.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace Shop_API_Baltaio.Controllers
{
    [Route("v1")]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<string>> home([FromServices] DataContext DbContext)
        {
            User user = new User {Id = 1, Name = "Nelson Dos Santos", Password = "Voila544", Role = "Admin" };
            Category category = new Category {Id = 1, Title = "Acessorio" };
            Product product = new Product {Id = 1, Name = "Rolex", Price = 250000, CategoryId = 1, Category = category };

            try
            {
                DbContext.Users.Add(user);
                DbContext.Categories.Add(category);
                DbContext.Products.Add(product);
                await DbContext.SaveChangesAsync();
                return Ok("Default values inserted");
            }
            catch (System.Exception)
            {
                return BadRequest(new {message = "Cant insert all defaults values"});
            }
        }
    }
}