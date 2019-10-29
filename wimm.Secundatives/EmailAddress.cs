using System;

namespace wimm.Secundatives
{
    /// <summary>
    /// A validation type to represent an Rfc5322 compliant email address (addr-spec).
    /// </summary>
    public class EmailAddress
    {
        public string LocalPart { get; private set; }

        public string Domain { get; private set; }

        public EmailAddress(string localPart, string domain)
        {
            if (!(Parsers.LocalPart(new StringStream(localPart)) && Parsers.Domain(new StringStream(domain))))
                throw new FormatException("baahahahd");

            LocalPart = localPart;
            Domain = domain;
        }

        private class Parsers
        {

            /// <summary>
            /// addr-spec = local-part "@" domain
            /// </summary>
            public static bool AddrSpec(CharStream s) =>
                Parser.Make(LocalPart)
                    .Then(Parser.Match('@'))
                    .Then(Domain)(s);

            /// <summary>
            /// local-part = dot-atom / quoted-string / obs-local-part
            /// </summary>
            public static bool LocalPart(CharStream s) =>
                Parser.Make(DotAtom)
                    .Or(QuotedString)
                    .Or(ObsLocalPart)(s);

            /// <summary>
            /// domain = dot-atom / domain-literal / obs-domain
            /// </summary>
            public static bool Domain(CharStream s) =>
                 Parser.Make(DotAtom)
                    .Or(DomainLiteral)
                    .Or(ObsDomain)(s);

            /// <summary>
            /// dot-atom = [CFWS] dot-atom-text [CFWS]
            /// </summary>
            public static bool DotAtom(CharStream s) =>
                Parser.Optional(CFWS)
                    .Then(DotAtomText)
                    .Then(Parser.Optional(CFWS))(s);

            /// <summary>
            /// quoted-string = [CFWS] DQUOTE *([FWS] qcontent) [FWS] DQUOTE [CFWS]
            /// </summary>
            public static bool QuotedString(CharStream s) =>
                Parser.Optional(CFWS)
                    .Then(Parser.Match('"'))
                    .Then(Parser.ZeroOrMore(Parser.Optional(FWS).Then(QContent)))
                    .Then(Parser.Optional(FWS))
                    .Then(Parser.Match('"'))
                    .Then(Parser.Optional(CFWS))(s);

            /// <summary>
            /// obs-local-part = word *("." word)
            /// </summary>
            public static bool ObsLocalPart(CharStream s) =>
                Parser.Make(Word).Then(Parser.ZeroOrMore(Parser.Match('.').Then(Word)))(s);

            /// <summary>
            /// domain-literal = [CFWS] "[" *([FWS] dtext) [FWS] "]" [CFWS]
            /// </summary>
            public static bool DomainLiteral(CharStream s) =>
                Parser.Optional(CFWS)
                    .Then(Parser.Match('['))
                    .Then(Parser.ZeroOrMore(Parser.Optional(FWS).Then(DText)))
                    .Then(Parser.Match(']'))
                    .Then(Parser.Optional(CFWS))(s);

            /// <summary>
            /// obs-domain = atom *("." atom)
            /// </summary>
            public static bool ObsDomain(CharStream s) =>
                Parser.Make(Atom).Then(Parser.ZeroOrMore(Parser.Match('.').Then(Atom)))(s);

            /// <summary>
            /// dot-atom-text = 1*atext *("." 1*atext)
            /// </summary>
            public static bool DotAtomText(CharStream s) =>
                Parser.Make(AText).AtLeast(1)
                    .Then(Parser.ZeroOrMore(Parser.Match('.').Then(Parser.Make(AText).AtLeast(1))))(s);

            /// <summary>
            /// qcontent = qtext / quoted-pair
            /// </summary>
            public static bool QContent(CharStream s) => Parser.Make(QText).Or(QuotedPair)(s);

