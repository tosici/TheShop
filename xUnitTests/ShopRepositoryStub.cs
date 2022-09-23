using Shop.WebApi.Interfaces;
using Shop.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTests
{
    internal class ShopRepositoryStub : IShopRepository
    {
        public Article? GetArticleById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Article> GetArticles()
        {
            throw new NotImplementedException();
        }

        public Article? GetByIdMaxPrice(int id, int maxPrice)
        {
            return new Article { Id = 12, Name = "Test" };

        }

        public Article Save(Article article)
        {
            throw new NotImplementedException();
        }
    }
}
