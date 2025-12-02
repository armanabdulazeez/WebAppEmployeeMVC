namespace WebAppEmpMVC.HttpClients
{
    public interface IGenericHttpClient
    {
        Task<TResponse> GetAsAsync<TResponse>(string address);
        Task<TResponse> PostAsAsync<TResponse>(string address, dynamic request);
        Task<TResponse> PutAsAsync<TResponse>(string address, dynamic request, TResponse defaultResponse);
        Task<TResponse> DeleteAsAsync<TResponse>(string address, TResponse defaultResponse);
    }
}
