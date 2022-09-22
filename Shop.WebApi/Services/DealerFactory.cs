using Shop.WebApi.Interfaces;
using Shop.WebApi.Models;

namespace Shop.WebApi.Services
{
    class DealerFactory
    {
        public static IDealerService GetDealer(Dealer dealer)
        {
            switch (dealer.DealerId)
            {
                case "1":
                    return new Dealer1(dealer.DealerUrl);
                case "2":
                    return new Dealer2(dealer.DealerUrl);
                default:
                    throw new ApplicationException($"Dealer service {dealer} cannot be created");
            }

        }
    }
}
