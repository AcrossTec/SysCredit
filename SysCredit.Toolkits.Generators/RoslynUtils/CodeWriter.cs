using System.CodeDom.Compiler;

/// <inheritdoc />
public sealed class CodeWriter : IndentedTextWriter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="StringWriter"></param>
    /// <param name="BaseIndent"></param>
    public CodeWriter(StringWriter StringWriter, int BaseIndent) : base(StringWriter)
    {
        Indent = BaseIndent;
    }

    /// <summary>
    /// 
    /// </summary>
    public void StartBlock()
    {
        this.WriteLine("{");
        this.Indent++;
    }

    /// <summary>
    /// 
    /// </summary>
    public void EndBlock()
    {
        this.Indent--;
        this.WriteLine("}");
    }

    /// <summary>
    /// 
    /// </summary>
    public void EndBlockWithComma()
    {
        this.Indent--;
        this.WriteLine("},");
    }

    /// <summary>
    /// 
    /// </summary>
    public void EndBlockWithSemicolon()
    {
        this.Indent--;
        this.WriteLine("};");
    }

    /// <summary>
    ///     The IndentedTextWriter adds the indentation _after_ writing the first line of text.
    ///     This method can be used ot initialize indentation when an emit method might only emit one line
    ///     of code or when the code writer is emitting indented code as part of a larger string.
    /// </summary>
    public void InitializeIndent()
    {
        for (var I = 0; I < Indent; ++I)
        {
            Write(DefaultTabString);
        }
    }
}
