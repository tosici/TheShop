using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.WebApi.Models
{
    public class Article
    {
        public Article()
        {
        }
        public int ID { get; set; } = -1;

        public string Name_of_article { get; set; } = string.Empty;

        public int ArticlePrice { get; set; }
        public bool IsSold { get; set; }

        public DateTime SoldDate { get; set; }
        public int BuyerUserId { get; set; }
        public string DealerId { get; set; } = string.Empty;
    }
}