using Eshopam.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Eshopam.Services
{
    public class ProductService
    {
        private readonly HttpClient client;
        public ProductService(string baseAddress)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
        }

        public async Task<IEnumerable<ProductModel>> GetAsync()
        {
            string url = $"/Products";
            var response = await client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductModel>>(data);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new KeyNotFoundException("Product not found !");
            }
            else
            {
                throw new Exception($"Error status code : {response.StatusCode} \n {data}");
            }
        }

        public async Task<ProductModel> GetAsync(int id)
        {
            string url = $"/Products/{id}";
            var response = await client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ProductModel>(data);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new KeyNotFoundException("Product not found !");
            }
            else
            {
                throw new Exception($"Error status code : {response.StatusCode} \n {data}");
            }
        }

        public async Task<ProductModel> CreateAsync(ProductModel product, byte[] photo)
        {
            string url = $"Products";
            StringContent content = new StringContent
            (
                JsonConvert.SerializeObject(product),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            MultipartFormDataContent multipart = new MultipartFormDataContent();
            multipart.Add(content, "model");

            if(photo != null)
            {
                StreamContent stream = new StreamContent(new MemoryStream(photo));
                multipart.Add(stream, "photo", "photo.jpg");
            }

            var response = await client.PostAsync(url, multipart);
            var data = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ProductModel>(data);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                throw new DuplicateWaitObjectException($"Product code {product.Code} already exists !");
            }
            else
            {
                throw new Exception($"Error status code : {response.StatusCode} \n {data}");
            }
        }

        public async Task<ProductModel> UpdateAsync(ProductModel product, byte[] photo)
        {
            string url = $"Products";
            StringContent content = new StringContent
            (
                JsonConvert.SerializeObject(product),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            MultipartFormDataContent multipart = new MultipartFormDataContent();
            multipart.Add(content, "model");

            if (photo != null)
            {
                StreamContent stream = new StreamContent(new MemoryStream(photo));
                multipart.Add(stream, "photo", "photo.jpg");
            }
            var response = await client.PutAsync(url, multipart);
            var data = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ProductModel>(data);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new KeyNotFoundException($"Product id {product.Id} not found !");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                throw new DuplicateWaitObjectException($"Product code {product.Code} already exists !");
            }
            else
            {
                throw new Exception($"Error status code : {response.StatusCode} \n {data}");
            }
        }



        public async Task<ProductModel> DeleteAsync(int id)
        {
            string url = $"Products/{id}";
            var response = await client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ProductModel>(data);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new KeyNotFoundException("Product not found !");
            }
            else
            {
                throw new Exception($"Error status code : {response.StatusCode} \n {data}");
            }
        }

    }
}
