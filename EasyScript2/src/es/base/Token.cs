namespace Easily.ES {

    internal enum TokenType {

        Comment, Operator, String, Word, Number, Keyword

    }

	/// <summary>
	/// @author Easily
	/// </summary>
	internal class Token {

	    public string Value { get; set; }
	    public TokenType Type { get; set; }

	    public override string ToString() {
	        return $"{nameof(Value)}: {Value}, {nameof(Type)}: {Type}";
	    }

	}

}