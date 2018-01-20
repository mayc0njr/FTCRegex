namespace FTCRegex.Parser
{
    
    /* Represents an object that can be stacked on the SymbolStack.
     * This can be a Symbol, a Sequence of Symbols, an Operator, or an Sequence of Symbols and operators.
     * Override Equals and ToString is required to this interface works well with Sequence.
     * */
    public interface IStackable
    {

    }
}