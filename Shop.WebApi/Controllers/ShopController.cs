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
        private readonly ILogger<ShopController> _logger;

        public ShopController(IShopService shopService, ILogger<ShopController> logger)
        {
            _shopService = shopService;
            _logger = logger;
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
            catch (UnauthorizedAccessException)
            {
                _logger.LogError($"Authorization error when getting article {id}");
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            
            if (article == null)
            {
                _logger.LogInformation($"Article {id} not found for price {maxExpectedPrice}");
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
            catch (UnauthorizedAccessException)
            {
                _logger.LogError($"Authorization error when gettting article {id}");
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
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
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
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
            catch (UnauthorizedAccessException)
            {
                _logger.LogError($"Authorization error when buying article {article.Id} from dealer {article.DealerId}");
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

            return Ok(article);
        }
        #endregion
    }
}
