using System;

namespace cm
{
    class Program
    {
        static void Main(string[] args)
        {
            // for (int i = 0; i < 4; i++)
            // {
            //     Random random = new Random();
            //     int num = random.Next(1, 1000) + 100;
            //     Console.WriteLine(num);
            // }

            while (true)
            {
                Console.Write("> ");
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    return;

                var lexer = new Lexer(line);
                while (true)
                {
                    var token = lexer.NextToken();
                    if (token.Kind == Syntaxkind.EndOfFileToken)
                        break;
                    Console.WriteLine($"{token.Kind}: '{token.Text}'");
                    if (token.Value != null)
                        Console.Write($" {token.Value}");

                    Console.WriteLine();
                }
            }
        }
    }

    enum Syntaxkind
    {
        NumberToken,
        WhitespaceToken,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        OpenParenthesisToken,
        CloseParenthesisToken,
        BadToken,
        EndOfFileToken,
    }
    class SyntaxToken
    {
        public SyntaxToken(Syntaxkind kind, int position, String text, object value)
        {
            Kind = kind;
            Position = position;
            Text = text;
            Value = value;
        }

        public Syntaxkind Kind { get; }
        public int Position { get; }
        public string Text { get; }
        public object Value { get; }
    }
    class Lexer
    {
        private readonly string _text;
        private int _position;
        public Lexer(String text)
        {
            _text = text;
        }

        private char Current
        {
            get
            {
                if (_position >= _text.Length)
                    return '\0';
                return _text[_position];
            }
        }

        private void Next()
        {
            _position++;
        }
        public SyntaxToken NextToken()
        {
            //? what we can detect with no.
            // <numbers>
            // + - * / ( )
            // <whitspaces>

            if (_position >= _text.Length)
                return new SyntaxToken(Syntaxkind.EndOfFileToken, _position, "\0", null);

            if (char.IsDigit(Current))
            {
                var start = _position;

                while (char.IsDigit(Current))
                    Next();

                var length = _position - start;
                var text = _text.Substring(start, length);
                int.TryParse(text, out var value);
                return new SyntaxToken(Syntaxkind.NumberToken, start, text, value);
            }

            if (char.IsWhiteSpace(Current))
            {
                var start = _position;

                while (char.IsWhiteSpace(Current))
                    Next();

                var length = _position - start;
                var text = _text.Substring(start, length);
                int.TryParse(text, out var value);
                return new SyntaxToken(Syntaxkind.WhitespaceToken, start, text, null);
            }

            if (Current == '+')
                return new SyntaxToken(Syntaxkind.PlusToken, _position++, "+", null);
            if (Current == '-')
                return new SyntaxToken(Syntaxkind.MinusToken, _position++, "-", null);
            if (Current == '*')
                return new SyntaxToken(Syntaxkind.StarToken, _position++, "*", null);
            if (Current == '/')
                return new SyntaxToken(Syntaxkind.SlashToken, _position++, "/", null);
            if (Current == '(')
                return new SyntaxToken(Syntaxkind.OpenParenthesisToken, _position++, "(", null);
            if (Current == ')')
                return new SyntaxToken(Syntaxkind.CloseParenthesisToken, _position++, ")", null);

            return new SyntaxToken(Syntaxkind.BadToken, _position++, _text.Substring(_position - 1, 1), null);
        }
    }


    class Parser
    {
        public Parser(string text)
        {
            
        }
    }
}


