using Neo.SymVM.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Neo.SymVM
{
    /// <summary>
    /// Represents the evaluation stack in the VM.
    /// </summary>
    public sealed class SymEvalStack : IReadOnlyList<SymStackItem>
    {
        private readonly List<SymStackItem> innerList = new List<SymStackItem>();

        internal SymEvalStack() { }

        /// <summary>
        /// Gets the number of items on the stack.
        /// </summary>
        public int Count => innerList.Count;

        internal void Clear()
        {
            innerList.Clear();
        }

        internal void CopyTo(SymEvalStack stack, int count = -1)
        {
            if (count < -1 || count > innerList.Count)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (count == 0) return;
            if (count == -1 || count == innerList.Count)
                stack.innerList.AddRange(innerList);
            else
                stack.innerList.AddRange(innerList.Skip(innerList.Count - count));
        }

        public IEnumerator<SymStackItem> GetEnumerator()
        {
            return innerList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return innerList.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void Insert(int index, SymStackItem item)
        {
            if (index > innerList.Count) throw new InvalidOperationException($"Insert out of bounds: {index}/{innerList.Count}");
            innerList.Insert(innerList.Count - index, item);
        }

        internal void MoveTo(SymEvalStack stack, int count = -1)
        {
            if (count == 0) return;
            CopyTo(stack, count);
            if (count == -1 || count == innerList.Count)
                innerList.Clear();
            else
                innerList.RemoveRange(innerList.Count - count, count);
        }

        /// <summary>
        /// Returns the item at the specified index from the top of the stack without removing it.
        /// </summary>
        /// <param name="index">The index of the object from the top of the stack.</param>
        /// <returns>The item at the specified index.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SymStackItem Peek(int index = 0)
        {
            if (index >= innerList.Count) throw new InvalidOperationException($"Peek out of bounds: {index}/{innerList.Count}");
            if (index < 0)
            {
                index += innerList.Count;
                if (index < 0) throw new InvalidOperationException($"Peek out of bounds: {index}/{innerList.Count}");
            }
            return innerList[innerList.Count - index - 1];
        }

        SymStackItem IReadOnlyList<SymStackItem>.this[int index] => Peek(index);

        /// <summary>
        /// Pushes an item onto the top of the stack.
        /// </summary>
        /// <param name="item">The item to be pushed.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Push(SymStackItem item)
        {
            innerList.Add(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void Reverse(int n)
        {
            if (n < 0 || n > innerList.Count)
                throw new ArgumentOutOfRangeException(nameof(n));
            if (n <= 1) return;
            innerList.Reverse(innerList.Count - n, n);
        }

        /// <summary>
        /// Removes and returns the item at the top of the stack.
        /// </summary>
        /// <returns>The item removed from the top of the stack.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SymStackItem Pop()
        {
            return Remove<SymStackItem>(0);
        }

        /// <summary>
        /// Removes and returns the item at the top of the stack and convert it to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to convert to.</typeparam>
        /// <returns>The item removed from the top of the stack.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Pop<T>() where T : SymStackItem
        {
            return Remove<T>(0);
        }

        internal T Remove<T>(int index) where T : SymStackItem
        {
            if (index >= innerList.Count)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (index < 0)
            {
                index += innerList.Count;
                if (index < 0)
                    throw new ArgumentOutOfRangeException(nameof(index));
            }
            index = innerList.Count - index - 1;
            if (!(innerList[index] is T item))
                throw new InvalidCastException($"The item can't be casted to type {typeof(T)}");
            innerList.RemoveAt(index);
            return item;
        }

        public override string ToString()
        {
            return $"[{string.Join(", ", innerList.Select(p => $"{p.Type}({p})"))}]";
        }
    }
}
