using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTCRegex.Parser
{
    /* Represents a State from an finite automaton.
     * Contains an ID, a bool indicating if is a final-state, and a "transition function" represented as a list of transitions.
     * */
    public class State
    {
        public int StateId { get; set; }

        public bool Final { get; set; }
        public bool Initial { get; set; }

        public int AutomatonId { get; set; }
        public Automaton Automaton { get; set; }
        
        [InverseProperty("From")]
        public List<Transition> FromHere { get; set; }

        [InverseProperty("Targe")]
        public List<Transition> TargetHere { get; set; }

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