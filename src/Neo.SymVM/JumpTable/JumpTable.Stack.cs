// Copyright (C) 2015-2024 The Neo Project.
//
// JumpTable.Stack.cs file belongs to the neo project and is free
// software distributed under the MIT software license, see the
// accompanying file LICENSE in the main directory of the
// repository or http://www.opensource.org/licenses/mit-license.php
// for more details.
//
// Redistribution and use in source and binary forms with or without
// modifications are permitted.

using Neo.SymVM.Types;
using System;
using System.Runtime.CompilerServices;

namespace Neo.SymVM
{
    /// <summary>
    /// Partial class for stack manipulation within a jump table in the execution engine.
    /// </summary>
    public partial class JumpTable
    {
        /// <summary>
        /// Pushes the number of stack items in the evaluation stack onto the stack.
        /// <see cref="OpCode.DEPTH"/>
        /// </summary>
        /// <param name="engine">The execution engine.</param>
        /// <param name="instruction">The instruction being executed.</param>
        /// <remarks>Pop 0, Push 1</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void Depth(SymEngine engine, SymInstruction instruction)
        {
            engine.Push(engine.CurrentContext!.SymEvalStack.Count);
        }

        /// <summary>
        /// Removes the top item from the evaluation stack.
        /// <see cref="OpCode.DROP"/>
        /// </summary>
        /// <param name="engine">The execution engine.</param>
        /// <param name="instruction">The instruction being executed.</param>
        /// <remarks>Pop 1, Push 0</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void Drop(SymEngine engine, SymInstruction instruction)
        {
            engine.Pop();
        }

        /// <summary>
        /// Removes the second-to-top stack item.
        /// <see cref="OpCode.NIP"/>
        /// </summary>
        /// <param name="engine">The execution engine.</param>
        /// <param name="instruction">The instruction being executed.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void Nip(SymEngine engine, SymInstruction instruction)
        {
            engine.CurrentContext!.SymEvalStack.Remove<SymStackItem>(1);
        }

        /// <summary>
        /// Removes the n-th item from the top of the evaluation stack.
        /// <see cref="OpCode.XDROP"/>
        /// </summary>
        /// <param name="engine">The execution engine.</param>
        /// <param name="instruction">The instruction being executed.</param>
        /// <remarks>Pop 1, Push 0</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void XDrop(SymEngine engine, SymInstruction instruction)
        {
            var n = (int)engine.Pop().GetInteger();
            if (n < 0)
                throw new InvalidOperationException($"The negative value {n} is invalid for OpCode.{instruction.OpCode}.");
            engine.CurrentContext!.SymEvalStack.Remove<SymStackItem>(n);
        }

        /// <summary>
        /// Clears all items from the evaluation stack.
        /// <see cref="OpCode.CLEAR"/>
        /// </summary>
        /// <param name="engine">The execution engine.</param>
        /// <param name="instruction">The instruction being executed.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void Clear(SymEngine engine, SymInstruction instruction)
        {
            engine.CurrentContext!.SymEvalStack.Clear();
        }

        /// <summary>
        /// Duplicates the item on the top of the evaluation stack.
        /// <see cref="OpCode.DUP"/>
        /// </summary>
        /// <param name="engine">The execution engine.</param>
        /// <param name="instruction">The instruction being executed.</param>
        /// <remarks>Pop 0, Push 1</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void Dup(SymEngine engine, SymInstruction instruction)
        {
            engine.Push(engine.Peek());
        }

        /// <summary>
        /// Copies the second item from the top of the evaluation stack and pushes the copy onto the stack.
        /// <see cref="OpCode.OVER"/>
        /// </summary>
        /// <param name="engine">The execution engine.</param>
        /// <param name="instruction">The instruction being executed.</param>
        /// <remarks>Pop 0, Push 1</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void Over(SymEngine engine, SymInstruction instruction)
        {
            engine.Push(engine.Peek(1));
        }

