using System;

namespace Vendor.WebApi.Models
{
    public class Article
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }
        public bool IsSold { get; set; }

        public DateTime SoldDate { get; set; }
        public int BuyerUserId { get; set; }
    }
}