using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Shop_API_Baltaio.Models;
using System.Collections.Generic;
using Shop_API_Baltaio.Data;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Shop_API_Baltaio.Controllers
{
    [Route("v1/categories")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        [ResponseCache(VaryByHeader = "User-Agent", Location = ResponseCacheLocation.Any, Duration = 30)]
        public async Task<ActionResult<List<Category>>> get([FromServices]DataContext DbContext)
        {
            var categories = await DbContext.Categories.AsNoTracking().ToListAsync();
            return Ok(categories);
        }
        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Category>> getById(int id, [FromServices]DataContext DbContext)
        {
            var category = await DbContext.Categories.AsNoTracking().Where(x=>x.Id == id).FirstOrDefaultAsync();
            if(category == null)
                return NotFound(new {message = "Não foi possível encontrar esta categoria"});
            return Ok(category);
        }
        [HttpPost]
        [Route("")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<Category>> post([FromServices]DataContext DbContext,[FromBody]Category model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                DbContext.Categories.Add(model);
                await DbContext.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest(new {message = "Não foi possível criar a categoria"});
            }
        }
        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<Category>> put(
            int id,
            [FromServices]DataContext DbContext,
            [FromBody]Category model)
        {
            var category = await DbContext.Categories.AsNoTracking().Where(x=>x.Id == id).FirstOrDefaultAsync();
            if(category == null)
                return NotFound(new {message = "categoria não encontrada"});
            try
            {
                DbContext.Entry<Category>(model).State = EntityState.Modified;
                await DbContext.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception)
            {
                return NotFound("Não foi possível efetuar a atualização da categoria");
            }
        }
    }
}