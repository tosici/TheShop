using Microsoft.Extensions.Caching.Memory;
using Shop.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTests
{
    public class MemoryCacheStub : IMemoryCache
    {
        public ICacheEntry CreateEntry(object key)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Remove(object key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(object key, out object value)
        {
            value = new Article { Id = 12, Name = "Test" };
            return true;
        }
    }
}
