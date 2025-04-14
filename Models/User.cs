using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User
    {
        public int IdUser { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Hash { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
    }
}
