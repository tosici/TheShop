using System.Collections.Generic;
using System.Linq;
using Shop.WebApi.Interfaces;
using Shop.WebApi.Models;

namespace Shop.WebApi.DatabaseLayer
{
    public class Db : IDb
    {
        private List<Article> _articles = new List<Article>();

        public Db()
        {
            //mock for testing
            for (int i = 1; i < 10; i++)
            {
                _articles.Add(new Article
                {
                    Id = i,
                    Name = $"Article {i}",
                    Price = new Random().Next(100, 500)
                });
            }
        }

        public Article? GetById(int id)
        {
            return _articles.Find(x => x.Id == id);
        }

        public Article? GetByIdMaxPrice(int id, int maxPrice)
        {
            return _articles.Find(x => x.Id == id && x.IsSold != true && x.Price <= maxPrice);
        }

        public List<Article> GetArticles()
        {
            return _articles;
        }

        public int Insert(Article article)
        {
            article.Id = _articles.Max(art => art.Id) + 1;
            _articles.Add(article);
            return article.Id;
        }
        public void Update(Article article)
        {
            int index = _articles.FindIndex(art => art.Id == article.Id);
            _articles[index] = article;
            
        }
    }
}