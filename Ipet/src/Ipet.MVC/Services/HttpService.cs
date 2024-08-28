using Ipet.MVC.Interfaces;

namespace Ipet.MVC.Services
{
    public class HttpService : HttpExtension, IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("http://192.168.15.101:7042/");
            _httpClient = httpClient;
        }
        public async Task<T> SendPostAsync<T>(string uri, object body)
        {
            var loginContent = body != null ? ObterConteudo(body) : null;

            var response = await _httpClient.PostAsync(uri, loginContent);

            if (!TratarErrosResponse(response))
            {
                throw new Exception(response.RequestMessage.ToString());
            }

            return await DeserializarObjetoResponse<T>(response);
        }

        public async Task SendPostAsync (string uri, object body)
        {
            var loginContent = body != null ? ObterConteudo(body) : null;

            var response = await _httpClient.PostAsync(uri, loginContent);

            if (!TratarErrosResponse(response))
            {
                throw new Exception(response.RequestMessage.ToString());
            }
        }

        public async Task<T> SendGetAsync<T>(string uri)
        {

            var response = await _httpClient.GetAsync(uri);

            if (!TratarErrosResponse(response))
            {
                throw new Exception(response.RequestMessage.ToString());
            }

            return await DeserializarObjetoResponse<T>(response);
        }

        public async Task SendGetAsync(string uri)
        {

            var response = await _httpClient.GetAsync(uri);

            if (!TratarErrosResponse(response))
            {
                throw new Exception(response.RequestMessage.ToString());
            }
        }

        public async Task<T> SendPutAsync<T>(string uri, object body)
        {
            var loginContent = body != null ? ObterConteudo(body) : null;

            var response = await _httpClient.PutAsync(uri, loginContent);

            if (!TratarErrosResponse(response))
            {
                throw new Exception(response.RequestMessage.ToString());
            }

            return await DeserializarObjetoResponse<T>(response);
        }

        public async Task SendPutAsync(string uri, object body)
        {
            var loginContent = body != null ? ObterConteudo(body) : null;

            var response = await _httpClient.PutAsync(uri, loginContent);

            if (!TratarErrosResponse(response))
            {
                throw new Exception(response.RequestMessage.ToString());
            }
        }

        public async Task<T> SendDeleteAsync<T>(string uri)
        {

            var response = await _httpClient.DeleteAsync(uri);

            if (!TratarErrosResponse(response))
            {
                throw new Exception(response.RequestMessage.ToString());
            }

            return await DeserializarObjetoResponse<T>(response);
        }

        public async Task SendDeleteAsync (string uri)
        {

            var response = await _httpClient.DeleteAsync(uri);

            if (!TratarErrosResponse(response))
            {
                throw new Exception(response.RequestMessage.ToString());
            }
        }
    }
}