            /// <summary>
            /// word = atom / quoted-string
            /// </summary>
            public static bool Word(CharStream s) => Parser.Make(Atom).Or(QuotedString)(s);

            /// <summary>
            /// dtext = %d33-90 / %d94-126 / obs-dtext
            /// </summary>
            public static bool DText(CharStream s) =>
                Parser.InRange((char)33, (char)90)
                    .Or(Parser.InRange((char)94, (char)126))
                    .Or(ObsDText)(s);

            /// <summary>
            /// atom = [CFWS] 1*atext [CFWS]
            /// </summary>
            public static bool Atom(CharStream s) =>
                Parser.Optional(CFWS)
                    .Then(Parser.Make(AText).AtLeast(1))
                    .Then(Parser.Optional(CFWS))(s);

            /// <summary>
            /// atext           =   ALPHA / DIGIT /    ; Printable US-ASCII
            ///                     "!" / "#" /        ;  characters not including
            ///                     "$" / "%" /        ;  specials.Used for atoms.
            ///                     "&amp;" / "'" /
            ///                     "*" / "+" /
            ///                     "-" / "/" /
            ///                     "=" / "?" /
            ///                     "^" / "_" /
            ///                     "`" / "{" /
            ///                     "|" / "}" /
            ///                     "~"
            /// </summary>
            public static bool AText(CharStream s) =>
                Parser.Make(ALPHA)
                    .Or(DIGIT)
                    .Or(Parser.AnyOf("!#$%&'*+-/=?^_`{|}~"))(s);

            /// <summary>
            /// qtext           =   %d33 /             ; Printable US-ASCII
            ///                     %d35-91 /          ;  characters not including
            ///                     %d93-126 /         ;  "\" or the quote character
            ///                     obs-qtext
            /// </summary>
            public static bool QText(CharStream s) =>
                Parser.Match((char)33)
                    .Or(Parser.InRange((char)35, (char)91))
                    .Or(Parser.InRange((char)93, (char)126))
                    .Or(ObsQText)(s);

            /// <summary>
            /// quoted-pair = ("\" (VCHAR / WSP)) / obs-qp
            /// </summary>
            public static bool QuotedPair(CharStream s) =>
                (Parser.Match('\\').Then(Parser.Make(VCHAR).Or(WSP)))
                    .Or(ObsQp)(s);

            /// <summary>
            /// obs-dtext = obs-NO-WS-CTL / quoted-pair
            /// </summary>
            public static bool ObsDText(CharStream s) => Parser.Make(ObsNoWsCtl).Or(QuotedPair)(s);

            /// <summary>
            /// obs-qtext = obs-NO-WS-CTL
            /// </summary>
            public static bool ObsQText(CharStream s) => ObsNoWsCtl(s);

            /// <summary>
            /// obs-qp = "\" (%d0 / obs-NO-WS-CTL / LF / CR)
            /// </summary>
            public static bool ObsQp(CharStream s) =>
                Parser.Match('\\')
                    .Then(
                        Parser.Match((char)0)
                            .Or(ObsNoWsCtl)
                            .Or(Parser.Match('\n'))
                            .Or(Parser.Match('\r')))(s);

            /// <summary>
            /// obs-NO-WS-CTL   =   %d1-8 /            ; US-ASCII control
            ///                     %d11 /             ;  characters that do not
            ///                     %d12 /             ;  include the carriage
            ///                     %d14-31 /          ;  return, line feed, and
            ///                     %d127              ;  white space characters
            /// </summary>
            public static bool ObsNoWsCtl(CharStream s) =>
                Parser.InRange((char)1, (char)8)
                    .Or(Parser.InRange((char)11, (char)12))
                    .Or(Parser.InRange((char)14, (char)31))
                    .Or(Parser.Match((char)127))(s);

            /// <summary>
            /// comment = "(" *([FWS] ccontent) [FWS] ")"
            /// </summary>
            public static bool Comment(CharStream s) =>
                Parser.Match('(')
                    .Then(Parser.Optional(FWS).Then(CContent).AtLeast(1).Then(Parser.Optional(FWS)))
                    .Then(Parser.Match(')'))(s);

