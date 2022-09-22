using Shop.WebApi.Models;

namespace Shop.WebApi.Interfaces
{
    public interface IShopService
    {
        Article? GetArticle(int id, int maxPrice);
        Article? GetArticleById(int id);
        Article? FindArticleDealersMaxPrice(int id, int maxPrice);
        Article? FindArticleDealersById(int id);
        Article BuyArticle(Article article, int buyerId);
        List<Article> GetArticlesLocal();
    }
}
