using Data.Data;
using Data.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.Net_web_api_p1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopDbContext context;
        public ProductsController(ShopDbContext context) { 
            this.context = context;
        }
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(context.Products.ToList());
        }
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var entity = context.Products.Find(id);
            if (entity == null) return NotFound();

            return Ok(entity);
        }

        [HttpPost]
        public IActionResult Create(Product model)
        {
            if (!ModelState.IsValid) return NotFound();
            context.Products.Add(model);
            context.SaveChanges();

            return Ok();
        }
        [HttpPut]
        public IActionResult Edit(Product model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var entity = context.Products.AsNoTracking().FirstAsync(x => x.Id == model.Id);
            if(entity != null) return NotFound();

            context.Products.Update(model);
            context.SaveChanges();

            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var entity = context.Products.Find(id);
            if (entity != null) return NotFound();

            context.Products.Remove(entity);
            context.SaveChanges();

            return Ok();
        }
    }
}
