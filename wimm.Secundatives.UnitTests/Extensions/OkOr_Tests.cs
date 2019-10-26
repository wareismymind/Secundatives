using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace wimm.Secundatives.UnitTests.Extensions
{
    public class OkOr_Tests
    {
        [Fact]
        public void OkOrFunc_Value_ReturnsResultWithValue()
        {
            var underTest = new Maybe<string>("doot");
            
            var result = underTest.OkOr(() => TestingEnum.BadThingsHappened);

            Assert.Equal("doot", result.Value);
        }

        [Fact]
        public void OkOrFunc_None_ReturnsFuncResultAsError()
        {
            var underTest = Maybe<string>.None;

            var result = underTest.OkOr(() => TestingEnum.BadThingsHappened);

            Assert.Equal(TestingEnum.BadThingsHappened, result.Error);
        }


        [Fact]
        public void OkOrErrorValue_Value_ReturnsResultWithValue()
        {
            var underTest = new Maybe<string>("doot");

            var result = underTest.OkOr(TestingEnum.BadThingsHappened);

            Assert.Equal("doot", result.Value);
        }

        [Fact]
        public void OkOrErrorValue_None_ReturnsErrorValue()
        {
            var underTest = Maybe<string>.None;

            var result = underTest.OkOr(TestingEnum.BadThingsHappened);

            Assert.Equal(TestingEnum.BadThingsHappened, result.Error);
        }


        [Fact]
        public async Task OkOrErrorTaskFunc_Value_ReturnsResultWithValue()
        {
            var underTest = Task.FromResult(new Maybe<string>("doot"));
            var result = await underTest.OkOr(() => TestingEnum.BadThingsHappened);

            Assert.Equal("doot", result.Value);
        }


        [Fact]
        public async Task OkOrErrorTaskFunc_None_ReturnsResultWithValue()
        {
            var underTest = Task.FromResult(Maybe<string>.None);
            var result = await underTest.OkOr(() => TestingEnum.BadThingsHappened);

            Assert.Equal(TestingEnum.BadThingsHappened, result.Error);
            
        }


        [Fact]
        public async Task OkOrErrorTaskValue_Value_ReturnsResultWithValue()
        {
            var underTest = Task.FromResult(new Maybe<string>("doot"));
            var result = await underTest.OkOr(TestingEnum.BadThingsHappened);

            Assert.Equal("doot", result.Value);
        }

        [Fact]
        public async Task OkOrErrorTaskValue_None_ReturnsErrorValue()
        {
            var underTest = Task.FromResult(Maybe<string>.None);
            var result = await underTest.OkOr(TestingEnum.BadThingsHappened);
            Assert.Equal(TestingEnum.BadThingsHappened, result.Error);
        }



        public enum TestingEnum
        {
            BadThingsHappened
        }


    }
}
