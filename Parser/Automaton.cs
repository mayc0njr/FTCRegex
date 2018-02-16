using System.Collections.Generic;
using System.Linq;

using FTCRegex.Models;

namespace FTCRegex.Parser
{
    /* Represents a Automaton, that contains an Alphabet, a set of states, a set of final states. (with transition function).
     * Used to recognize tags, accepting/rejecting inputs.
     * */
    public class Automaton
    {
        public List<State> States { get; set; }
        public List<char> Alphabet { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }

        public bool IsFinal(State s)
        {
            return s.Final;
        }
        public bool IsInitial(State s)
        {
            return s.Initial;
        }

        public IEnumerable<State> FinalStates {
            get
            {
                return States.Where(item => item.Final);
            }
        }

        public  IEnumerable<State> InitialStates(State s)
        {
                return States.Where(item => item.Initial);
        }
    }
}