using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.WebApi.Controllers;

namespace xUnitTests
{
    public class ShopControllerTest
    {

        #region members
        ShopController _controller;
        ShopServiceStub shopService;
        #endregion

        #region Ctors
        public ShopControllerTest()
        {
            try
            {
                shopService = new ShopServiceStub();
                _controller = new ShopController(shopService);
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
    }
}