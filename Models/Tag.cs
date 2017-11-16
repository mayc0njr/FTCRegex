

namespace MayconJr.StringParser.Models
{
    public class Tag
    {
        public const char ESCAPE_CHAR = '\\';
        public const int WILDCARD = 0;
        public const int DOT = 1;
        public const int PLUS = 2;
        public const int NEWLINE = 3;
        public const int BAR = 4;
        public const int LAMBDA = 5;
        public static readonly char[] ESCAPE = {'*', '.', '+', 'n', '\\', 'l'};
        public const string INVALID_NAME = "Tag name invalid.";
        public const string INVALID_DEFINITION = "Tag definition invalid.";
        public int TagId { get; set; }
        public string Name { get; set; }
        public string Definition { get; set; }
        public Automaton Automaton { get; set; }

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
            return Equals (obj as Tag);
        }

        public bool Equals(Tag t)
        {
            return Name == t.Name || Definition == t.Definition;
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Definition.GetHashCode();
        }
    }
}