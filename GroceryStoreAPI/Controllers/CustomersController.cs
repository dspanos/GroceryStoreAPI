using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
    public class CustomersController : ControllerBase
    {
        readonly GenericRepository<Customer> repo = new GenericRepository<Customer>();
        readonly GenericRepository<Order> ordersRepo = new GenericRepository<Order>();

        // GET: Customers/
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            var customers = repo.GetData(JSONData.customers);
            return customers.ToList();
        }

        // GET: Customers/5/orders
        [HttpGet("{customerId}/orders", Name = "GetCustomerOrders")]
        public ActionResult<IEnumerable<Order>> GetCustomerOrders(int customerId)
        {
            var orders = ordersRepo.GetData(JSONData.orders);
            return orders.Where(o => o.CustomerId == customerId).ToList();
        }

        // GET: Customers/5
        [HttpGet("{id}", Name = "GetCustomer")]
        public ActionResult<Customer> Get(int id)
        {
            var customer = repo.GetData(JSONData.customers).FirstOrDefault(c => c.Id == id);
            return customer;
        }

        // POST: Customers/ (create new customer)
        [HttpPost]
        public ActionResult Post([FromBody] Customer customer)
        {
            if (customer != null)
            {
                repo.Insert(customer);
                return Ok();
            }
            else
            {
                return NotFound();
            }
            
        }

        // PUT: Customers/5 (Edit customer)
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Customer customer)
        {
            var existingCustomer = repo.GetData(JSONData.customers).Where(c => c.Id == id)
                .FirstOrDefault<Customer>();

            if (existingCustomer != null)
            {
                existingCustomer.Name = customer.Name;

                repo.Update(existingCustomer);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE: Customers/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existingCustomer = repo.GetData(JSONData.customers).Where(c => c.Id == id)
                .FirstOrDefault<Customer>();

            if (existingCustomer != null)
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
