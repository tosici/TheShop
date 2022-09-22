using Newtonsoft.Json;
using Shop.WebApi.Interfaces;
using Shop.WebApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;

namespace Shop.WebApi.Services
{
    public class Dealer2 :IDealerService
    {
        private readonly string _supplierUrl;

        public Dealer2(string url)
        {
            _supplierUrl = url;
        }

        public async Task<bool> ArticleInInventory(int id)
        {
            using (var client = new HttpClient())
            {
                var response = client.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"{_supplierUrl}/Supplier/ArticleInInventory/{id}"));
                var msg = await response;
                switch (msg.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return JsonConvert.DeserializeObject<bool>(response.Result.Content.ReadAsStringAsync().Result);
                    case HttpStatusCode.BadRequest:
                        var reason = msg.ReasonPhrase;
                        try
                        {
                            var obj = await msg.Content.ReadAsStringAsync();
                            if (!string.IsNullOrWhiteSpace(obj))
                            {
                                reason = obj;
                            }
                        }
                        catch { /*ignored if message cant be readed*/}
                        throw new ValidationException(reason);

                    case HttpStatusCode.Unauthorized:
                        throw new UnauthorizedAccessException();
                }
                throw new Exception(response.Result.StatusCode.ToString());
            }
        }

        public async Task<Article> BuyArticle(Article article)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(article), Encoding.UTF8, "application/json");
                var response = client.PostAsync($"{_supplierUrl}/Supplier/{article?.BuyerUserId}",content);
                var msg = await response;
                switch (msg.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return JsonConvert.DeserializeObject<Article>(response.Result.Content.ReadAsStringAsync().Result);
                    case HttpStatusCode.BadRequest:
                        var reason = msg.ReasonPhrase;
                        try
                        {
                            var obj = await msg.Content.ReadAsStringAsync();
                            if (!string.IsNullOrWhiteSpace(obj))
                            {
                                reason = obj;
                            }
                        }
                        catch { /*ignored if message cant be readed*/}
                        throw new ValidationException(reason);

                    case HttpStatusCode.Unauthorized:
                        throw new UnauthorizedAccessException();
                }
                throw new Exception(response.Result.StatusCode.ToString());

            }
        }

        public async Task<Article> GetArticle(int id)
        {
            using (var client = new HttpClient())
            {
                var response = client.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"{_supplierUrl}/Supplier/GetArticle/{id}"));
                var msg = await response;
                switch (msg.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return JsonConvert.DeserializeObject<Article>(response.Result.Content.ReadAsStringAsync().Result);
                    case HttpStatusCode.BadRequest:
                        var reason = msg.ReasonPhrase;
                        try
                        {
                            var obj = await msg.Content.ReadAsStringAsync();
                            if (!string.IsNullOrWhiteSpace(obj))
                            {
                                reason = obj;
                            }
                        }
                        catch { /*ignored if message cant be readed*/}
                        throw new ValidationException(reason);

                    case HttpStatusCode.Unauthorized:
                        throw new UnauthorizedAccessException();
                }
                throw new Exception(response.Result.StatusCode.ToString());
            }
        }
    }
}