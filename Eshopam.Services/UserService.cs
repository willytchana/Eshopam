using Eshopam.Models;
using Newtonsoft.Json;
using System;
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

        public async Task<UserModel> Login(string username, string password)
        {
            //http://localhost:8180/api
            string url = $"/Users/username={username}&password={password}";
            var response = await client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<UserModel>(data);
            }
            else if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new UnauthorizedAccessException("Username or password is incorrect !");
            }
            else
            {
                throw new Exception($"Error status code : {response.StatusCode} \n {data}");
            }
        }
    }
}
