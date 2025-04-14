using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ResponseBase<T>
    {
        public bool Error {  get; set; }
        public string Message { get; set; } = string.Empty;
        public T Response { get; set; }
    }
}
