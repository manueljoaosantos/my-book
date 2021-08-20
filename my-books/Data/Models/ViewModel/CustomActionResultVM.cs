using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Data.Models.ViewModel
{
    public class CustomActionResultVM
    {
        public Exception Exception { get; set; }
        //public Object Data { get; set; }
        public Publisher Publisher { get; set; }
    }
}
