using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Task3.Logic
{
    public class Set<T> : IEnumerable<T>, ICloneable where T : class, IComparable
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
        /// Unites two sets.
        /// </summary>
        /// <param name="set1">The first set.</param>
        /// <param name="set2">The second set.</param>
        /// <returns>Returns the union set.</returns>
        public static Set<T> Union(Set<T> set1, Set<T> set2)
        {
            var unionSet = new Set<T>();
            var elements = set1.Union(set2);
            foreach (var element in elements)
                unionSet.Insert(element);

            return unionSet;
        }

        /// <summary>
        /// Intersects two sets.
        /// </summary>
        /// <param name="set1">The first set.</param>
        /// <param name="set2">The second set.</param>
        /// <returns>Returns the intersection set.</returns>
        public static Set<T> Intersect(Set<T> set1, Set<T> set2)
        {
            var intersectionSet = new Set<T>();
            var elements = set1.Intersect(set2);
            foreach (var element in elements)
                intersectionSet.Insert(element);

            return intersectionSet;
        }

        /// <summary>
        /// Calculates the difference between
        /// the first set and the second set.
        /// </summary>
        /// <param name="set1">The first set.</param>
        /// <param name="set2">The second set.</param>
        /// <returns>Returns the difference.</returns>
        public static Set<T> Except(Set<T> set1, Set<T> set2)
        {
            var exceptedSet = new Set<T>();
            var elements = set1.Except(set2);
            foreach (var element in elements)
                exceptedSet.Insert(element);

            return exceptedSet;
        }

        /// <summary>
        /// Calculates the symmetric difference between
        /// the first set and the second set.
        /// </summary>
        /// <param name="set1">The first set.</param>
        /// <param name="set2">The second set.</param>
        /// <returns>Returns the symmetric difference.</returns>
        public static Set<T> SymmetricExcept(Set<T> set1, Set<T> set2)
        {
            var union = Union(set1, set2);
            var intersection = Intersect(set1, set2);
            return Except(union, intersection);
        }

        /// <summary>
        /// Clones the set.
        /// </summary>
        /// <returns>Returns the clone of the set.</returns>
        object ICloneable.Clone() => Clone();

        /// <summary>
        /// Clones the set.
        /// </summary>
        /// <returns>Returns the clone of the set.</returns>
        public Set<T> Clone()
        {
            var setCopy = new Set<T>
            {
                Size = Size,
                Elements = new T[Capacity]
            };
            Array.Copy(Elements, setCopy.Elements, Size);
            return setCopy;
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
