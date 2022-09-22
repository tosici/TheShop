using Shop.WebApi.Interfaces;
using Shop.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTests
{
    internal class ShopServiceStub : IShopService
    {
        public Article BuyArticle(Article article, int buyerId)
        {
            throw new NotImplementedException();
        }

        public Article? FindArticleDealersById(int id)
        {
            throw new NotImplementedException();
        }

        public Article? FindArticleDealersMaxPrice(int id, int maxPrice)
        {
            throw new NotImplementedException();
        }

        public Article? GetArticle(int id, int maxPrice)
        {
            if (id == 0)
            {
                return null;
            }
            return new Article { ID=3, Name_of_article="Test", ArticlePrice = 300};
        }

        public Article? GetArticleById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Article> GetArticlesLocal()
        {
            throw new NotImplementedException();
        }
    }
}
