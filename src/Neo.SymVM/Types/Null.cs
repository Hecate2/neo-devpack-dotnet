// Copyright (C) 2015-2024 The Neo Project.
//
// Null.cs file belongs to the neo project and is free
// software distributed under the MIT software license, see the
// accompanying file LICENSE in the main directory of the
// repository or http://www.opensource.org/licenses/mit-license.php
// for more details.
//
// Redistribution and use in source and binary forms with or without
// modifications are permitted.

using System;
using System.Diagnostics.CodeAnalysis;

namespace Neo.SymVM.Types
{
    /// <summary>
    /// Represents <see langword="null"/> in the VM.
    /// </summary>
    public class Null : SymStackItem
    {
        public new StackItemType Type => StackItemType.Any;

        internal Null() { }

        public override SymStackItem ConvertTo(StackItemType type)
        {
            if (type == StackItemType.Any || !Enum.IsDefined(typeof(StackItemType), type))
                throw new InvalidCastException($"Type can't be converted to StackItemType: {type}");
            return this;
        }

        public new SymStackItem Equals(SymStackItem? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is UnknownStackItem)
                return new UnknownStackItem(type: StackItemType.Boolean);
            return other is Null;
        }

        public new bool GetBoolean()
        {
            return false;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        [return: MaybeNull]
        public override T GetInterface<T>()
        {
            return default;
        }

        public override string? GetString()
        {
            return null;
        }

        public override string ToString()
        {
            return "NULL";
        }
    }
}
