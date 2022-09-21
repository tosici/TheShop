using Shop.WebApi.Models;

namespace Shop.WebApi.Interfaces
{
    public interface IShopRepository
    {
        Article? GetArticleById(int id);
        Article? GetByIdMaxPrice(int id, int maxPrice);
        Article Save(Article article);
    }
}
