using Newtonsoft.Json;
using Sportifly.Interface;
using Sportifly.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace Sportifly.Service
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {  
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5239/api/user-service/");
        }

       public async Task<List<UserModel>> GetUsersAsyc() 
        {
           var respons = await _httpClient.GetAsync("user");
            if (respons.IsSuccessStatusCode)
            {
                var json = await  respons.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<UserModel>>(json);
            }
            else
            {
                throw new Exception("Ошибка при получении данных сервера");
            }
        }
    }
}
