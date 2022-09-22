using Microsoft.AspNetCore.Mvc;
using Shop.WebApi.DatabaseLayer;
using Shop.WebApi.Interfaces;
using Shop.WebApi.Models;
using Shop.WebApi.Services;
using System.ComponentModel.DataAnnotations;

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

        #region get
        [HttpGet("GetArticle/{id}/{maxExpectedPrice}")]
        public ActionResult<Article?> GetArticle(int id, int maxExpectedPrice = 200)
        {
            Article? article = null;
            try
            {
                article = _shopService.GetArticle(id, maxExpectedPrice);
            }
            catch (ValidationException v)
            {
                return BadRequest(v.Message);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            if (article == null)
            {
                return new EmptyResult();
            }
            else
            {
                return Ok(article);
            }
        }

        [HttpGet("GetArticleById/{id}")]
        public ActionResult<Article?> GetArticleById(int id)
        {
            Article? article = null;
            try
            {
                article = _shopService.GetArticleById(id);
            }
            catch (ValidationException v)
            {
                return BadRequest(v.Message);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (article == null)
            {
                return new EmptyResult();
            }
            else
            {
                return Ok(article);
            }
        }

        [HttpGet("GetArticlesLocal")]
        public ActionResult<List<Article>> GetArticlesLocal()
        {
            List<Article> articles = new List<Article>();
            try
            {
                articles = _shopService.GetArticlesLocal();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(articles);
        }
        #endregion

        #region post
        [HttpPost("{buyerId}")]
        public  ActionResult<Article> BuyArticle([FromBody] Article article, int buyerId)
        {
            if (article == null)
            {
                return UnprocessableEntity();
            }
            try
            {
                _shopService.BuyArticle(article, buyerId);
            }
            catch (ValidationException v)
            {
                return BadRequest(v.Message);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(article);
        }
        #endregion
    }
}
