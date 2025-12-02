namespace WebAppEmpMVC.HttpClients
{
    public class AddressApiHttpClient : GenericHttpClient
    {
        public AddressApiHttpClient(IConfiguration config, IHttpContextAccessor accessor)
            : base(new HttpClient(), config, accessor)
        {
            _client.BaseAddress = new Uri(config["ApiClientConfiguration:AddressApiUrl"]);
        }
    }
}
