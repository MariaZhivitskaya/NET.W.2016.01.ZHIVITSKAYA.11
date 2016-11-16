
using System;

namespace Task2.Logic
{
    public class Queue <T>
    {
        private int _capacity;

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

        public int Size { get; private set; }

        public T[] Elements { get; }

        public Queue(int capacity)
        {
            Capacity = capacity;
            Size = 0;
            Elements = new T[Capacity];
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

            Elements[++Size] = element;
        }

        /// <summary>
        /// Gets the element form the queue.
        /// </summary>
        /// <exception cref="">
        /// Thrown if the queue is empty;
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
        /// Shifts the array of elements one position forward.
        /// </summary>
        private void ShiftForward()
        {
            for (int i = 0; i < Size - 1;)
            {
                var tmp = Elements[i];
                Elements[i] = Elements[++i];
                Elements[i] = tmp;
            }
        }
    }
}
