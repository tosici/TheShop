using Microsoft.AspNetCore.Mvc;
using Vendor.WebApi.Models;
using Vendor.WebApi.Services;

namespace Vendor.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : Controller
    {
        private DatabaseDriver DatabaseDriver;
        private Logger logger;

        private SupplierService _supplierService;

        public SupplierController()
        {
            DatabaseDriver = new DatabaseDriver();
            logger = new Logger();
            _supplierService = new SupplierService();
        }

        [HttpGet("ArticleInInventory/{id}")]
        public ActionResult<bool> ArticleInInventory(int id)
        {
            return _supplierService.ArticleInInventory(id);
        }

        [HttpGet("GetArticle/{id}")]
        public ActionResult<Article> GetArticle(int id)
        {

            var sd = new ServiceReferencePay.PaymentServiceSoapClient(ServiceReferencePay.PaymentServiceSoapClient.EndpointConfiguration.PaymentServiceSoap12);
            var res = sd.GetAllFileTypes(new ServiceReferencePay.GetAllFileTypesRequest(new ServiceReferencePay.CallContext()));
            var articleExists = _supplierService.ArticleInInventory(id);
            if (articleExists)
            {
                return _supplierService.GetArticle(id);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("{buyerId}")]
        public ActionResult<Article> BuyArticle([FromBody]Article article, int buyerId)
        {
            var id = article.ID;
            if (article == null)
            {
                return UnprocessableEntity();
            }

            logger.Debug("Trying to sell article with id=" + id);

            article.IsSold = true;
            article.SoldDate = DateTime.Now;
            article.BuyerUserId = buyerId;

            try
            {
                DatabaseDriver.Save(article);
                logger.Info("Article with id=" + id + " is sold.");
                return article;
            }
            catch (ArgumentNullException ex)
            {
                logger.Error("Could not save article with id=" + id);
                throw new Exception("Could not save article with id");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

