using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Shop_API_Baltaio.Models;
using System.Collections.Generic;
using Shop_API_Baltaio.Models;
using Shop_API_Baltaio.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Shop_API_Baltaio.Controllers
{
    [Route("v1/products")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product>>> get([FromServices]DataContext DbContext)
        {
            var products = await DbContext.Products.AsNoTracking()
                .Include(x=>x.Category).ToListAsync();
            return Ok(products);
        }
        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Product>> getById([FromServices]DataContext DbContext, int id)
        {
            var product = await DbContext.Products.
            AsNoTracking()
            .Include(x=>x.Category).Where(x=>x.Id == id).FirstOrDefaultAsync();
            if(product == null)
                return NotFound(new {message = "Produto não encontrado"});
            return Ok(product);

        }
        [HttpPost]
        [Route("")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<Product>> post(
            [FromServices] DataContext DbContext,
            [FromBody] Product model
        )
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                DbContext.Add(model);
                await DbContext.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest(new {message = "Não foi possível inserir o produto"});
            }
        }
        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<Product>> put(
            int id,
            [FromServices]DataContext DbContext,
            [FromBody]Product model
        )
        {
            var product = DbContext.Products.AsNoTracking()
            .Include(x=>x.Category).Where(x=>x.Id == id).FirstOrDefaultAsync();
            if(product == null)
                return BadRequest(new {message = "Produto não encontrado"});
            try
            {
                DbContext.Entry<Product>(model).State = EntityState.Modified;
                await DbContext.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest(new {message = "Não foi possível atualizar o produto"});
            }
        } 
    }
}