﻿using System;
using Vendor.WebApi.Models;

namespace Vendor.WebApi.Services
{
    public class SupplierService
    {
        public bool ArticleInInventory(int id)
        {
            return new Random().NextDouble() >= 0.5;
        }

        public Article GetArticle(int id)
        {
            return new Article()
            {
                Id = id,
                Name = $"Article {id}",
                Price = new Random().Next(100,500)
            };
        }
    }
}