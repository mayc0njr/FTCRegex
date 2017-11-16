


namespace FTCRegex.Models
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
        public string Name { get; }
        public int Operands { get; }
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
            return Equals (obj as Operator);
        }

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