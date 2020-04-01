using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderDetails.Models
{
    public class OrderDetail
    {
        public User UserDetails { get; set; }
        public List<Order> Orders { get; set; }
    }
}
