using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    public class OrdersController : ControllerBase
    {
        readonly GenericRepository<Order> repo = new GenericRepository<Order>();

        // GET: Orders/
        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        {
            var orders = repo.GetData(JSONData.orders);
            return orders.ToList();
        }

        // GET: Orders/OrdersByDate?orderDate=11-24-2019
        [HttpGet("{action}", Name = "GetOrdersByDate")]
        public ActionResult<IEnumerable<Order>> OrdersByDate([FromQuery] DateTime orderDate)
        {
            var orders = repo.GetData(JSONData.orders);
            var filteredOrders = orders.Where(d => d.OrderDate.Date.ToShortDateString() == orderDate.Date.ToShortDateString()).ToList();

            return filteredOrders;
        }

        // GET: Orders/5
        [HttpGet("{id}", Name = "GetOrder")]
        public ActionResult<Order> Get(int id)
        {
            var order = repo.GetData(JSONData.orders).FirstOrDefault(o => o.Id == id);
            return order;
        }
    }
}
