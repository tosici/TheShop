using Newtonsoft.Json;
using Shop.WebApi.Models;

namespace Shop.WebApi.Interfaces
{
    public interface IDealerService
    {
        Task<bool> ArticleInInventory(int id);
        Task<Article> GetArticle(int id);
        Task<Article> BuyArticle(Article article);
    }
}
