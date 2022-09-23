using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Shop.WebApi.DatabaseLayer;
using Shop.WebApi.Interfaces;
using Shop.WebApi.Models;
using System.Runtime.Intrinsics.Arm;

namespace Shop.WebApi.Services
{
    public class ShopService : IShopService
    {
        private readonly IShopRepository _shopRepository;
        private readonly IOptions<List<Dealer>> _dealers;
        private IMemoryCache _cache;
        public ShopService(IShopRepository shopRepository, IMemoryCache cache, IOptions<List<Dealer>> dealers)
        {
            _shopRepository = shopRepository;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _dealers = dealers;
        }


        public Article? GetArticle(int id, int maxPrice)
        {
            if (_cache.TryGetValue($"Article_{id}", out Article? article) && article?.IsSold != true && article?.Price < maxPrice)
            {
                return article;
            }
            article = _shopRepository.GetByIdMaxPrice(id, maxPrice);
            article ??= FindArticleDealersMaxPrice(id, maxPrice);
            if (article != null)
            {
                _cache.Set($"Article_{id}", article, TimeSpan.FromHours(2));
            }
            return article;
        }

        public Article? GetArticleById(int id)
        {
            if (_cache.TryGetValue($"Article_{id}", out Article? article))
            {
                return article;
            }
            article = _shopRepository.GetArticleById(id);
            article ??= FindArticleDealersById(id);
            if (article != null)
            {
                _cache.Set($"Article_{id}", article, TimeSpan.FromHours(2));
            }
            return article;
        }


        public Article? FindArticleDealersMaxPrice(int id, int maxPrice)
        {
            Article? article;
            foreach (Dealer dealer in _dealers.Value)
            {
                IDealerService dealerService = DealerFactory.GetDealer(dealer);
                if (dealerService.ArticleInInventory(id).Result)
                {
                    article = dealerService.GetArticle(id).Result;
                    if (article.Price < maxPrice)
                    {
                        article.DealerId = dealer.DealerId;
                        _cache.Set($"Article_{id}", article, TimeSpan.FromHours(2));
                        return article;
                    }
                }
            }
            return null;
        }

        public Article? FindArticleDealersById(int id)
        {
            Article? article;
            foreach (Dealer dealer in _dealers.Value)
            {
                IDealerService dealerService = DealerFactory.GetDealer(dealer);
                if (dealerService.ArticleInInventory(id).Result)
                {
                    article = dealerService.GetArticle(id).Result;
                    if (article != null)
                    {
                        article.DealerId = dealer.DealerId;
                        _cache.Set($"Article_{id}", article, TimeSpan.FromHours(2));
                        return article;
                    }
                }
            }
            return null;
        }

        public List<Article> GetArticlesLocal()
        {
            return _shopRepository.GetArticles();
        }

        public Article BuyArticle(Article article, int buyerId)
        {
            article.IsSold = true;
            article.SoldDate = DateTime.Now;
            article.BuyerUserId = buyerId;
            if (!string.IsNullOrWhiteSpace(article.DealerId))
            {
                Article result = DealerFactory.GetDealer(_dealers.Value.Single(d => d.DealerId == article.DealerId)).BuyArticle(article).Result;
                _cache.Remove($"Article_{result.Id}");
                return result;
            }
            else
            {
                _shopRepository.Save(article);
                _cache.Remove($"Article_{article.Id}");
                return article;
            }
        }
    }
}
