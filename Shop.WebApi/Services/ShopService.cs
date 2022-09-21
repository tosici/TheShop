using Microsoft.Extensions.Caching.Memory;
using Shop.WebApi.DatabaseLayer;
using Shop.WebApi.Interfaces;
using Shop.WebApi.Models;

namespace Shop.WebApi.Services
{
    public class ShopService : IShopService
    {
        private readonly IShopRepository _shopRepository;
        private IMemoryCache _cache;
        public ShopService(IShopRepository shopRepository, IMemoryCache cache)
        {
            _shopRepository = shopRepository;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }


        public Article? GetArticle(int id, int maxPrice)
        {
            if (_cache.TryGetValue($"Article_{id}", out Article? article) && article?.ArticlePrice < maxPrice)
            {
                return article;
            }
            article = _shopRepository.GetByIdMaxPrice(id, maxPrice);
            if (article == null)
            {
                article = FindArticleDealersMaxPrice(id, maxPrice);
            }
            if (article != null)
            {
                _cache.Set($"Article_{id}", article);
            }
            return article;
        }


        public Article? FindArticleDealersMaxPrice(int id, int maxPrice)
        {
            throw new NotImplementedException();
        }

        public Article BuyArticle(Article article, int buyerId)
        {
            article.IsSold = true;
            article.SoldDate = DateTime.Now;
            article.BuyerUserId = buyerId;
            if (article.DealerId > 0)
            {
                return DealerFactory.GetDealer(article.DealerId).BuyArticle(article);
            }
            else
            {
                return _shopRepository.Save(article);
            }
        }
    }
}
