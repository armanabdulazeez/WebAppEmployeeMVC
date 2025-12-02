namespace WebAppEmpMVC.HttpClients
{
    public class EmployeeApiHttpClient : GenericHttpClient
    {
        public EmployeeApiHttpClient(IConfiguration config, IHttpContextAccessor accessor)
            : base(new HttpClient(), config, accessor)
        {
            _client.BaseAddress = new Uri(config["ApiClientConfiguration:EmployeeApiUrl"]);
        }
    }
}   
