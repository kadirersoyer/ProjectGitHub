using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace OrderManagment.Core.Helpers
{
    public class ConfigHelper
    {
        public string GetApiUrl
        {
            get
            {
                return WebConfigurationManager.AppSettings["WebApiURL"];
            }
        }
    }
}