            /// <summary>
            /// ccontent = ctext / quoted-pair / comment
            /// </summary>
            public static bool CContent(CharStream s) =>
                Parser.Make(CText).Or(QuotedPair).Or(Comment)(s);

            /// <summary>
            /// ctext           =   %d33-39 /          ; Printable US-ASCII
            ///                     %d42-91 /          ;  characters not including
            ///                     %d93-126 /         ;  "(", ")", or "\"
            ///                     obs-ctext
            /// </summary>
            public static bool CText(CharStream s) =>
                Parser.InRange((char)33, (char)39)
                    .Or(Parser.InRange((char)42, (char)91))
                    .Or(Parser.InRange((char)93, (char)126))
                    .Or(ObsCText)(s);

            /// <summary>
            /// obs-ctext = obs-NO-WS-CTL
            /// </summary>
            public static bool ObsCText(CharStream s) => ObsNoWsCtl(s);


            /// <summary>
            /// CFWS = (1*([FWS] comment) [FWS]) / FWS
            /// </summary>
            public static bool CFWS(CharStream s) =>
                (Parser.Optional(FWS).Then(Comment).AtLeast(1).Then(Parser.Optional(FWS)))
                    .Or(FWS)(s);

            /// <summary>
            /// FWS = ([*WSP CRLF] 1*WSP) /  obs-FWS
            /// </summary>
            public static bool FWS(CharStream s) =>
                (Parser.Optional(Parser.ZeroOrMore(WSP).Then(Parser.Match('\r')).Then(Parser.Match('\n')))
                    .Then(Parser.Make(WSP).AtLeast(1)))
                    .Or(ObsFWS)(s);

            /// <summary>
            /// obs-FWS = 1*WSP *(CRLF 1*WSP)
            /// </summary>
            public static bool ObsFWS(CharStream s) =>
                Parser.Make(WSP).AtLeast(1)
                    .Then(
                        Parser.ZeroOrMore(
                            Parser.Match('\r').Then(Parser.Match('\n')).Then(Parser.Make(WSP).AtLeast(1))))(s);

            /// <summary>
            /// ALPHA = %x41-5A / %x61-7A   ; A-Z / a-z
            /// </summary>
            public static bool ALPHA(CharStream s) =>
                Parser.InRange('A', 'Z').Or(Parser.InRange('a', 'z'))(s);

            /// <summary>
            /// DIGIT = %x30-39   ; 0-9
            /// </summary>
            public static bool DIGIT(CharStream s) => Parser.InRange('0', '9')(s);

            /// <summary>
            /// VCHAR = %x21-7E   ; visible(printing) characters
            /// </summary>
            public static bool VCHAR(CharStream s) => Parser.InRange((char)0x21, (char)0x7e)(s);

            /// <summary>
            /// WSP = SP / HTAB
            /// </summary>
            public static bool WSP(CharStream s) => Parser.AnyOf("\x20\x09")(s);

        }
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

        public static ParseFn AnyOf(string chars)
        {
            return Make((s) =>
            {
                var n = s.Read();
                return n.Exists && chars.IndexOf(n.Value) > -1;
            });
        }

        public static ParseFn InRange(char min, char max)
        {
            return Make((s) =>
            {
                var n = s.Read();
                return n.Exists && n.Value >= min && n.Value <= max;
            });
        }

        public static ParseFn Optional(ParseFn parse) => s => Make(parse)(s) || true;

        public static ParseFn ZeroOrMore(ParseFn parse)
        {
            var p = Make(parse);

            return (s) =>
            {
                while (p(s)) ;

                return true;
            };
        }
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

        public static ParseFn AtLeast(this ParseFn parse, int times) =>
            Parser.Make(parse.Repeat(times).Then(Parser.ZeroOrMore(parse)));
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
