using System;
using Xunit;

namespace wimm.Secundatives.UnitTests
{
    public class Variant_Test
    {
        private const string _testString = "doot";

        [Fact]
        public void GetLong_LongNotTypeParam_Throws()
        {
            var underTest = ConstructInt();
            Assert.Throws<NotSupportedException>(() => underTest.Is<long>());
        }



        [Fact]
        public void GetString_ContainsInt_Throws()
        {
            var underTest = ConstructInt();
            Assert.Throws<InvalidOperationException>(() => underTest.Get<string>());
        }

        [Fact]
        public void IsBase_ContainsDerived_ReturnsTrue()
        {
            var underTest = new Variant<string, A>(new B());
            Assert.True(underTest.Is<A>());
        }

        [Fact]
        public void IsDerived_ContainsBase_Throws()
        {
            var underTest = new Variant<string, A>(new A());
            Assert.Throws<NotSupportedException>(() => underTest.Get<B>());
        }


        [Fact]
        public void GetString_ContainsString_ReturnsString()
        {
            var underTest = ConstructString();
            Assert.Equal(_testString, underTest.Get<string>());
        }
        
        [Fact]
        public void IsString_ContainsString_ReturnsTrue()
        {
            var underTest = ConstructString();
            Assert.True(underTest.Is<string>());
        }

        [Fact]
        public void IsString_ContainsInt_ReturnsFalse()
        {
            var underTest = ConstructInt();
            Assert.False(underTest.Is<string>());
        }

        [Fact]
        public void Construct_ValidValue_ConstructsWithValue()
        {
            var stringVariant = new Variant<int, string>("doot");
            Assert.True(stringVariant.Is<string>());

            var intVariant = new Variant<int, string>(16);
            Assert.True(intVariant.Is<int>());
        }

        private Variant<int, string> ConstructString()
        {
            return new Variant<int, string>(_testString);
        }

        private Variant<int, string> ConstructInt()
        {
            return new Variant<int, string>(42);
        }

        class A { }
        class B : A { }
    }
}
