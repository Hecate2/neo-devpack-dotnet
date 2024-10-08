// Copyright (C) 2015-2024 The Neo Project.
//
// JumpTable.Splice.cs file belongs to the neo project and is free
// software distributed under the MIT software license, see the
// accompanying file LICENSE in the main directory of the
// repository or http://www.opensource.org/licenses/mit-license.php
// for more details.
//
// Redistribution and use in source and binary forms with or without
// modifications are permitted.

using System;
using System.Runtime.CompilerServices;

namespace Neo.SymVM
{
    /// <summary>
    /// Partial class representing a jump table for executing specific operations related to string manipulation.
    /// </summary>
    public partial class JumpTable
    {
        /// <summary>
        /// Creates a new buffer with the specified length and pushes it onto the evaluation stack.
        /// <see cref="OpCode.NEWBUFFER"/>
        /// </summary>
        /// <param name="engine">The execution engine.</param>
        /// <param name="instruction">The instruction being executed.</param>
        /// <remarks>Pop 1, Push 1</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void NewBuffer(SymEngine engine, SymInstruction instruction)
        {
            int length = (int)engine.Pop().GetInteger();
            engine.Push(new Types.Buffer(length));
        }

        /// <summary>
        /// Copies a specified number of bytes from one buffer to another buffer.
        /// <see cref="OpCode.MEMCPY"/>
        /// </summary>
        /// <param name="engine">The execution engine.</param>
        /// <param name="instruction">The instruction being executed.</param>
        /// <remarks>Pop 5, Push 0</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void Memcpy(SymEngine engine, SymInstruction instruction)
        {
            int count = (int)engine.Pop().GetInteger();
            if (count < 0)
                throw new InvalidOperationException($"The value {count} is out of range.");
            int si = (int)engine.Pop().GetInteger();
            if (si < 0)
                throw new InvalidOperationException($"The value {si} is out of range.");
            ReadOnlySpan<byte> src = engine.Pop().GetSpan();
            if (checked(si + count) > src.Length)
                throw new InvalidOperationException($"The value {count} is out of range.");
            int di = (int)engine.Pop().GetInteger();
            if (di < 0)
                throw new InvalidOperationException($"The value {di} is out of range.");
            Types.Buffer dst = engine.Pop<Types.Buffer>();
            if (checked(di + count) > dst.Size)
                throw new InvalidOperationException($"The value {count} is out of range.");
            // TODO: check if we can optimize the memcpy by using peek instead of  dup then pop
            src.Slice(si, count).CopyTo(dst.InnerBuffer.Span[di..]);
        }

        /// <summary>
        /// Concatenates two buffers and pushes the result onto the evaluation stack.
        /// <see cref="OpCode.CAT"/>
        /// </summary>
        /// <param name="engine">The execution engine.</param>
        /// <param name="instruction">The instruction being executed.</param>
        /// <remarks>Pop 2, Push 1</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void Cat(SymEngine engine, SymInstruction instruction)
        {
            var x2 = engine.Pop().GetSpan();
            var x1 = engine.Pop().GetSpan();
            int length = x1.Length + x2.Length;
            Types.Buffer result = new Types.Buffer(length, false);
            x1.CopyTo(result.InnerBuffer.Span);
            x2.CopyTo(result.InnerBuffer.Span[x1.Length..]);
            engine.Push(result);
        }

        /// <summary>
        /// Extracts a substring from the specified buffer and pushes it onto the evaluation stack.
        /// <see cref="OpCode.CAT"/>
        /// </summary>
        /// <param name="engine">The execution engine.</param>
        /// <param name="instruction">The instruction being executed.</param>
        /// <remarks>Pop 3, Push 1</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void SubStr(SymEngine engine, SymInstruction instruction)
        {
            int count = (int)engine.Pop().GetInteger();
            if (count < 0)
                throw new InvalidOperationException($"The value {count} is out of range.");
            int index = (int)engine.Pop().GetInteger();
            if (index < 0)
                throw new InvalidOperationException($"The value {index} is out of range.");
            var x = engine.Pop().GetSpan();
            if (count > x.Length)
                throw new InvalidOperationException($"The value of count {count} is out of range.");
            if (index >= x.Length)
                throw new InvalidOperationException($"The value of index {index} is out of range.");
            if (checked(index + count) > x.Length)
                throw new InvalidOperationException($"The value of index + count {index + count} is out of range.");
            Types.Buffer result = new Types.Buffer(count, false);
            x.Slice(index, count).CopyTo(result.InnerBuffer.Span);
            engine.Push(result);
        }

        /// <summary>
        /// Extracts a specified number of characters from the left side of the buffer and pushes them onto the evaluation stack.
        /// <see cref="OpCode.LEFT"/>
        /// </summary>
        /// <param name="engine">The execution engine.</param>
        /// <param name="instruction">The instruction being executed.</param>
        /// <remarks>Pop 2, Push 1</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void Left(SymEngine engine, SymInstruction instruction)
        {
            int count = (int)engine.Pop().GetInteger();
            if (count < 0)
                throw new InvalidOperationException($"The value {count} is out of range.");
            var x = engine.Pop().GetSpan();
            if (count > x.Length)
                throw new InvalidOperationException($"The value {count} is out of range.");
            Types.Buffer result = new Types.Buffer(count, false);
            x[..count].CopyTo(result.InnerBuffer.Span);
            engine.Push(result);
        }

        /// <summary>
        /// Extracts a specified number of characters from the right side of the buffer and pushes them onto the evaluation stack.
        /// <see cref="OpCode.RIGHT"/>
        /// </summary>
        /// <param name="engine">The execution engine.</param>
        /// <param name="instruction">The instruction being executed.</param>
        /// <remarks>Pop 2, Push 1</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void Right(SymEngine engine, SymInstruction instruction)
        {
            int count = (int)engine.Pop().GetInteger();
            if (count < 0)
                throw new InvalidOperationException($"The value {count} is out of range.");
            var x = engine.Pop().GetSpan();
            if (count > x.Length)
                throw new InvalidOperationException($"The value {count} is out of range.");
            Types.Buffer result = new Types.Buffer(count, false);
            x[^count..^0].CopyTo(result.InnerBuffer.Span);
            engine.Push(result);
        }
    }
}
