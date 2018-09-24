using System;
using Xunit;

namespace Zillow.Tests
{
    public class Tests
    {
        [Fact]
        public void GetZesimate_Success()
        {
            var zillow = new Zillow();

            var result = zillow.GetZestimate(44564688);

            Assert.NotNull(result);
        }
    }
}
