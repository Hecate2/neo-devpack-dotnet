using Neo.VM;
using System;
using System.Collections.Generic;

namespace Neo.SymVM
{
    partial class SymExecContext
    {
        private class SymSharedStates
        {
            public readonly Script Script;
            public readonly SymEvalStack SymEvalStack;
            public SymSlot? StaticFields;
            public readonly Dictionary<Type, object> States;

            public SymSharedStates(Script script)
            {
                Script = script;
                SymEvalStack = new SymEvalStack();
                States = new Dictionary<Type, object>();
            }
        }
    }
}
