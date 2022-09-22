using Shop.WebApi.Models;

namespace Shop.WebApi.Interfaces
{
    public interface IDb
    {
        Article? GetById(int id);
        Article? GetByIdMaxPrice(int id, int maxPrice);
        List<Article> GetArticles();
        int Insert(Article article);
        void Update(Article article);
    }
}
