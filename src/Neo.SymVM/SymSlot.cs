using Neo.SymVM.Types;
using System.Collections;
using System.Collections.Generic;

namespace Neo.SymVM
{
    /// <summary>
    /// Used to store local variables, arguments and static fields in the VM.
    /// </summary>
    public class SymSlot : IReadOnlyList<SymStackItem>
    {
        private readonly SymStackItem[] items;

        /// <summary>
        /// Gets the item at the specified index in the slot.
        /// </summary>
        /// <param name="index">The zero-based index of the item to get.</param>
        /// <returns>The item at the specified index in the slot.</returns>
        public SymStackItem this[int index]
        {
            get
            {
                return items[index];
            }
            internal set
            {
                ref var oldValue = ref items[index];
                oldValue = value;
            }
        }

        /// <summary>
        /// Gets the number of items in the slot.
        /// </summary>
        public int Count => items.Length;

        /// <summary>
        /// Creates a slot containing the specified items.
        /// </summary>
        /// <param name="items">The items to be contained.</param>
        /// <param name="referenceCounter">The reference counter to be used.</param>
        public SymSlot(SymStackItem[] items)
        {
            this.items = items;
        }

        /// <summary>
        /// Create a slot of the specified size.
        /// </summary>
        /// <param name="count">Indicates the number of items contained in the slot.</param>
        /// <param name="referenceCounter">The reference counter to be used.</param>
        public SymSlot(int count)
        {
            items = new SymStackItem[count];
            System.Array.Fill(items, SymStackItem.Null);
        }

        IEnumerator<SymStackItem> IEnumerable<SymStackItem>.GetEnumerator()
        {
            foreach (SymStackItem item in items) yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
}
