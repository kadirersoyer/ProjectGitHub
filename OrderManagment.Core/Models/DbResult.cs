using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderManagment.Core.Models
{
    public class DbResult
    {
        public string message { get; set; }
        public bool status { get; set; }
        public object data { get; set; }
    }
}