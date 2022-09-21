using Shop.WebApi.Interfaces;

namespace Shop.WebApi.Services
{
    class DealerFactory
    {
        public static IDealer GetDealer(int dealer)
        {
            switch (dealer)
            {
                case 1:
                    return new Dealer1();
                case 2:
                    return new Dealer2();
                default:
                    throw new ApplicationException($"Dealer service {dealer} cannot be created");
            }

        }
    }
}
