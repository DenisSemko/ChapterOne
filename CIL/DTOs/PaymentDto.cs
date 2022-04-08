using System;
using System.Collections.Generic;
using System.Text;

namespace CIL.DTOs
{
    public class PaymentDto
    {
        public string Data { get; set; }
        public string Signature { get; set; }
        public int Result { get; set; }
    }
}
