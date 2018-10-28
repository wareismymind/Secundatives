using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using wimm.Secundatives.Extensions;

namespace wimm.Secundatives.UnitTests.Extensions
{
    public class VariantMap_Test
    {
        [Fact]
        public void MapValue_ValueInt_ReturnsIntFuncResult()
        {
            var underTest = ConstructInt();
            Assert.Equal(43, underTest.MapValue((x) => x + 1, x => x.Length));
        }

        [Fact]
        public void MapValue_ValueString_ReturnsStringFuncResult()
        {
            var underTest = ConstructString();
            Assert.Equal(4, underTest.MapValue((x) => x + 1, x => x.Length));
        }

        [Fact]
        public void MapAction_ValueInt_ExecutesIntAction()
        {
            var underTest = ConstructInt();
            int i = 0;
            underTest.MapAction(x => i++, y => i--);
            Assert.Equal(1, i);
        }

        [Fact]
        public void MapAction_ValueString_ExecutesStringAction()
        {
            var underTest = ConstructString();
            int i = 0;
            underTest.MapAction(x => i++, y => i--);
            Assert.Equal(-1, i);
        }

        public Variant<int,string> ConstructInt()
        {
            return new Variant<int, string>(42);
        }

        public Variant<int,string> ConstructString()
        {
            return new Variant<int, string>("doot");
        }
    }
}
