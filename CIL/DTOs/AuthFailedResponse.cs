using System;
using System.Collections.Generic;
using System.Text;

namespace CIL.DTOs
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
