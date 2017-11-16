

namespace MayconJr.StringParser.Models
{
    /* Represents a Transition, that contains a symbol, and a target-state.
     * Used in a list to represent a Transition-Function.
     * */
    public class Transition
    {
        public State Target { get; set; }
        public char Symbol { get; set; }


        public int StateId { get; set; }
        public State State { get; set; }
    }
}