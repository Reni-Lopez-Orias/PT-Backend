using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ValidateLoginUserRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Hash { get; set; } = string.Empty;
    }
}
