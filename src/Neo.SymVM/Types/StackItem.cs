// Copyright (C) 2015-2024 The Neo Project.
//
// StackItem.cs file belongs to the neo project and is free
// software distributed under the MIT software license, see the
// accompanying file LICENSE in the main directory of the
// repository or http://www.opensource.org/licenses/mit-license.php
// for more details.
//
// Redistribution and use in source and binary forms with or without
// modifications are permitted.

#pragma warning disable CS0659

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Neo.SymVM.Types
{
    /// <summary>
    /// The base class for all types in the VM.
    /// </summary>
    public abstract class SymStackItem : IEquatable<SymStackItem>
    {
        [ThreadStatic]
        private static Boolean? tls_true = null;

        /// <summary>
        /// Represents <see langword="true"/> in the VM.
        /// </summary>
        public static Boolean True
        {
            get
            {
                tls_true ??= new Boolean(true);
                return tls_true;
            }
        }

        [ThreadStatic]
        private static Boolean? tls_false = null;

        /// <summary>
        /// Represents <see langword="false"/> in the VM.
        /// </summary>
        public static Boolean False
        {
            get
            {
                tls_false ??= new Boolean(false);
                return tls_false;
            }
        }

        [ThreadStatic]
        private static Null? tls_null = null;

        /// <summary>
        /// Represents <see langword="null"/> in the VM.
        /// </summary>
        public static SymStackItem Null
        {
            get
            {
                tls_null ??= new Types.Null();
                return tls_null;
            }
        }

        /// <summary>
        /// Indicates whether the object is <see cref="Null"/>.
        /// </summary>
        public bool IsNull => this is Null;

        /// <summary>
        /// The type of this VM object.
        /// </summary>
        public abstract StackItemType Type { get; }

        /// <summary>
        /// Convert the VM object to the specified type.
        /// </summary>
        /// <param name="type">The type to be converted to.</param>
        /// <returns>The converted object.</returns>
        public virtual SymStackItem ConvertTo(StackItemType type)
        {
            if (type == Type) return this;
            if (type == StackItemType.Boolean) return GetBoolean();
            throw new InvalidCastException();
        }

        internal virtual void Cleanup()
        {
        }

        /// <summary>
        /// Copy the object and all its children.
        /// </summary>
        /// <returns>The copied object.</returns>
        public SymStackItem DeepCopy(bool asImmutable = false)
        {
#if NET5_0_OR_GREATER
            return DeepCopy(new Dictionary<SymStackItem, SymStackItem>(ReferenceEqualityComparer.Instance), asImmutable);
#else
            return DeepCopy(new Dictionary<SymStackItem, SymStackItem>(Neo.VM.ReferenceEqualityComparer.Instance), asImmutable);
#endif
        }

        internal virtual SymStackItem DeepCopy(Dictionary<SymStackItem, SymStackItem> refMap, bool asImmutable)
        {
            return this;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is SymStackItem item) return Equals(item);
            return false;
        }

        public virtual bool Equals(SymStackItem? other)
        {
            return ReferenceEquals(this, other);
        }

        /// <summary>
        /// Wrap the specified <see cref="object"/> and return an <see cref="InteropInterface"/> containing the <see cref="object"/>.
        /// </summary>
        /// <param name="value">The wrapped <see cref="object"/>.</param>
        /// <returns></returns>
        public static SymStackItem FromInterface(object? value)
        {
            if (value is null) return Null;
            return new InteropInterface(value);
        }

        /// <summary>
        /// Get the boolean value represented by the VM object.
        /// </summary>
        /// <returns>The boolean value represented by the VM object.</returns>
        public abstract bool GetBoolean();

        /// <summary>
        /// Get the integer value represented by the VM object.
        /// </summary>
        /// <returns>The integer value represented by the VM object.</returns>
        public virtual BigInteger GetInteger()
        {
            throw new InvalidCastException();
        }

        /// <summary>
        /// Get the <see cref="object"/> wrapped by this interface and convert it to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to convert to.</typeparam>
        /// <returns>The wrapped <see cref="object"/>.</returns>
        [return: MaybeNull]
        public virtual T GetInterface<T>() where T : notnull
        {
            throw new InvalidCastException();
        }

        /// <summary>
        /// Get the readonly span used to read the VM object data.
        /// </summary>
        /// <returns></returns>
        public virtual ReadOnlySpan<byte> GetSpan()
        {
            throw new InvalidCastException();
        }

        /// <summary>
        /// Get the <see cref="string"/> value represented by the VM object.
        /// </summary>
        /// <returns>The <see cref="string"/> value represented by the VM object.</returns>
        public virtual string? GetString()
        {
            return Neo.VM.Utility.StrictUTF8.GetString(GetSpan());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator SymStackItem(sbyte value)
        {
            return (Integer)value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator SymStackItem(byte value)
        {
            return (Integer)value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator SymStackItem(short value)
        {
            return (Integer)value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator SymStackItem(ushort value)
        {
            return (Integer)value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator SymStackItem(int value)
        {
            return (Integer)value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator SymStackItem(uint value)
        {
            return (Integer)value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator SymStackItem(long value)
        {
            return (Integer)value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator SymStackItem(ulong value)
        {
            return (Integer)value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator SymStackItem(BigInteger value)
        {
            return (Integer)value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator SymStackItem(bool value)
        {
            return value ? True : False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator SymStackItem(byte[] value)
        {
            return (ByteString)value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator SymStackItem(ReadOnlyMemory<byte> value)
        {
            return (ByteString)value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator SymStackItem(string value)
        {
            return (ByteString)value;
        }
    }
}
