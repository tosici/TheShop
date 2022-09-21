using Newtonsoft.Json;
using Shop.WebApi.Models;

namespace Shop.WebApi.Interfaces
{
    public interface IDealer
    {
        bool ArticleInInventory(int id);
        Article GetArticle(int id);
        Article BuyArticle(Article article);
    }
}
