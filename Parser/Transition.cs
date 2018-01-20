

namespace FTCRegex.Parser
{
    /* Represents a Transition, that contains a from-state, a symbol, and a target-state.
     * Used in a list to represent a Transition-Function.
     * */
    public class Transition
    {
        public int TransitionId { get; set; }
        public State FromId { get; set; }
        public State From { get; set; }

        public State TargetId { get; set; }
        public State Target { get; set; }
        
        public char Symbol { get; set; }

    }
}