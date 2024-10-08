// Copyright (C) 2015-2024 The Neo Project.
//
// Struct.cs file belongs to the neo project and is free
// software distributed under the MIT software license, see the
// accompanying file LICENSE in the main directory of the
// repository or http://www.opensource.org/licenses/mit-license.php
// for more details.
//
// Redistribution and use in source and binary forms with or without
// modifications are permitted.

using System;
using System.Collections.Generic;

namespace Neo.SymVM.Types
{
    /// <summary>
    /// Represents a structure in the VM.
    /// </summary>
    public class Struct : Array
    {
        public new StackItemType Type => StackItemType.Struct;

        /// <summary>
        /// Create a structure with the specified fields. And make the structure use the specified <see cref="ReferenceCounter"/>.
        /// </summary>
        /// <param name="referenceCounter">The <see cref="ReferenceCounter"/> to be used by this structure.</param>
        /// <param name="fields">The fields to be included in the structure.</param>
        public Struct(IEnumerable<SymStackItem>? fields = null)
            : base(fields)
        {
        }

        /// <summary>
        /// Create a new structure with the same content as this structure. All nested structures will be copied by value.
        /// </summary>
        /// <param name="limits">Execution engine limits</param>
        /// <returns>The copied structure.</returns>
        public Struct Clone()
        {
            Struct result = new Struct();
            Queue<Struct> queue = new Queue<Struct>();
            queue.Enqueue(result);
            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                Struct a = queue.Dequeue();
                Struct b = queue.Dequeue();
                foreach (SymStackItem item in b)
                {
                    if (item is Struct sb)
                    {
                        Struct sa = new Struct();
                        a.Add(sa);
                        queue.Enqueue(sa);
                        queue.Enqueue(sb);
                    }
                    else
                    {
                        a.Add(item);
                    }
                }
            }
            return result;
        }

        public override SymStackItem ConvertTo(StackItemType type)
        {
            if (type == StackItemType.Array)
                return new Array(new List<SymStackItem>(_array));
            return base.ConvertTo(type);
        }

        public override bool Equals(SymStackItem? other)
        {
            if (!(other is Struct s)) return false;
            Stack<SymStackItem> stack1 = new Stack<SymStackItem>();
            Stack<SymStackItem> stack2 = new Stack<SymStackItem>();
            stack1.Push(this);
            stack2.Push(s);
            while (stack1.Count > 0)
            {
                SymStackItem a = stack1.Pop();
                SymStackItem b = stack2.Pop();
                if (a is ByteString byteString)
                {
                    if (!byteString.Equals(b)) return false;
                }
                else
                {
                    if (a is Struct sa)
                    {
                        if (ReferenceEquals(a, b)) continue;
                        if (!(b is Struct sb)) return false;
                        if (sa.Count != sb.Count) return false;
                        foreach (SymStackItem item in sa)
                            stack1.Push(item);
                        foreach (SymStackItem item in sb)
                            stack2.Push(item);
                    }
                    else
                    {
                        if (!a.Equals(b)) return false;
                    }
                }
            }
            return true;
        }
    }
}
