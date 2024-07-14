using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Shop_API_Baltaio.Models;
using System.Collections.Generic;
using System.Linq;
using Shop_API_Baltaio.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Shop_API_Baltaio.Services;
using Microsoft.AspNetCore.Authorization;

namespace Shop_API_Baltaio.Controllers
{
    [Route("v1/users")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<List<User>>> get([FromServices]DataContext DbContext)
        {
            var users = await DbContext.Users.AsNoTracking().ToListAsync();
            return Ok(users);
        }
        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<User>> getById(int id,[FromServices]DataContext DbContext)
        {
            var user = await DbContext.Users.AsNoTracking().Where(x=>x.Id == id).FirstOrDefaultAsync();
            if(user == null)
                return NotFound(new {message = "Utilizador não encontrado"});
            return Ok(user);
        }
        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> post([FromServices]DataContext DbContext,[FromBody]User model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                model.Role = "Manager";
                DbContext.Users.Add(model);
                await DbContext.SaveChangesAsync();
                model.Password = "";
                return Ok(model);
            }
            catch (System.Exception)
            {
                return BadRequest(new {message = "Não foi possível criar o utilizador"});
            }
        }
        [HttpPut]
        [Route("{id:int}")] 
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<User>> put(
            int id,
            [FromServices]DataContext DbContext,
            [FromBody] User model
        )
        {
            var user = await DbContext.Users.AsNoTracking().Where(x=>x.Id == id).FirstOrDefaultAsync();
            if(user == null)
                return NotFound(new {message = "Utilizador não encontrado"});
            try
            {
                DbContext.Entry<User>(model).State = EntityState.Modified;
                await DbContext.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest(new {message = "Não foi possivel atualizar o utilizador"});
            }
        }
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> authenticate(
            [FromServices]DataContext DbContext,
            [FromBody]User model){
            var user = await DbContext.Users.AsNoTracking()
            .Where(x=>x.Name == model.Name && x.Password == model.Password).FirstOrDefaultAsync();

            if(user == null)
                return NotFound(new {message = "Utilizador ou senha incorreta"});
            var token = TokenService.generateToken(user);
            user.Password = "";
            return new {
                token = token,
                user = user
            };
        }
    }
}