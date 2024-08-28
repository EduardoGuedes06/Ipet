namespace Ipet.MVC.Interfaces
{
    public interface IHttpService
    {
        Task<T> SendPostAsync<T>(string uri, object body);
        Task SendPostAsync(string uri, object body);
        Task<T> SendPutAsync<T>(string uri, object body);
        Task SendPutAsync (string uri, object body);
        Task<T> SendGetAsync<T>(string uri);
        Task SendGetAsync(string uri);
        Task<T> SendDeleteAsync<T>(string uri);
        Task SendDeleteAsync(string uri);
    }
}