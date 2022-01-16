using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Domain
{
    public class AuthenticationResult
    {
        public string AccessToken { get; set; }
        public string Username { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
