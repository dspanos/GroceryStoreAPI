using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GroceryStoreAPI.Interfaces;
using GroceryStoreAPI.Models;
using GroceryStoreAPI.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GroceryStoreAPI.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly GenericRepository<Product> repo = new GenericRepository<Product>();

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var products = repo.GetData(JSONData.products);
            return products.ToList();
        }

        // GET: Products/5
        [HttpGet("{id}", Name = "GetProduct")]
        public ActionResult<Product> Get(int id)
        {
            var product = repo.GetData(JSONData.products).FirstOrDefault(p => p.Id == id);
            return product;
        }


        // POST: Products/
        [HttpPost]
        public ActionResult Post([FromBody] Product product)
        {
            if (product != null)
            {
                repo.Insert(product);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: Products/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Product product)
        {
            var existingProduct = repo.GetData(JSONData.products).Where(p => p.Id == id)
                .FirstOrDefault<Product>();

            if (existingProduct != null)
            {
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;

                repo.Update(existingProduct);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE: Products/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existingProduct = repo.GetData(JSONData.products).Where(c => c.Id == id)
                .FirstOrDefault<Product>();

            if (existingProduct != null)
            {
                repo.Delete(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
