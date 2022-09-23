using Microsoft.Extensions.Options;
using Shop.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTests
{
    public class OptionsStub : IOptions<List<Dealer>>
    {
        public List<Dealer> Value => throw new NotImplementedException();
    }
}
