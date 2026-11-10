using System;
using System.Collections.Generic;

namespace FrontierDispatch.Core
{
    // Assignment Phase 3. Implement as a binary min-heap.

    /// <summary>
    /// A minimum-priority queue implemented as a binary min-heap.
    ///
    /// Items with lower integer priority values should be returned before
    /// items with higher priority values.
    ///
    /// This class is intentionally incomplete in the assignment starter.
    /// </summary>
    public sealed class MinPriorityQueue<T>
    {
        // The list stores the physical representation of the binary heap.
        // Your implementation is responsible for preserving the min-heap invariant.
        private readonly List<Entry> _heap = new List<Entry>();

        /// <summary>
        /// Number of items currently stored in the queue.
        /// </summary>
        public int Count => _heap.Count;

        /// <summary>
        /// Adds an item with the supplied priority.
        /// Lower priority values are removed before higher priority values.
        /// </summary>
        public void Enqueue(T item, int priority)
        {
            // TODO: insert the new entry and restore the min-heap invariant.
            _heap.Add(new Entry(item, priority));
            SiftUp(_heap.Count - 1);
        
        }

        /// <summary>
        /// Returns the minimum-priority item without removing it.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the queue is empty.
        /// </exception>
        public T Peek()
        {
            // TODO: return the current minimum without changing the queue.
            if (_heap.Count == 0)
            {
                throw new NotImplementedException("there is nothing in the queue");

            }
            return _heap[0].Item;
        }

        /// <summary>
        /// Removes and returns the minimum-priority item.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the queue is empty.
        /// </exception>
        public T Dequeue()
        {
            // TODO: remove the minimum item and restore the min-heap invariant.
            if (_heap.Count == 0)
            {
                throw new NotImplementedException("there is nothing in the queue");
            }

            T smallest = _heap[0].Item;
            int lastIndex = _heap.Count - 1;
            _heap[0] = _heap[lastIndex];
            _heap.RemoveAt(lastIndex);

            if (_heap.Count > 0 )
            {
                SiftDown(0);
            }
            return smallest;


        }

        /// <summary>
        /// Stores one item together with the priority used by the heap.
        /// </summary>
        /// 

  private void SiftUp(int index)
        {
            while (index > 0 )
            {
                int parentIndex = (index - 1) / 2;

                if (_heap[index].Priority >= _heap[parentIndex].Priority)
                {
                    break;
                }
                Swap(index, parentIndex);
                index = parentIndex;
            }
        }

        private void SiftDown(int index)
        {
            int leftIndex = index * 2 + 1;
            int rightIndex = index * 2 + 2;
            int smallestIndex = index;

            if (leftIndex < _heap.Count && _heap[leftIndex].Priority < _heap[smallestIndex].Priority)
            {
                smallestIndex = leftIndex;
            }

            if (rightIndex < _heap.Count && _heap[rightIndex].Priority < _heap[smallestIndex].Priority)
            {
                smallestIndex = rightIndex;
            }

            if (smallestIndex == index)
            {
                return;
            }
            Swap(index, smallestIndex);
            index = smallestIndex;
        }
        private void Swap(int a, int b)
        {
            Entry temp = _heap[a];
            _heap[a] = _heap[b];
            _heap[b] = temp;
        }
        private readonly struct Entry
        {
            public Entry(T item, int priority)
            {
                Item = item;
                Priority = priority;
            }

            public T Item { get; }
            public int Priority { get; }
        }
    }
}
