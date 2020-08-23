using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShortUrlTests
{
    
    class HasherTests
    {
        private string _testurl;

        [SetUp]
        public void SetUp()
        {
            _testurl = @"https://docs.microsoft.com/ru-ru/dotnet/standard/base-types/standard-numeric-format-strings";
        }

        [Test]
        public void Is_GetShortHash_NotNull()
        {
            var result = ShortUrl.Models.Logic.Hash.Hasher.GetShortHash(_testurl); 

            Assert.IsNotNull(result);
        }

        [Test]
        public void Is_GetShortHash_ThrowsNullArgumentException()
        {
            Assert.Throws<ArgumentNullException>( () => ShortUrl.Models.Logic.Hash.Hasher.GetShortHash(null));
            Assert.Throws<ArgumentNullException>(() => ShortUrl.Models.Logic.Hash.Hasher.GetShortHash(""));
            Assert.Throws<ArgumentNullException>(() => ShortUrl.Models.Logic.Hash.Hasher.GetShortHash(" "));
        }

        [Test]
        public void Are_GetShortHash_Equal()
        {
            var result = ShortUrl.Models.Logic.Hash.Hasher.GetShortHash(_testurl);

            Assert.AreEqual("4817b", result);
        }
    }


}
