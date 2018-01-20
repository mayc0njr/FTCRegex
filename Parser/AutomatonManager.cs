using System.Collections.Generic;

namespace FTCRegex.Parser
{
    /* Represents a Automaton, that contains an Alphabet, a set of states, a set of final states. (with transition function).
     * Used to recognize tags, accepting/rejecting inputs.
     * */
    public class AutomatonManager
    {

        //Generates a Automaton thar processes languages of a single symbol.
        public Automaton Unitary(char symbol)
        {
            Automaton a = new Automaton(){
                // States = new List<int>(){ 0, 1 },
                // InitialState = 0,
                // FinalStates = new List<int>(){ 1 },
                // Alphabet = new List<char>(){ symbol },
                // TransitionMatrix = new List<Dictionary<char, int>>() {new Dictionary<char, int>(){ { symbol, 1 } } }
            };
            return a;
        }
    }
}