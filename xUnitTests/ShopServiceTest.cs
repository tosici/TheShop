using Shop.WebApi.Models;
using Shop.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTests
{
    public class ShopServiceTest
    {

        readonly ShopService shopService;

        public ShopServiceTest()
        {

            shopService = new ShopService(new ShopRepositoryStub(), new MemoryCacheStub(), new OptionsStub());
        }

        
        [Fact]
        public void GetArticle_InCacheOk()
        {
            try
            {
                var article = shopService.GetArticle(12, 22);
                Assert.NotNull(article);
                Assert.IsType<Article>(article);
            }
            catch (Exception ex)
            {
                Assert.True(false, $"{ex.GetBaseException().Message}\r\n{ex.StackTrace}");
            }
        }
    }
}
