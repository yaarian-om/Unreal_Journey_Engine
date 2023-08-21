using AutoMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class WeatherService
    {

        public static async Task<string> GetWeatherAsync(string location)
        {
            var _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://api.openweathermap.org");

            string _apiKey = "bdf8311cf479f4dc1b80dce66a71df0d";

            string endpoint = $"/data/2.5/weather?q={location}&appid={_apiKey}";

            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }

    }

}
