using Eshopam.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Eshopam.Services
{
    public class UserService
    {
        private readonly HttpClient client;
        public UserService(string baseAddress)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
        }

        public async Task<UserModel> GetAsync(int id)
        {
            string url = $"/Users/{id}";
            var response = await client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<UserModel>(data);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new KeyNotFoundException("User not found !");
            }
            else
            {
                throw new Exception($"Error status code : {response.StatusCode} \n {data}");
            }
        }

        public async Task<UserModel> LoginAsync(string username, string password)
        {
            string url = $"Users?username={username}&password={password}";
            var response = await client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<UserModel>(data);
            }
            else if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new UnauthorizedAccessException("User name or password is incorrect !");
            }
            else
            {
                throw new Exception($"Error status code : {response.StatusCode} \n {data}");
            }
        }

        public async Task<UserModel> CreateAsync(UserModel user)
        {
            string url = $"Users";
            StringContent content = new StringContent
            (
                JsonConvert.SerializeObject(user),
                System.Text.Encoding.UTF8,
                "application/json"
            );
            var response = await client.PostAsync(url, content);
            var data = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<UserModel>(data);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                throw new DuplicateWaitObjectException($"User name {user.Username} already exists !");
            }
            else
            {
                throw new Exception($"Error status code : {response.StatusCode} \n {data}");
            }
        }

        public async Task<UserModel> UpdateAsync(UserModel user)
        {
            string url = $"Users";
            StringContent content = new StringContent
            (
                JsonConvert.SerializeObject(user),
                System.Text.Encoding.UTF8,
                "application/json"
            );
            var response = await client.PutAsync(url, content);
            var data = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<UserModel>(data);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new KeyNotFoundException($"User id {user.Id} not found !");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                throw new DuplicateWaitObjectException($"User name {user.Username} already exists !");
            }
            else
            {
                throw new Exception($"Error status code : {response.StatusCode} \n {data}");
            }
        }


    }
}
