using Shop.WebApi.Interfaces;
using Shop.WebApi.Models;

namespace Shop.WebApi.Repository
{
    public class ShopRepository : IShopRepository
    {
        private readonly IDb _db;

        public ShopRepository(IDb db)
        {
            _db = db;
        }

        public Article? GetArticleById(int id)
        {
            return _db.GetById(id);
        }

        public Article? GetByIdMaxPrice(int id, int maxPrice)
        {
            return _db.GetByIdMaxPrice(id, maxPrice);
        }

        public Article Save(Article article)
        {
            if (article.ID == 0)
            {
                article.ID = _db.Insert(article);
            }
            else
            {
                _db.Update(article);
            }
            return article;
        }
    }
}
