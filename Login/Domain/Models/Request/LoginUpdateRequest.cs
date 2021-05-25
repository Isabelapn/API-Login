using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Domain.Models.Request
{
    public class LoginUpdateRequest : LoginRequest
    {
        public int IdUsers { get; set; }
    }
}
