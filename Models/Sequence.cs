using System;
using System.Text;
using System.Collections.Generic;

namespace MayconJr.StringParser.Models
{
    /* Represents a sequence of symbols merged by Operator.
     * Provides basic functions to compare Sequences.
     * Provides also functions to extract the IStackable objects from the sequence.
     * Uses a Inverse Polish Notation to add operators by default.
     * */
    public class Sequence : IStackable
    {
        private List<IStackable> lista;
        public Sequence(params IStackable[] s)
        {
            lista = new List<IStackable>();
            foreach (var item in s)
            {
                lista.Add(item);
            }
        }
        public Sequence(IStackable s)
        {
            lista = new List<IStackable>();
            lista.Add(s);
        }

        public void Add(IStackable element)
        {
            lista.Add(element);
        }
        /* Adds the Kleene operator in the sequence.
         * It's the same as call Add(Operator.KLEENE).
         * */
        public void Kleene()
        {
            lista.Add(Operator.KLEENE);
        }

        /* Adds a IStackable element on the sequence, then,
         * adds the Union Operator in the sequence.
         * */
        public void Union(IStackable c)
        {
            lista.Add(c);
            lista.Add(Operator.UNION);
        }

        /* Adds a IStackable element on the sequence, then,
         * adds the Concat Operator in the sequence.
         * */
        public void Concat(IStackable c)
        {
            lista.Add(c);
            lista.Add(Operator.CONCAT);
        }

        /* Breaks a Sequence into the basic forms.
         * If an element in the sequence is an Symbol or Operator, include it in the return.
         * If an element is another sequence, breaks it recursively.
         * returns a list of elements that is not sequences.
         * */
        public List<IStackable> BreakSequence()
        {
            List<IStackable> ls = new List<IStackable>();
            foreach(var x in this.lista)
            {
                if(x is Sequence)
                {
                    var s = x as Sequence;
                    ls.AddRange(s.BreakSequence());
                    continue;
                }
                ls.Add(x);
            }
            return ls;
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
            return Equals (obj as Sequence);
        }

        public bool Equals(Sequence s)
        {
            if(this.Count() != s.Count())
                return false;
            for(var x=0 ; x < lista.Count ; x++)
            {
                if(!this[x].Equals(s[x]))
                    return false;
            }
            return true;
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        // Define the indexer to allow client code to use [] notation.
        public IStackable this[int i]
        {
            get { return lista[i]; }
        }

        /* Returns the number of elements in this sequence.
         * */
        public int Count()
        {
            return lista.Count;
        }

        public override string ToString(){
            StringBuilder sb = new StringBuilder();
            foreach(var symbol in lista)
            {
                sb.Append(symbol.ToString());
            }
            return sb.ToString();
        }
    }
}