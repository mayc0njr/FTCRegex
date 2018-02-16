using System.Collections.Generic;


namespace FTCRegex.Parser
{
    public class SymbolStack
    {
        private Stack<IStackable> stack;
        public Sequence Expression { get; }
        private bool failed;
        public SymbolStack()
        {
            stack = new Stack<IStackable>();
            failed = false;
            Expression = new Sequence();
        }

        public void Clear()
        {
            failed = false;
            stack.Clear();
        }
        public void Add(IStackable ist)
        {
            // if(failed)
            //     return;
            Expression.Add(ist);
            if(ist is Operator)
            {
                var op = ist as Operator;
                if(stack.Count >= op.Operands)
                {
                    Sequence sq = new Sequence();
                    for(var x=0 ; x < op.Operands ; x++)
                    {
                        sq.Add(stack.Pop());
                    }
                    sq.Add(op);
                    stack.Push(sq);
                }else
                {
                    failed = true;
                }
            }else
            {
                stack.Push(ist);
            }
        }
        public bool Empty()
        {
            return stack.Count == 0;
        }

        public bool Verify
        {
            get
            {
                return !failed && stack.Count <= 1;
            }
        }
    }
}