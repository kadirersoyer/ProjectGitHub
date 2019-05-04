using OrderManagment.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace OrderManagment.Core.Repositories
{
    public class ProductViewRepository : IProductViewRepository
    {
        string APIURL = string.Empty;
        Helpers.ConfigHelper configHelper;

        public ProductViewRepository()
        {
            configHelper = new Helpers.ConfigHelper();
            APIURL = configHelper.GetApiUrl;
        }

        public IEnumerable<ProductDetailList> GetProductDetailLists(string URL)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIURL);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync(APIURL + URL).Result;

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<IEnumerable<ProductDetailList>>().Result;
                }
                else
                {
                    return null;
                }
            }
        }
    }

}