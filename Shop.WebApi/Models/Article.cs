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
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Price { get; set; }
        public bool IsSold { get; set; }

        public DateTime SoldDate { get; set; }
        public int BuyerUserId { get; set; }
        public string DealerId { get; set; } = string.Empty;
    }
}