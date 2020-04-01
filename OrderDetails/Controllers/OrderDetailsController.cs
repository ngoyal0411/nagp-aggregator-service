using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderDetails.Models;

namespace OrderDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private string userServiceUrl = string.Empty;
        private string orderServiceUrl = string.Empty;

        public OrderDetailsController()
        {
            userServiceUrl = Environment.GetEnvironmentVariable("USERSERVICE_URL") ?? "http://localhost:4231";
            orderServiceUrl= Environment.GetEnvironmentVariable("ORDERSERVICE_URL") ?? "http://localhost:4170";
        }
        // GET: api/OrderDetails
        [HttpGet]
        public OrderDetail Get()
        {
            return AggregatorResponse();
        }

        // GET: api/OrderDetails/5
        [HttpGet("{id}", Name = "Get")]
        public OrderDetail Get(int id)
        {
            return AggregatorResponse();
        }
        private User GetUser()
        {
            User userResponse = new User();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(userServiceUrl);
                //HTTP GET
                var responseTask = client.GetAsync("/api/user");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<User>();
                    readTask.Wait();
                    userResponse = readTask.Result;
                }
            }
            return userResponse;
        }
        private List<Order> GetOrders()
        {
            OrderData ordersResponse = new OrderData();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(orderServiceUrl);
                //HTTP GET
                var responseTask = client.GetAsync("/api/orders");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<OrderData>();
                    readTask.Wait();
                    ordersResponse = readTask.Result;
                }
            }
            return ordersResponse.Orders;
        }
        public OrderDetail AggregatorResponse()
        {
            OrderDetail orderDetail = new OrderDetail();
            orderDetail.UserDetails = GetUser();
            orderDetail.Orders = GetOrders();
            return orderDetail;
        }


    }
}
