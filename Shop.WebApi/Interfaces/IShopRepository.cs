using Shop.WebApi.Models;

namespace Shop.WebApi.Interfaces
{
    public interface IShopRepository
    {
        Article? GetArticleById(int id);
        Article? GetByIdMaxPrice(int id, int maxPrice);
        List<Article> GetArticles();
        Article Save(Article article);
    }
}
