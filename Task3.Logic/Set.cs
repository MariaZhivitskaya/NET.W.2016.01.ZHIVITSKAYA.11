using System;
using System.Collections;
using System.Collections.Generic;

namespace Task3.Logic
{
    public class Set <T> : IEnumerable<T>/*, IEquatable<T> */where T : class
    {
        private int _capacity;
        private T[] _elements = {};

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

        public Set(int capacity)
        {
            Capacity = capacity;
            Size = 0;
            Elements = new T[Capacity];
        }

        public Set(int capacity, params T[] elements)
        {
            Capacity = capacity;
            Elements = elements;
            Size = elements.Length;
        }

        /// <summary>
        /// Indicates whether the set is full.
        /// </summary>
        /// <returns>Returns true or false.</returns>
        public bool IsFull() => Size == Capacity;

        /// <summary>
        /// Indicates whether the set is empty.
        /// </summary>
        /// <returns>Returns true or false.</returns>
        public bool IsEmpty() => Size == 0;

        /// <summary>
        /// Inserts the element in the set.
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the set is full.
        /// </exception>
        /// </summary>
        /// <param name="element">The element.</param>
        public void Insert(T element)
        {
            if (IsFull())
                throw new ArgumentOutOfRangeException("Set is full!");

            for (int i = 0; i < Size; i++)
                if (Elements[i].Equals(element))
                    break;

            Elements[Size++] = element;
        }

        /// <summary>
        /// Erases the element from the set.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the set is full.
        /// </exception>
        /// <param name="element">The element.</param>
        public void Erase(T element)
        {
            if (IsEmpty())
                throw new ArgumentOutOfRangeException("Set is empty!");

            int index = Index(element);

            for (int i = index; i < Size - 1; i++)
                Elements[i] = Elements[i + 1];

            Size--;
        }

        /// <summary>
        /// Gets the enumerator for the set.
        /// </summary>
        /// <returns>Returns the next element.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for(int i = 0; i < Size; i++)
                yield return Elements[i];
        }

        /// <summary>
        /// Gets the enumerator for the set.
        /// </summary>
        /// <returns>Returns the next element.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /*public bool Equals(T other)
        {
            if (other == null)
                return false;

            return (this == other);
        }*/

        /// <summary>
        /// Finds the index of a specified element.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if such element isn't in the set.
        /// </exception>
        /// <param name="element">The element.</param>
        /// <returns>Returns the index.</returns>
        private int Index(T element)
        {
            for (int i = 0; i < Size; i++)
                if (Elements[i].Equals(element))
                    return i;

            throw new ArgumentNullException("No such element in the set!");
        }
    }
}
