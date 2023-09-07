using AutoFixture;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest.Services
{
    public class BaseTest
    {
        protected readonly Fixture _fixture;

        public BaseTest()
        {
            _fixture = new Fixture();
        }
    }
}
