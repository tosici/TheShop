using Shop.WebApi.Models;

namespace Shop.WebApi.Interfaces
{
    public interface IShopService
    {
        Article? GetArticle(int id, int maxPrice);
        Article? FindArticleDealersMaxPrice(int id, int maxPrice);
        Article BuyArticle(Article article, int buyerId);
    }
}
