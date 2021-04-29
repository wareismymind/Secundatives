using System.Threading.Tasks;
using Xunit;

namespace wimm.Secundatives.UnitTests.Extensions
{
    public class ResultMapping_Test
    {
        [Fact]
        public void MapResult_IsSuccessAndFuncReturnsValue_ReturnsResultOfFunc()
        {
            var underTest = ConstructWith("doot");
            var res = underTest.Map(x => "deet");

            Assert.Equal("deet", res.Value);
        }

        [Fact]
        public void MapResult_IsErrorAndFuncReturnsValue_ReturnsError()
        {
            var underTest = new Result<string, TestError>(TestError.Sadness);

            var res = underTest.Map(x => "deet");

            Assert.True(res.IsError);
            Assert.Equal(TestError.Sadness, res.Error);

        }

        [Fact]
        public async Task MapResult_IsSuccessAndFuncReturnsTask_ReturnsResultOfFunc()
        {
            var underTest = ConstructWith("doot");
            var res = await underTest.Map(x => Task.FromResult("skeleton"));

            Assert.Equal("skeleton", res.Value);
        }

        [Fact]
        public async Task MapResult_IsErrorAndFuncReturnsTask_ReturnsError()
        {
            var underTest = new Result<string, TestError>(TestError.Sadness);
            var res = await underTest.Map(x => Task.FromResult("skeleton"));

            Assert.True(res.IsError);
            Assert.Equal(TestError.Sadness, res.Error);
        }

        [Fact]
        public async Task MapTaskResult_IsSuccessAndFuncReturnsValue_ReturnsResultOfFunc()
        {
            var underTest = Task.FromResult(ConstructWith("doot"));
            var res = await underTest.Map(x => "skeleton");

            Assert.Equal("skeleton", res.Value);
        }

        [Fact]
        public async Task MapTaskResult_IsErrorAndFuncReturnsValue_ReturnsError()
        {
            var underTest = Task.FromResult(new Result<string, TestError>(TestError.Sadness));
            var res = await underTest.Map(x => "skeleton");

            Assert.True(res.IsError);
            Assert.Equal(TestError.Sadness, res.Error);
        }


        [Fact]
        public async Task MapTaskResult_IsSuccessAndFuncReturnsTask_ReturnsResultOfFunc()
        {
            var underTest = Task.FromResult(ConstructWith("doot"));
            var res = await underTest.Map(x => Task.FromResult("skeleton"));

            Assert.Equal("skeleton", res.Value);
        }

        [Fact]
        public async Task MapTaskResult_IsErrorAndFuncReturnsTask_ReturnsError()
        {
            var underTest = Task.FromResult(new Result<string, TestError>(TestError.Sadness));
            var res = await underTest.Map(x => Task.FromResult("skeleton"));

            Assert.True(res.IsError);
            Assert.Equal(TestError.Sadness, res.Error);
        }

        [Fact]
        public async Task MapResult_IsSuccessAndFuncReturnsTaskResult_ReturnsResultOfFunc()
        {
            var underTest = ConstructWith("doot");
            var res = await underTest.Map(x => Task.FromResult(ConstructWith("skeleton")));

            Assert.Equal("skeleton", res.Value);
        }

        [Fact]
        public async Task MapResult_IsErrorAndFuncReturnsTaskResult_ReturnsError()
        {
            var underTest = new Result<string, TestError>(TestError.Sadness);
            var res = await underTest.Map(x => Task.FromResult(ConstructWith("skeleton")));

            Assert.True(res.IsError);
            Assert.Equal(TestError.Sadness, res.Error);
        }
        //

        [Fact]
        public async Task MapTaskResult_IsSuccessAndFuncReturnsTaskResult_ReturnsResultOfFunc()
        {
            var underTest = Task.FromResult(ConstructWith("doot"));
            var res = await underTest.Map(x => Task.FromResult(ConstructWith("skeleton")));

            Assert.Equal("skeleton", res.Value);
        }

        [Fact]
        public async Task MapTaskResult_IsErrorAndFuncReturnsTaskResult_ReturnsError()
        {
            var underTest = Task.FromResult(new Result<string, TestError>(TestError.Sadness));
            var res = await underTest.Map(x => Task.FromResult(ConstructWith("skeleton")));

            Assert.True(res.IsError);
            Assert.Equal(TestError.Sadness, res.Error);
        }

        [Fact]
        public void MapErrorValue_IsErrorAndFuncReturnsValue_ReturnsResultOfFunc()
        {
            var underTest = new Result<string, TestError>(TestError.Sadness);
            var res = underTest.MapError(x => OtherError.OtherSadness);

            Assert.True(res.IsError);
            Assert.Equal(OtherError.OtherSadness, res.Error);
        }


        [Fact]
        public void MapErrorValue_IsSuccessAndFuncReturnsValue_ReturnsValue()
        {
            var underTest = ConstructWith("doot");
            var res = underTest.MapError(x => OtherError.OtherSadness);

            Assert.Equal("doot", res.Value);
        }

        [Fact]
        public async Task MapErrorTask_IsErrorAndFuncReturnsValue_ReturnsResultOfFunc()
        {
            var underTest = Task.FromResult(new Result<string, TestError>(TestError.Sadness));
            var res = await underTest.MapError(x => OtherError.OtherSadness);

            Assert.True(res.IsError);
            Assert.Equal(OtherError.OtherSadness, res.Error);
        }


        [Fact]
        public async Task MapErrorTask_IsSuccessAndFuncReturnsValue_ReturnsValue()
        {
            var underTest = Task.FromResult(ConstructWith("doot"));
            var res = await underTest.MapError(x => OtherError.OtherSadness);

            Assert.Equal("doot", res.Value);
        }

        [Fact]
        public void MapOr_IsSuccess_ReturnsResultOfFunc()
        {
            var underTest = ConstructWith(1);
            var res = underTest.MapOr(_ => "deet", _ => OtherError.OtherSadness.ToString());

            Assert.Equal("deet", res);
        }


        [Fact]
        public void MapOr_IsError_ReturnsResultOfErrorFunc()
        {
            var underTest = new Result<string, TestError>(TestError.Sadness);
            var res = underTest.MapOr(_ => "deet", _ => OtherError.OtherSadness.ToString());

            Assert.Equal(OtherError.OtherSadness.ToString(), res);
        }

        private Result<T, TestError> ConstructWith<T>(T value)
        {
            return new Result<T, TestError>(value);
        }


        private enum TestError
        {
            Sadness
        }

        private enum OtherError
        {
            OtherSadness
        }

    }
}
