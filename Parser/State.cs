using System.Collections.Generic;
using System;

namespace FTCRegex.Parser
{
    /* Represents a State from an finite automaton.
     * Contains an ID, and a "transition function" represented as a list of transitions.
     * */
    public class State
    {
        public int StateId { get; set; }
        public List<Transition> Transitions { get; set; }

        public int AutomatonId { get; set; }
        public Automaton Automaton { get; set; }

        public override bool Equals(object obj)
        {
            var st = obj as State;
            if(st == null)
                return false;
            return Equals(st);
        }

        public bool Equals(State st)
        {
            return this.StateId == st.StateId;
        }

        public override int GetHashCode()
        {
            return StateId.GetHashCode();
        }
    }
}