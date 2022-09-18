using Microsoft.AspNetCore.Mvc;
using Shop.WebApi.Models;
using Shop.WebApi.Services;

namespace Shop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : Controller
    {
        private Db Db;
        private Logger logger;

        private CachedSupplier CachedSupplier;
        private Warehouse Warehouse;
        private Dealer1 Dealer1;
        private Dealer2 Dealer2;

        public ShopController()
        {
            Db = new Db();
            logger = new Logger();
            CachedSupplier = new CachedSupplier();
            Warehouse = new Warehouse();
            Dealer1 = new Dealer1();
            Dealer2 = new Dealer2();
        }




        [HttpGet("GetArticle/{id}/{maxExpectedPrice}")]
        public ActionResult<Article> GetArticle(int id, int maxExpectedPrice = 200)
        {
            Article? article = null;
            Article? tmp = null;
            var articleExists = CachedSupplier.ArticleInInventory(id);
            if (articleExists)
            {
                tmp = CachedSupplier.GetArticle(id);
                if (maxExpectedPrice < tmp.ArticlePrice)
                {
                    articleExists = Warehouse.ArticleInInventory(id);
                    if (articleExists)
                    {
                        tmp = Warehouse.GetArticle(id);
                        if (maxExpectedPrice < tmp.ArticlePrice)
                        {
                            articleExists = Dealer1.ArticleInInventory(id);
                            if (articleExists)
                            {
                                tmp = Dealer1.GetArticle(id);
                                if (maxExpectedPrice < tmp.ArticlePrice)
                                {
                                    articleExists = Dealer2.ArticleInInventory(id);
                                    if (articleExists)
                                    {
                                        tmp = Dealer2.GetArticle(id);
                                        if (maxExpectedPrice < tmp.ArticlePrice)
                                        {
                                            article = tmp;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (article != null)
                    {
                        CachedSupplier.SetArticle(article);
                    }
                }
                else
                {
                    return NotFound();
                }
            }

            return article;
        }

        [HttpPost("{buyerId}")]
        public  ActionResult<Article> BuyArticle([FromBody] Article article, int buyerId)
        {
            if (article == null)
            {
                return UnprocessableEntity();
            }

            logger.Debug($"Trying to sell article with id="+article.ID.ToString());

            article.IsSold = true;
            article.SoldDate = DateTime.Now;
            article.BuyerUserId = buyerId;

            try
            {
                Db.Save(article);
                logger.Info($"Article with id {article.ID} is sold.");
                return article;
            }
            catch (ArgumentNullException ex)
            {
                logger.Error("Could not save article with id " + article.ID.ToString());
                throw new Exception("Could not save article with id");
            }
            catch (Exception)
            {
                return BadRequest(article);
            }
        }

    }
}