        /// <summary>
        /// Copies the nth item from the top of the evaluation stack and pushes the copy onto the stack.
        /// <see cref="OpCode.PICK"/>
        /// </summary>
        /// <param name="engine">The execution engine.</param>
        /// <param name="instruction">The instruction being executed.</param>
        /// <remarks>Pop 1, Push 1</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void Pick(SymEngine engine, SymInstruction instruction)
        {
            var n = (int)engine.Pop().GetInteger();
            if (n < 0)
                throw new InvalidOperationException($"The negative value {n} is invalid for OpCode.{instruction.OpCode}.");
            engine.Push(engine.Peek(n));
        }

        /// <summary>
        /// Copies the top item on the evaluation stack and inserts the copy between the first and second items.
        /// <see cref="OpCode.TUCK"/>
        /// </summary>
        /// <param name="engine">The execution engine.</param>
        /// <param name="instruction">The instruction being executed.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void Tuck(SymEngine engine, SymInstruction instruction)
        {
            engine.CurrentContext!.SymEvalStack.Insert(2, engine.Peek());
        }

        /// <summary>
        /// Swaps the top two items on the evaluation stack.
        /// <see cref="OpCode.SWAP"/>
        /// </summary>
        /// <param name="engine">The execution engine.</param>
        /// <param name="instruction">The instruction being executed.</param>
        /// <remarks>Pop 0, Push 1</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void Swap(SymEngine engine, SymInstruction instruction)
        {
            var x = engine.CurrentContext!.SymEvalStack.Remove<SymStackItem>(1);
            engine.Push(x);
        }

        /// <summary>
        /// Left rotates the top three items on the evaluation stack.
        /// <see cref="OpCode.ROT"/>
        /// </summary>
        /// <param name="engine">The execution engine.</param>
        /// <param name="instruction">The instruction being executed.</param>
        /// <remarks>Pop 0, Push 1</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void Rot(SymEngine engine, SymInstruction instruction)
        {
            var x = engine.CurrentContext!.SymEvalStack.Remove<SymStackItem>(2);
            engine.Push(x);
        }

        /// <summary>
        /// The item n back in the stack is moved to the top.
        /// <see cref="OpCode.ROLL"/>
        /// </summary>
        /// <param name="engine">The execution engine.</param>
        /// <param name="instruction">The instruction being executed.</param>
        /// <remarks>Pop 1, Push 1</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void Roll(SymEngine engine, SymInstruction instruction)
        {
            var n = (int)engine.Pop().GetInteger();
            if (n < 0)
                throw new InvalidOperationException($"The negative value {n} is invalid for OpCode.{instruction.OpCode}.");
            if (n == 0) return;
            var x = engine.CurrentContext!.SymEvalStack.Remove<SymStackItem>(n);
            engine.Push(x);
        }

        /// <summary>
        /// Reverses the order of the top 3 items on the evaluation stack.
        /// <see cref="OpCode.REVERSE3"/>
        /// </summary>
        /// <param name="engine">The execution engine.</param>
        /// <param name="instruction">The instruction being executed.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void Reverse3(SymEngine engine, SymInstruction instruction)
        {
            engine.CurrentContext!.SymEvalStack.Reverse(3);
        }

        /// <summary>
        /// Reverses the order of the top 4 items on the evaluation stack.
        /// <see cref="OpCode.REVERSE4"/>
        /// </summary>
        /// <param name="engine">The execution engine.</param>
        /// <param name="instruction">The instruction being executed.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void Reverse4(SymEngine engine, SymInstruction instruction)
        {
            engine.CurrentContext!.SymEvalStack.Reverse(4);
        }

        /// <summary>
        /// Reverses the order of the top n items on the evaluation stack.
        /// <see cref="OpCode.REVERSEN"/>
        /// </summary>
        /// <param name="engine">The execution engine.</param>
        /// <param name="instruction">The instruction being executed.</param>
        /// <remarks>Pop 1, Push 0</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void ReverseN(SymEngine engine, SymInstruction instruction)
        {
            var n = (int)engine.Pop().GetInteger();
            engine.CurrentContext!.SymEvalStack.Reverse(n);
        }
    }
}
