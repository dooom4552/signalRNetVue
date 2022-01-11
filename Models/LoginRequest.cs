using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJWS.Models
{
    public class LoginRequest
    {
        public string password { get; set; }
        public string username { get; set; }
       
    }
}