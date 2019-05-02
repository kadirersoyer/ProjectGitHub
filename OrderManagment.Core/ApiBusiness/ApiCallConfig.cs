using System.Web.Hosting;

namespace OrderManagment.Core.ApiBusiness
{
    public class ApiCallConfig
    {
        public static string GetApiConfig
        {
            get
            {
                return
                 System.IO.File.ReadAllText(HostingEnvironment.MapPath(@"~/App_Data/apiconfig.txt"));
            }
        }
    }
}