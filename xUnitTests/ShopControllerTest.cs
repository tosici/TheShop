using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shop.WebApi.Controllers;

namespace xUnitTests
{
    public class ShopControllerTest
    {

        #region members
        readonly ShopController _controller;
        readonly ShopServiceStub shopService;
        readonly LoggerStub logger;
        
        #endregion

        #region Ctors
        public ShopControllerTest()
        {
            try
            {
                shopService = new ShopServiceStub();
                logger = new LoggerStub();
                _controller = new ShopController(shopService, logger);
            }
            catch (Exception ex)
            {
                Assert.True(false, $"{ex.GetBaseException().Message}\r\n{ex.StackTrace}");
            }
        }
        #endregion

        [Fact]
        public void GetArticle_Ok()
        {
            try
            {
                var result = _controller.GetArticle(12, 13);
                Assert.IsType<OkObjectResult>(result.Result);
            }
            catch (Exception ex)
            {
                Assert.True(false, $"{ex.GetBaseException().Message}\r\n{ex.StackTrace}");
            }
        }

        [Fact]
        public void GetArticle_Empty()
        {
            try
            {
                var result = _controller.GetArticle(0);
                Assert.IsType<EmptyResult>(result.Result);
            }
            catch (Exception ex)
            {
                Assert.True(false, $"{ex.GetBaseException().Message}\r\n{ex.StackTrace}");
            }
        }

        [Fact]
        public void GetArticle_BadRequest()
        {
            try
            {
                var result = _controller.GetArticle(1);
                Assert.IsType<BadRequestObjectResult>(result.Result);
            }
            catch (Exception ex)
            {
                Assert.True(false, $"{ex.GetBaseException().Message}\r\n{ex.StackTrace}");
            }
        }


    }
}