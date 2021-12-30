using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ErrorModel
    {
        public string ErrorTitle { get; set; }
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
