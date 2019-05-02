using OrderManagment.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Hosting;

namespace OrderManagment.Core.ApiBusiness
{
    public class CategoryApiCall
    {
        private string BaseURL = string.Empty;

        public CategoryApiCall()
        {
            BaseURL = ApiBusiness.ApiCallConfig.GetApiConfig;
        }
        public IEnumerable<Category> GetAllCategories()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("category/getall").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<IEnumerable<Category>>().Result;
                }
                else
                {
                    return null;
                }
            }
        }
        public bool Insert(Category Category)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsJsonAsync("category/createcategory", Category).Result;

                return response.IsSuccessStatusCode;
            }
        }
        public bool Update(Category Category)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsJsonAsync("category/updatecategory", Category).Result;

                return response.IsSuccessStatusCode;
            }
        }
        public Category GetCategoryById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("category/GetCategoryById/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<Category>().Result;
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

                var response = client.DeleteAsync("category/delete/" + id).Result;

            }
        }
    }
}