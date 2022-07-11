using Flock.Models;
using Flock.Services;
using Flock.Dtos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flock.Responses;

namespace Flock.Requests
{
    internal class LoginRequest
    {
        public IAuthentication _auth { get; set; }

        public IConfiguration _config;

        public LoginRequest(IAuthentication Authentication, IConfiguration Configuration)
        {
            _auth = Authentication;
            _config = Configuration;
        }

        public async Task<bool> GetUserToken()
        {
            var user = _auth.AskForCredentials();
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.BaseAddress = new Uri(_config.GetValue<string>("BaseUrl"));
            var response = await client.PostAsync("api/login", data);

            var responseBody = await response.Content.ReadAsStringAsync();

            if(response.IsSuccessStatusCode)
            {
                var credentials = JsonConvert.DeserializeObject<UserDto>(responseBody);
                var success = _auth.PersistCredentials(credentials);
                if (success)
                {
                    Console.WriteLine("You are now loged in.");
                    return true;
                }
            }
            Console.WriteLine("Something went wrong with the login.");

            return false;
        }
    }
}
