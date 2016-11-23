using System;
using System.Collections;
using System.Collections.Generic;

namespace Task2.Logic
{
    /// <summary>
    /// Represents the Generic queue.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    public class Queue<T> : IEnumerable<T>
    {
        private T[] _elements = {};
        private const int ScalingCoefficient = 2;
        private const int StartCapacity = 5;

        /// <summary>
        /// A property for a capacity.
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the capacity is less or equal than 0.
        /// </exception>
        /// </summary>
        public int Capacity
        {
            get { return _capacity; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Wrong capacity!");

                _capacity = value;
            }
        }

        /// <summary>
        /// A property for a size.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// A property for elements.
        /// <exception cref="ArgumentException">
        /// Thrown if the number of elements is less than 1.
        /// </exception>
        /// </summary>
        public T[] Elements
        {
            get { return _elements; }
            private set
            {
                if (value.Length < 1)
                    throw new ArgumentException("Wrong number of parameters!");

                _elements = new T[Capacity];
                value.CopyTo(_elements, 0);
            }
        }

        public Queue(int capacity)
        {
            Capacity = capacity;
            Size = 0;
            Elements = new T[Capacity];
        }

        public Queue(int capacity, params T[] elements)
        {
            Capacity = capacity;
            Elements = elements;
            Size = elements.Length;
        }

        public T this[int index]
        {
            get { return _elements[index]; }
            set { _elements[index] = value; }
        }

        /// <summary>
        /// Indicates whether the queue is full.
        /// </summary>
        /// <returns>Returns true or false.</returns>
        public bool IsFull() => Size == Capacity;

        /// <summary>
        /// Indicates whether the queue is empty.
        /// </summary>
        /// <returns>Returns true or false.</returns>
        public bool IsEmpty() => Size == 0;

        /// <summary>
        /// Inserts the element in the queue.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the queue is full.
        /// </exception>
        /// <param name="element">The element.</param>
        public void Push(T element)
        {
            if (IsFull())
                throw new ArgumentOutOfRangeException("Queue is full!");

            Elements[Size++] = element;
        }

        /// <summary>
        /// Gets the element from the queue.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the queue is empty.
        /// </exception>
        /// <returns>Returns the first element.</returns>
        public T Pop()
        {
            if (IsEmpty())
                throw new ArgumentOutOfRangeException("Queue is empty!");

            var firstElement = Elements[0];
            ShiftForward();
            Size--;

            return firstElement;
        }

        /// <summary>
        /// Gets the enumerator for the queue.
        /// </summary>
        /// <returns>Returns the next element.</returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => new Iterator(this);

        /// <summary>
        /// Gets the enumerator for the queue.
        /// </summary>
        /// <returns>Returns the next element.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Gets the enumerator for the queue.
        /// </summary>
        /// <returns>Returns the next element.</returns>
        public Iterator GetEnumerator() => new Iterator(this);

        /// <summary>
        /// An interator for a queue.
        /// </summary>
        public struct Iterator : IEnumerator<T>
        {
            private readonly Queue<T> _queue;
            private int _currentIndex;

            public Iterator(Queue<T> queue)
            {
                _currentIndex = -1;
                _queue = queue;
            }

            /// <summary>
            /// A property for a current element.
            /// </summary>
            public T Current
            {
                get
                {
                    if (_currentIndex == -1 || _currentIndex == _queue.Size)
                        throw new InvalidOperationException();

                    return _queue[_currentIndex];
                }
            }

            /// <summary>
            /// Resets the iterator.
            /// </summary>
            public void Reset() => _currentIndex = -1;

            /// <summary>
            /// Gets the current element.
            /// </summary>
            object IEnumerator.Current => Current;

            /// <summary>
            /// Indicates if there are more elements in the queue.
            /// </summary>
            /// <returns></returns>
            public bool MoveNext() => ++_currentIndex < _queue.Size;

            /// <summary>
            /// Disposes hte garbage.
            /// </summary>
            public void Dispose() { }
        }

        /// <summary>
        /// Shifts the array of elements one position forward.
        /// </summary>
        private void ShiftForward()
        {
            for (int i = 0; i < Size - 1;)
                Elements[i] = Elements[++i];
        }
    }
}
