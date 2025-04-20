
using System.Net;
using System;
using System.Text.Json;
using System.Web;

namespace TestApiSep.Service
{
    public class CedulaService : ICedulaService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CedulaService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<string> BuscarCedulaAsync(string idCedula)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("ApiSep");

            var jsonData = new
            {
                maxResult = "100",
                nombre = "",
                paterno = "",
                materno = "",
                idCedula = idCedula
            };

            var jsonString = JsonSerializer.Serialize(jsonData);
            var encodedJson = HttpUtility.UrlEncode(jsonString);

            var url = $"{httpClient.BaseAddress}?json={encodedJson}";

            using HttpResponseMessage response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al consultar la cédula: {response.StatusCode}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }
    }
}
