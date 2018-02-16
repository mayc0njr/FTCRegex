


namespace FTCRegex.Parser
{
    /* Represents an operators between Stackable Elements, like Sequences or symbols.
     * Provides basic compare and represent functions.
     * Doesn't provides operators functionalities.
     * */
    public class Operator : IStackable
    {
        public static readonly Operator UNION = new Operator("Union", 2, "+");
        public static readonly Operator CONCAT = new Operator("Concat", 2, ".");
        public static readonly Operator KLEENE = new Operator("Kleene", 1, "*");
        //Name of the operator.
        public string Name { get; }
        //Count of Operands.
        public int Operands { get; }
        //Human-Readable representation.
        private string Representation { get; }

        public Operator(string name, int operands, string representation){
            Name = name;
            Operands = operands;
            Representation = representation;
        }

        public override string ToString()
        {
            return Representation;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Equals (obj as Operator);
        }

        //Compare operator name and number of operands to determine equality.
        public bool Equals(Operator o)
        {
            return this.Name.Equals(o.Name) && this.Operands == o.Operands;
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Name.GetHashCode() << 2 + Operands;
        }
    }
}