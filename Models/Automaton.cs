using System.Collections.Generic;

namespace FTCRegex.Models
{
    /* Represents a Automaton, that contains an Alphabet, a set of states, a set of final states. (with transition function).
     * Used to recognize tags, accepting/rejecting inputs.
     * */
    public class Automaton
    {
        public List<State> States { get; set; }
        public List<State> FinalStates { get; set; }
        public State InitialState { get; set; }
        public List<char> Alphabet { get; set; }

        public bool IsFinal(State s)
        {
            return FinalStates.Contains(s);
        }

        public bool IsInitial(State s)
        {
            return this.InitialState.Equals(s);
        }
    }
}