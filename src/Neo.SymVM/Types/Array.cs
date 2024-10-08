// Copyright (C) 2015-2024 The Neo Project.
//
// Array.cs file belongs to the neo project and is free
// software distributed under the MIT software license, see the
// accompanying file LICENSE in the main directory of the
// repository or http://www.opensource.org/licenses/mit-license.php
// for more details.
//
// Redistribution and use in source and binary forms with or without
// modifications are permitted.

using System;
using System.Collections;
using System.Collections.Generic;

namespace Neo.SymVM.Types
{
    /// <summary>
    /// Represents an array or a complex object in the VM.
    /// </summary>
    public class Array : CompoundType, IReadOnlyList<SymStackItem>
    {
        protected readonly List<SymStackItem> _array;

        /// <summary>
        /// Get or set item in the array.
        /// </summary>
        /// <param name="index">The index of the item in the array.</param>
        /// <returns>The item at the specified index.</returns>
        public SymStackItem this[int index]
        {
            get => _array[index];
            set
            {
                if (IsReadOnly) throw new InvalidOperationException("The object is readonly.");
                _array[index] = value;
            }
        }

        /// <summary>
        /// The number of items in the array.
        /// </summary>
        public override int Count => _array.Count;
        public override IEnumerable<SymStackItem> SubItems => _array;
        public override int SubItemsCount => _array.Count;
        public new StackItemType Type => StackItemType.Array;

        /// <summary>
        /// Create an array containing the specified items. And make the array use the specified <see cref="ReferenceCounter"/>.
        /// </summary>
        /// <param name="referenceCounter">The <see cref="ReferenceCounter"/> to be used by this array.</param>
        /// <param name="items">The items to be included in the array.</param>
        public Array(IEnumerable<SymStackItem>? items = null)
            : base()
        {
            _array = items switch
            {
                null => new List<SymStackItem>(),
                List<SymStackItem> list => list,
                _ => new List<SymStackItem>(items)
            };
        }

        /// <summary>
        /// Add a new item at the end of the array.
        /// </summary>
        /// <param name="item">The item to be added.</param>
        public void Add(SymStackItem item)
        {
            if (IsReadOnly) throw new InvalidOperationException("The object is readonly.");
            _array.Add(item);
        }

        public override void Clear()
        {
            if (IsReadOnly) throw new InvalidOperationException("The object is readonly.");
            _array.Clear();
        }

        public override SymStackItem ConvertTo(StackItemType type)
        {
            if (Type == StackItemType.Array && type == StackItemType.Struct)
                return new Struct(new List<SymStackItem>(_array));
            return base.ConvertTo(type);
        }

        internal sealed override SymStackItem DeepCopy(Dictionary<SymStackItem, SymStackItem> refMap, bool asImmutable)
        {
            if (refMap.TryGetValue(this, out SymStackItem? mappedItem)) return mappedItem;
            Array result = this is Struct ? new Struct() : new Array();
            refMap.Add(this, result);
            foreach (SymStackItem item in _array)
                result.Add(item.DeepCopy(refMap, asImmutable));
            result.IsReadOnly = true;
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<SymStackItem> GetEnumerator()
        {
            return _array.GetEnumerator();
        }

        /// <summary>
        /// Remove the item at the specified index.
        /// </summary>
        /// <param name="index">The index of the item to be removed.</param>
        public void RemoveAt(int index)
        {
            if (IsReadOnly) throw new InvalidOperationException("The object is readonly.");
            _array.RemoveAt(index);
        }

        /// <summary>
        /// Reverse all items in the array.
        /// </summary>
        public void Reverse()
        {
            if (IsReadOnly) throw new InvalidOperationException("The object is readonly.");
            _array.Reverse();
        }
    }
}
