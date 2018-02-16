

namespace FTCRegex.Parser
{
    /* Represents a single symbol that can be Stacked into a SymbolStack.
     * Provides the basic function to compare symbols.
     * */
    public class Symbol : IStackable
    {
        public string Value { get; }

        public Symbol(string value)
        {
            Value = value;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //
            
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            // TODO: write your implementation of Equals() here
            
            return Equals(obj as Symbol);
        }

        public bool Equals(Symbol s)
        {
            return Value.Equals(s.Value);
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value;
        }
    }
}