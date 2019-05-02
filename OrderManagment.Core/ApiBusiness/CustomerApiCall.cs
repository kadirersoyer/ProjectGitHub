using OrderManagment.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace OrderManagment.Core.ApiBusiness
{
    public class CustomerApiCall
    {
        private string BaseURL = string.Empty;

        public CustomerApiCall()
        {
            BaseURL = ApiBusiness.ApiCallConfig.GetApiConfig;
        }
        public IEnumerable<Customer> GetAllCustomers()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("customer/getall").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<IEnumerable<Customer>>().Result;
                }
                else
                {
                    return null;
                }
            }
        }
        public bool Insert(Customer Customer)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsJsonAsync("customer/createcustomer", Customer).Result;

                return response.IsSuccessStatusCode;
            }
        }
        public bool Update(Customer Customer)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsJsonAsync("customer/updatecustomer", Customer).Result;

                return response.IsSuccessStatusCode;
            }
        }
        public Customer GetCustomerById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("customer/GetCustomerById/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<Customer>().Result;
                }
                else
                {
                    return null;
                }
            }
        }
        public void Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.DeleteAsync("customer/delete/" + id).Result;

            }
        }
    }
}