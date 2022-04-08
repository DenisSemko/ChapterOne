using System;
using System.Collections.Generic;
using System.Text;

namespace CIL.DTOs
{
    public class PayModel
    {
        public int version { get; set; }
        public string action { get; set; }
        public string public_key { get; set; }
        public decimal amount { get; set; }
        public string currency { get; set; }
        public string description { get; set; }
        public string order_id { get; set; }

        public string info { get; set; }
        public string product_name { get; set; }
        public string customer_user_id { get; set; }


        public int sandbox { get; set; }
    }
}
