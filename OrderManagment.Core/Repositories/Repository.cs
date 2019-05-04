using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Configuration;

namespace OrderManagment.Core.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        string APIURL = string.Empty;
        Helpers.ConfigHelper configHelper;

        public Repository()
        {
            configHelper = new Helpers.ConfigHelper();
            APIURL = configHelper.GetApiUrl;
        }

        public void Delete(int id, string URL)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIURL);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.DeleteAsync(URL + id).Result;
            }
        }

        public IEnumerable<T> GetAll(string URL)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIURL);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync(URL).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<IEnumerable<T>>().Result;
                }
                else
                {
                    return null;
                }
            }
        }


        public T GetById(int id, string URL)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIURL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(URL + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<T>().Result;
                }
                else
                {
                    return null;
                }
            }
        }

        public void Insert(T t, string URL)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIURL);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsJsonAsync(URL, t).Result;
            }
        }

        public void Update(T t, string URL)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIURL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsJsonAsync(URL,t).Result;

            }
        }
    }
}