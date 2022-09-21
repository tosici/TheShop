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
            for (int i = 0; i < 10; i++)
            {
                _articles.Add(new Article
                {
                    ID = i,
                    Name_of_article = $"Article {i}",
                    ArticlePrice = new Random().Next(100, 500)
                });
            }
        }

        public Article? GetById(int id)
        {
            return _articles.Find(x => x.ID == id);
        }

        public Article? GetByIdMaxPrice(int id, int maxPrice)
        {
            return _articles.Find(x => x.ID == id && x.IsSold != true && x.ArticlePrice <= maxPrice);
        }
        public int Insert(Article article)
        {
            article.ID = _articles.Max(art => art.ID) + 1;
            _articles.Add(article);
            return article.ID;
        }
        public void Update(Article article)
        {
            int index = _articles.FindIndex(art => art.ID == article.ID);
            _articles[index] = article;
            
        }
    }
}