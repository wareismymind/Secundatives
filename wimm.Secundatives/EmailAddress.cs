using System;

namespace wimm.Secundatives
{
    /// <summary>
    /// A validation type to represent an Rfc5322 compliant email address (addr-spec).
    /// </summary>
    public class EmailAddress
    {
        public EmailAddress(string localPart, string domain)
        {
            if (!(LocalPart(new StringStream(localPart)) && Domain(new StringStream(domain))))
                throw new FormatException("baahahahd");

            _localPart = localPart;
            _domain = domain;
        }

        private string _localPart;
        private string _domain;

        /// <summary>
        /// addr-spec = local-part "@" domain
        /// </summary>
        public static ParseFn AddrSpec =
            LocalPart
                .Then(Parser.Match('@'))
                .Then(Domain);

        /// <summary>
        /// local-part = dot-atom / quoted-string / obs-local-part
        /// </summary>
        public static ParseFn LocalPart =
            DotAtom
                .Or(QuotedString)
                .Or(ObsLocalPart);

        /// <summary>
        /// domain = dot-atom / domain-literal / obs-domain
        /// </summary>
        public static ParseFn Domain =
             DotAtom
                .Or(DomainLiteral)
                .Or(ObsDomain);

        /// <summary>
        /// dot-atom = [CFWS] dot-atom-text [CFWS]
        /// </summary>
        public static ParseFn DotAtom =
            (CFWS.Optional())
                .Then(DotAtomText)
                .Then((CFWS.Optional()));

        public static ParseFn QuotedString = s => false;

        public static ParseFn ObsLocalPart = s => false;

        public static ParseFn DomainLiteral = s => false;

        public static ParseFn ObsDomain = s => false;

        public static ParseFn DotAtomText = s => false;



        public static ParseFn CFWS = s => false;

    }

    /// <summary>
    /// A set of <see cref="ParseFn"/> factories for common patterns.
    /// </summary>
    public static class Parser
    {
        /// <summary>
        /// Makes a parser that encapsulates a parsing function and returns the incoming stream to
        /// its original state if the parse function fails.
        /// </summary>
        /// <param name="parse">The parsing function.</param>
        /// <returns>
        /// A parser that uses the given parse function to parse a stream and returns the stream to
        /// its original state if the parse function fails. If the stream cannot be returned to its
        /// original position after a parse fails the returned parser will throw an
        /// <see cref="Exception"/>.
        /// </returns>
        public static ParseFn Make(ParseFn parse)
        {
            return (s) =>
            {
                var position = s.Position;
                var result = parse(s);

                if (!result)
                    if (!s.Seek(position))
                        // TODO: Throw different exception
                        throw new Exception($"CharStream could not Seek to the Position it started in ({position}).");

                return result;
            };
        }

        public static ParseFn Match(char c) => Make((s) => s.Read() == c);
    }

    /// <summary>
    /// A function that consumes a specific pattern from an input stream and returns <c>true</c>,
    /// or returns <c>false</c> and leaves the stream unchanged.
    /// </summary>
    /// <param name="input">The input stream.</param>
    /// <returns>
    /// <c>true</c> if the stream began with a match for the pattern being parsed, otherwise
    /// <c>false</c>.
    /// </returns>
    public delegate bool ParseFn(CharStream input);

    public static class ParserExtensions
    {
        public static ParseFn Complete(this ParseFn a) =>
            Parser.Make(s => a(s) && !s.Read().Exists);

        public static ParseFn Or(this ParseFn a, ParseFn b) => Parser.Make(s => a(s) || b(s));

        public static ParseFn Then(this ParseFn a, ParseFn b) => Parser.Make(s => a(s) && b(s));

        public static ParseFn Optional(this ParseFn parse) => s => Parser.Make(parse)(s) || true;

        public static ParseFn Repeat(this ParseFn parse, int times)
        {
            return Parser.Make(s =>
            {
                for (var i = 0; i < times; i++)
                {
                    if (!parse(s))
                        return false;
                }

                return true;
            });
        }

    }

    /// <summary>
    /// A read-only, seekable stream of <see cref="char"/>s.
    /// </summary>
    public interface CharStream
    {
        /// <summary>
        /// The position within the stream. When the whole stream has been read then
        /// <see cref="Position"/> is equal to the length of the stream.
        /// </summary>
        long Position { get; }

        /// <summary>
        /// Reads a char from the stream and advances the position.
        /// </summary>
        /// <returns>
        /// The next char in the stream, or <see cref="Maybe{CharStream}.None"/> if the whole
        /// stream has already been read.
        /// </returns>
        Maybe<char> Read();

        /// <summary>
        /// Sets the read position of the stream to the specified offset. The offset must be
        /// greater than or equal to 0 and less than or equal to the length of the data. An offset
        /// of 0 will seek to the begining of the stream, 1 to the second char, etc. An offset
        /// equal to the length of the data will seek past the data. An offset greater than the
        /// legnth of the data will cause the function to fail and return false.
        /// </summary>
        /// <param name="offset">The position to seek to.</param>
        /// <returns><c>true</c> if the operation succeeded, otherwise <c>false</c>.</returns>
        bool Seek(long offset);
    }

    /// <summary>
    /// A <see cref="CharStream"/> wrapper for a <see cref="string"/>.
    /// </summary>
    public class StringStream : CharStream
    {
        /// <summary>
        /// Initializes a new <see cref="StringStream"/> for <paramref name="str"/>.
        /// </summary>
        /// <param name="str">The string to read.</param>
        public StringStream(string str)
        {
            _str = str ?? throw new ArgumentNullException(nameof(str));
            Position = 0;
        }

        /// <inheritDoc/>
        public long Position { get; private set; }

        /// <inheritDoc/>
        public Maybe<char> Read()
        {
            if (Position == _str.Length)
                return Maybe<char>.None;

            char c = _str[(int)Position];
            Position++;
            return c;
        }

        /// <inheritDoc/>
        public bool Seek(long offset)
        {
            if (offset < 0 || offset > _str.Length)
                return false;

            Position = offset;
            return true;
        }

        private string _str;
    }
}
