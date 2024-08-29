using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CustomActionResult
    {
        public string message { get; set; }
        public bool success { get; set; }

    }

    public class CustomActionResult<T> : CustomActionResult
    {
        public T data { get; set; }

    }
}
