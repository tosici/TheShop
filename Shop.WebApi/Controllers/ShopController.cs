using Microsoft.AspNetCore.Mvc;
using Shop.WebApi.DatabaseLayer;
using Shop.WebApi.Interfaces;
using Shop.WebApi.Models;
using Shop.WebApi.Services;

namespace Shop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }


        [HttpGet("GetArticle/{id}/{maxExpectedPrice}")]
        public ActionResult<Article?> GetArticle(int id, int maxExpectedPrice = 200)
        {
            Article? article = _shopService.GetArticle(id, maxExpectedPrice);
            if (article == null)
            {
                return new EmptyResult();
            }
            else
            {
                return Ok(article);
            }

        }

        [HttpPost("{buyerId}")]
        public  ActionResult<Article> BuyArticle([FromBody] Article article, int buyerId)
        {
            if (article == null)
            {
                return UnprocessableEntity();
            }
            _shopService.BuyArticle(article,buyerId);

            return Ok(article);
        }

    }
}
