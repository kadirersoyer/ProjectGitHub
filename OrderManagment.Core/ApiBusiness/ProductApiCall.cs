using OrderManagment.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace OrderManagment.Core.ApiBusiness
{
    public class ProductApiCall
    {
        private string BaseURL = string.Empty;

        public ProductApiCall()
        {
            BaseURL = ApiBusiness.ApiCallConfig.GetApiConfig;
        }
        public bool Insert(Product product)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsJsonAsync("product/createproduct", product).Result;

                return response.IsSuccessStatusCode;
            }
        }
        public bool Update(Product product)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsJsonAsync("product/updateproduct", product).Result;

                return response.IsSuccessStatusCode;
            }
        }
        public Product GetProductById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("product/GetProductById/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<Product>().Result;
                }
                else
                {
                    return null;
                }
            }
        }
        public IEnumerable<Product> GetProducts ()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("product/getall/").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
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

                var response = client.DeleteAsync("product/delete/" + id).Result;

            }
        }

    }
}