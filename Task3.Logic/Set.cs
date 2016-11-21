using System;
using System.Collections;
using System.Collections.Generic;

namespace Task3.Logic
{
    public class Set<T> : IEnumerable<T> where T : class, IComparable
    {
        private T[] _elements = {};
        private const int ScalingCoefficient = 2;
        private const int StartCapacity = 5;

        /// <summary>
        /// A property for a capacity.
        /// </summary>
        public int Capacity { get; private set; }

        /// <summary>
        /// A property for a size.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// A property for elements.
        /// </summary>
        public T[] Elements
        {
            get { return _elements; }
            private set
            {
                _elements = new T[Capacity];
                value.CopyTo(_elements, 0);
            }
        }

        public Set()
        {
            Capacity = StartCapacity;
            Size = 0;
            Elements = new T[Capacity];
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
        /// </summary>
        /// <param name="element">The element.</param>
        public void Insert(T element)
        {
            if (!Contains(element))
            {
                if (IsFull())
                    Resize();

                Elements[Size++] = element;
            }
        }

        /// <summary>
        /// Erases the element from the set.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the set is empty.
        /// </exception>
        /// <param name="element">The element.</param>
        public void Erase(T element)
        {
            if (IsEmpty())
                throw new ArgumentOutOfRangeException("Set is empty!");

            int index = Index(element);

            for (int i = index; i < Size - 1; i++)
                Elements[i] = Elements[i + 1];

            Elements[Size - 1] = default(T);
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

        /// <summary>
        /// Resizes the set according to the scaling coefficient.
        /// </summary>
        private void Resize()
        {
            Capacity *= ScalingCoefficient;
            Array.Resize(ref _elements, Capacity);
        }

        /// <summary>
        /// Indicates whether the set contains 
        /// the element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        private bool Contains(T element)
        {
            for (int i = 0; i < Size; i++)
                if (Elements[i].Equals(element))
                    return true;
            return false;
        }

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
