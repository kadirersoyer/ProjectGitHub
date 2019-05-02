using OrderManagment.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace OrderManagment.Core.ApiBusiness
{
    public class OrderApiCall
    {
        private string BaseURL = string.Empty;

        public OrderApiCall()
        {
            BaseURL = ApiBusiness.ApiCallConfig.GetApiConfig;
        }
        public IEnumerable<Order> GetAllOrders()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("order/getall").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<IEnumerable<Order>>().Result;
                }
                else
                {
                    return null;
                }
            }
        }
        public bool Insert(Order order)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsJsonAsync("order/createorder", order).Result;

                return response.IsSuccessStatusCode;
            }
        }
        public bool Update(Order Order)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsJsonAsync("order/updateorder", Order).Result;

                return response.IsSuccessStatusCode;
            }
        }
        public Order GetOrderById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("order/GetOrderId/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<Order>().Result;
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

                var response = client.DeleteAsync("order/delete/" + id).Result;
                
            }
        }
    }
}