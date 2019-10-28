using Moq;
using System;
using Xunit;

namespace wimm.Secundatives.UnitTests
{
    public class Parser_Test
    {
        [Fact]
        public void Make_ParseFails_ReturnsFalseAndResetsStream()
        {
            var stream = ResetableStream(42);

            var underTest = Parser.Make(s => false);

            AssertParseFailure(underTest, stream);
        }

        [Fact]
        public void Make_ParseSucceeds_ReturnsTrueAndDoesNotResetStream()
        {
            var stream = new Mock<CharStream>();

            var underTest = Parser.Make(s => true);

            AssertParseSuccess(underTest, stream);
        }

        [Fact]
        public void Make_StreamResetFails_Throws()
        {
            var stream = new Mock<CharStream>();

            var underTest = Parser.Make(s => false);

            Assert.Throws<Exception>(() => underTest(stream.Object));
        }

        [Fact]
        public void Match_NextCharDoesNotMatch_ParseFails()
        {
            var stream = ResetableStream(42);
            stream.Setup(s => s.Read()).Returns('b');

            var underTest = Parser.Match('a');

            AssertParseFailure(underTest, stream);
        }

        [Fact]
        public void Match_NextCharMatches_ParseSucceeds()
        {
            var stream = new Mock<CharStream>();
            stream.Setup(s => s.Read()).Returns('a');

            var underTest = Parser.Match('a');

            AssertParseSuccess(underTest, stream);
        }

        [Fact]
        public void Optional_Fails_ParseSucceedsWithReset()
        {
            var stream = ResetableStream(42);

            var underTest = Parser.Optional(s => false);

            AssertParseSuccess(underTest, stream, 1);
        }

        [Fact]
        public void Optional_Succeeds_ParseSucceeds()
        {
            var stream = new Mock<CharStream>();

            var underTest = Parser.Optional(s => true);

            AssertParseSuccess(underTest, stream);
        }

        [Fact]
        public void ZeroOrMore_NoMatches_SucceedsWithOneCallAndOneReset()
        {
            var stream = ResetableStream(42);

            var parse = new Mock<ParseFn>();

            var underTest = Parser.ZeroOrMore(parse.Object);

            AssertParseSuccess(underTest, stream, streamResets: 1);
            parse.Verify(p => p.Invoke(It.IsAny<CharStream>()), Times.Once);
        }

        [Fact]
        public void ZeroOrMore_NMatches_SucceedsWithNPlusOneCallsAndOneReset()
        {
            var stream = ResetableStream(42);

            var parse = new Mock<ParseFn>();
            parse.SetupSequence(p => p.Invoke(It.IsAny<CharStream>()))
                .Returns(true)
                .Returns(true);

            var underTest = Parser.ZeroOrMore(parse.Object);

            AssertParseSuccess(underTest, stream, streamResets: 1);
            parse.Verify(p => p.Invoke(It.IsAny<CharStream>()), Times.Exactly(3));
        }

        [Fact]
        public void Complete_OriginalParseFails_ParseFails()
        {
            var stream = ResetableStream(42);

            var underTest = new ParseFn(s => false).Complete();

            AssertParseFailure(underTest, stream); 
        }

        [Fact]
        public void Complete_ExtraCharactersInStream_ParseFails()
        {
            var stream = ResetableStream(42);
            stream.Setup(s => s.Read()).Returns('a');

            var underTest = new ParseFn(s => false).Complete();

            AssertParseFailure(underTest, stream);
        }

        [Fact]
        public void Complete_ParseConsumesEntireStream_ParseSucceeds()
        {
            var stream = new Mock<CharStream>();
            // the stream will naturally return a None maybe from Read()

            var underTest = new ParseFn(s => true).Complete();

            AssertParseSuccess(underTest, stream);
        }

        [Fact]
        public void Or_NeitherParseSucceeds_ParseFails()
        {
            var stream = ResetableStream(42);

            var underTest = new ParseFn(s => false).Or(s => false);

            AssertParseFailure(underTest, stream);
        }

        [Fact]
        public void Or_FirstParseSucceeds_ParseSucceeds()
        {
            var stream = new Mock<CharStream>();

            var underTest = new ParseFn(s => true).Or(s => false);

            AssertParseSuccess(underTest, stream);
        }

        [Fact]
        public void Or_SecondParseSucceeds_ParseSucceeds()
        {
            var stream = new Mock<CharStream>();

            var underTest = new ParseFn(s => false).Or(s => true);

            AssertParseSuccess(underTest, stream);
        }

        [Fact]
        public void Then_FirstParseFails_ParseFails()
        {
            var stream = ResetableStream(42);

            var underTest = new ParseFn(s => false).Then(s => true);

            AssertParseFailure(underTest, stream);
        }

        [Fact]
        public void Then_SecondParseFails_ParseFails()
        {
            var stream = ResetableStream(42);

            var underTest = new ParseFn(s => true).Then(s => false);

            AssertParseFailure(underTest, stream);
        }

