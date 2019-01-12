using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiModels;

namespace CoreApiClient
{
    public class ApiClient:IApiClient
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;

        public ApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }

        public async Task<IEnumerable<ToDoViewModel>> GetApiObjects<ToDoViewModel>()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
               "Todos"));

            return await this.GetAsync<List<ToDoViewModel>>(requestUrl);
        }

        public async Task PostApiObject<ToDoViewModel>(ToDoViewModel model)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
               "Todos"));

            await PostAsync<ToDoViewModel>(requestUrl, model);
        }

        public async Task EditApiObject<ToDoViewModel>(int id, ToDoViewModel model)
        {
            var requestUrl = CreateRequestUri($"Todos/{id}");

            await PutAsync<ToDoViewModel>(requestUrl, model);
        }

        public async Task DeleteApiObject(int id)
        {
            var requestUrl = CreateRequestUri($"Todos/{id}");

            await DeleteAsync(requestUrl);
        }

        private Uri CreateRequestUri(string relativePath, string queryString = "")
        {
            var getWebApiUrlAsString = this.configuration.GetValue<string>("WebApi:WebApiBaseUrl");

            var apiEndpointfUri = new Uri(getWebApiUrlAsString);

            var endpoint = new Uri(apiEndpointfUri, relativePath);

            var uriBuilder = new UriBuilder(endpoint);

            return uriBuilder.Uri;
        }

        private async Task<T> GetAsync<T>(Uri requestUrl)
        {
            var client = this.httpClientFactory.CreateClient();

            var response = await client.GetAsync(requestUrl);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                throw new HttpRequestException(e.Message);
            }

            var data = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(data);
        }

        private async Task PostAsync<T>(Uri requestUrl, T content)
        {
            var jsonContent = this.ConvertToJson(content);

            var client = this.httpClientFactory.CreateClient();

            var result = await client.PostAsync(requestUrl, jsonContent);

            try
            {
                result.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                throw new HttpRequestException(e.Message);
            }
        }

        private async Task PutAsync<T>(Uri requestUrl, T model)
        {
            var jsonContent = this.ConvertToJson(model);

            var client = this.httpClientFactory.CreateClient();

            var result = await client.PutAsync(requestUrl, jsonContent);

            try
            {
                result.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                throw new HttpRequestException(e.Message);
            }
        }

        private async Task DeleteAsync(Uri requestUrl)
        {
            var client = this.httpClientFactory.CreateClient();

            var result = await client.DeleteAsync(requestUrl);

            try
            {
                result.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                throw new HttpRequestException(e.Message);
            }
        }

        private StringContent ConvertToJson<T>(T content)
        {
            return new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
        }
    }
}
