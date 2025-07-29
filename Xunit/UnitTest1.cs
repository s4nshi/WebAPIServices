using Xunit;
using WebAPI.Services;
using System.Collections.Generic;

namespace Xunit
{
    public class UnitTest1
    {
        private readonly IAdPlatformService _service;
        public UnitTest1()
        {
            _service = new AdPlatformService();
        }

        public void Test1()
        {

        }
    }
}