        [Fact]
        public void Then_BothParsesSucceed_ParseSucceeds()
        {
            var stream = ResetableStream(42);

            var underTest = new ParseFn(s => true).Then(s => true);

            AssertParseSuccess(underTest, stream);
        }

        
        [Fact]
        public void Repeat_ZeroTimes_ParseSucceeds()
        {
            var stream = new Mock<CharStream>();

            var underTest = new ParseFn(s => false).Repeat(0);

            AssertParseSuccess(underTest, stream);
        }

        [Fact]
        public void Repeat_NotAllRepetitionsSucceed_ParseFails()
        {
            var stream = ResetableStream(42);

            var parse = new Mock<ParseFn>();
            parse.SetupSequence(s => s.Invoke(It.IsAny<CharStream>()))
                .Returns(true)
                .Returns(false);

            var underTest = parse.Object.Repeat(3);

            AssertParseFailure(underTest, stream);
            parse.Verify(p => p.Invoke(It.IsAny<CharStream>()), Times.Exactly(2));
        }
        
        [Fact]
        public void Repeat_AllRepeitionsSucceed_ParseSucceeds()
        {
            var stream = new Mock<CharStream>();

            var parse = new Mock<ParseFn>();
            parse.Setup(s => s.Invoke(It.IsAny<CharStream>())).Returns(true);

            var underTest = parse.Object.Repeat(3);

            AssertParseSuccess(underTest, stream);
            parse.Verify(p => p.Invoke(It.IsAny<CharStream>()), Times.Exactly(3));
        }

        private void AssertParseSuccess(ParseFn parse, Mock<CharStream> stream) =>
            AssertParseSuccess(parse, stream, 0);

        private void AssertParseSuccess(ParseFn parse, Mock<CharStream> stream, int streamResets)
        {
            Assert.True(parse(stream.Object));
            stream.Verify(s => s.Seek(It.IsAny<long>()), Times.Exactly(streamResets));
        }

        private void AssertParseFailure(ParseFn parse, Mock<CharStream> stream) =>
            AssertParseFailure(parse, stream, 1);

        private void AssertParseFailure(ParseFn parse, Mock<CharStream> stream, int streamResets)
        {
            Assert.False(parse(stream.Object));
            stream.Verify(s => s.Seek(It.IsAny<long>()), Times.Exactly(streamResets));
        }

        private Mock<CharStream> ResetableStream(long position)
        {
            var stream = new Mock<CharStream>();

            stream.SetupGet(s => s.Position).Returns(position);
            stream.Setup(s => s.Seek(position)).Returns(true);

            return stream;
        }
    }

    public class StringStream_Test
    {
        [Fact]
        public void Construct_NullStr_Throws() =>
            Assert.Throws<ArgumentNullException>(() => new StringStream(null));

        [Fact]
        public void Construct_NonNullStr_Constructs()
        {
            var _ = new StringStream("not null");
        }

        [Fact]
        public void Position_NoReads_IsZero()
        {
            var underTest = new StringStream("some value");

            Assert.Equal(0, underTest.Position);
        }

        [Fact]
        public void Position_DuringSequentialRead_Increments()
        {
            var chars = "the characters to read";
            var underTest = new StringStream(chars);

            for (var i = 1; i <= chars.Length; i++)
            {
                var _ = underTest.Read();

                Assert.Equal(i, underTest.Position);
            }
        }

        [Fact]
        public void Position_AfterAllReads_RemainsDataLength()
        {
            var chars = "the characters to read";
            var underTest = new StringStream(chars);

            for (var i = 1; i <= chars.Length; i++)
            {
                var __ = underTest.Read();
            }

            Assert.Equal(chars.Length, underTest.Position);
            var _ = underTest.Read();
            Assert.Equal(chars.Length, underTest.Position);
        }

        [Fact]
        public void Read_StartToFinish_ReturnsExpectedChars()
        {
            var chars = "the characters to read";
            var underTest = new StringStream(chars);

            foreach (var c in chars)
            {
                Assert.Equal(c, underTest.Read().Value);
            }
        }

        [Fact]
        public void Read_AfterEnd_KeepaReturningNone()
        {
            var chars = "the characters to read";
            var underTest = new StringStream(chars);

            for (var i = 1; i <= chars.Length; i++)
            {
                var _ = underTest.Read();
            }

            Assert.Equal(Maybe<char>.None, underTest.Read());
            Assert.Equal(Maybe<char>.None, underTest.Read());
        }

        [Fact]
        public void Seek_OffsetOutOfBounds_ReturnsFalseAndDoesNotChangePosition()
        {
            var chars = "the characters to read";
            var underTest = new StringStream(chars);

            var _ = underTest.Read();
            _ = underTest.Read();

            Assert.False(underTest.Seek(chars.Length * 2));
            Assert.Equal(2, underTest.Position);
        }

        [Fact]
        public void Seek_Inbounds_SeeksToPosition()
        {
            var chars = "the characters to read";
            var underTest = new StringStream(chars);

            var p = chars.Length / 2;
            underTest.Seek(p);

            Assert.Equal(p, underTest.Position);

            for (var i = p; i < chars.Length;  i++)
            {
                Assert.Equal(chars[i], underTest.Read().Value);
            }
        }
    }
}
