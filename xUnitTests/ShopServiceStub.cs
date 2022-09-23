using Microsoft.AspNetCore.Mvc;
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
            switch (id)
            {
                case 0:
                    return null;
                case 1:
                    throw new HttpRequestException("Bad Request", null, System.Net.HttpStatusCode.BadRequest);
            }
            return new Article { Id = 3, Name = "Test", Price = 300 };

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
