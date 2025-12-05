using Newtonsoft.Json;

namespace WebAppEmpMVC.HttpClients
{
    public class GenericHttpClient : IGenericHttpClient
    {
        public readonly HttpClient _client;
        private readonly IHttpContextAccessor _contextAccessor;

        public GenericHttpClient(HttpClient client, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _client = client;
            _contextAccessor = contextAccessor;

            _client.BaseAddress = new Uri(configuration["ApiClientConfiguration:EmployeeApiUrl"]);
            _client.Timeout = TimeSpan.FromMinutes(5);
            
        }

        public async Task<TResponse> GetAsAsync<TResponse>(string address)
        {
            AddToken();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, address);
            HttpResponseMessage response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                TResponse userResponse = JsonConvert.DeserializeObject<TResponse>(result);
                return userResponse;
            }
            var errorResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(errorResult);
        }

        public async Task<TResponse> PostAsAsync<TResponse>(string Address, dynamic dynamicRequest)
        {
            AddToken();
            var request = new HttpRequestMessage(HttpMethod.Post, Address);
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
            request.Content = new StringContent(JsonConvert.SerializeObject(dynamicRequest));
            request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                TResponse userResponse = JsonConvert.DeserializeObject<TResponse>(result);
                return userResponse;
            }
            string errorResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(errorResult);
        }

        public async Task<TResponse> PutAsAsync<TResponse>(string Address, dynamic dynamicRequest, TResponse isUpdated)
        {
            AddToken();
            var request = new HttpRequestMessage(HttpMethod.Put, Address);
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
            request.Content = new StringContent(JsonConvert.SerializeObject(dynamicRequest));
            request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                isUpdated = JsonConvert.DeserializeObject<TResponse>(result);
            }
            return isUpdated;
        }

        public async Task<bool> DeleteAsAsync(string address)
        {
            AddToken();

            var response = await _client.DeleteAsync(address);

            return response.IsSuccessStatusCode;
        }



        private void AddToken()
        {
            var token = _contextAccessor.HttpContext?.Session.GetString("token"); //JWTTOKEN

            if (!string.IsNullOrEmpty(token))
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            else
                _client.DefaultRequestHeaders.Authorization = null;
        }
    }
}
