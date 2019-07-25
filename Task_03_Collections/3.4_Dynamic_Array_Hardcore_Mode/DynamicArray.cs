using System;
using System.Collections;
using System.Collections.Generic;

namespace _34_Dynamic_Array_Hardcore_Mode
{
    class DynamicArray<T> : IEnumerable<T>, IEnumerable, ICloneable
    {
        #region Fields

        protected const int _defaultCapacity = 8;
        protected T[] _array;
        private int capacity;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance with a capacity of 8.
        /// </summary>
        public DynamicArray() : this(_defaultCapacity) { }

        /// <summary>
        /// Initializes an instance with installed capacity.
        /// </summary>
        /// <param name="capacity">dynamic array capacity</param>
        public DynamicArray(int capacity)
        {
            Capacity = capacity;
            _array = new T[capacity];
        }

        /// <summary>
        /// Initializes an instance with the passed collection.
        /// </summary>
        /// <param name="collection">passed collection</param>
        public DynamicArray(IEnumerable<T> collection)
            : this(_defaultCapacity)
        {
            AddRange(collection);
        }

        #endregion

        #region Indexer

        public T this[int index]
        {
            get
            {
                if (index < 0)
                {
                    index = Length + index;
                }

                if (IsOutOfRange(index))
                {
                    throw new ArgumentOutOfRangeException();
                }

                return _array[index];
            }

            set
            {
                if (IsOutOfRange(index))
                {
                    throw new ArgumentOutOfRangeException();
                }

                _array[index] = value;
            }
        }

        #endregion

        #region Properties

        public int Capacity
        {
            get => capacity;
            set
            {
                if (capacity > value)
                {
                    T[] tempArray = new T[value];
                    Array.Copy(_array, 0, tempArray, 0, value);
                    _array = tempArray;
                    Length = value;
                }

                capacity = value;
            }
        }
        public int Length { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Adding an object to the end of the array.
        /// </summary>
        /// <param name="item">transmitted item</param>
        public void Add(T item)
        {
            if (UpCapacity(Length + 1))
            {
                T[] tempArray = new T[Capacity];
                _array.CopyTo(tempArray, 0);

                _array = tempArray;
            }

            _array[Length] = item;
            Length++;
        }

        /// <summary>
        /// Adds a collection to the end of the array.
        /// </summary>
        /// <param name="collection">passed collection</param>
        public void AddRange(IEnumerable<T> collection)
        {
            int tempLength = GetLength(collection);

            if (UpCapacity(tempLength + Length))
            {
                T[] tempArray = new T[Capacity];
                _array.CopyTo(tempArray, 0);

                _array = tempArray;
            }

            foreach (var item in collection)
            {
                _array[Length] = item;
                Length++;
            }
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone()
        {
            return new DynamicArray<T>(capacity)
            {
                _array = _array.Clone() as T[],
                Length = Length
            };
        }

        /// <summary>
        ///  Returns the contents of the collection.
        /// </summary>
        public virtual IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Length; i++)
            {
                yield return _array[i];
            }
        }

        /// <summary>
        ///  Returns the contents of the collection.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Inserts an object at the specified location.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <param name="index">insert index</param>
        /// <param name="item">transmitted item</param>
        /// <returns>Returns true if the object is inserted; false otherwise.</returns>
        public bool Insert(int index, T item)
        {
            if (index > Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (Length == int.MaxValue)
            {
                return false;
            }

            if (Length == Capacity)
            {
                UpCapacity(Length + 1);
            }

            if (index < Length)
            {
                Array.Copy(_array, index, _array, index + 1, Length - index);
            }

            _array[index] = item;
            Length++;

            return true;
        }

        /// <summary>
        /// Deletes the specified object.
        /// </summary>
        /// <param name="item">transmitted item</param>
        /// <returns>Returns true if the object is deleted; false otherwise.</returns>
        public bool Remove(T item)
        {
            for (int i = 0; i < Length; i++)
            {
                if (_array[i].Equals(item))
                {
                    RemoveAt(i);

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes the object at the specified index.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <param name="index">transmitted index</param>
        public void RemoveAt(int index)
        {
            if (index >= Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            Length--;

            if (index < Length)
            {
                Array.Copy(_array, index + 1, _array, index, Length - index);
            }

            _array[Length] = default;
        }

        /// <summary>
        /// Converts to a regular array of the specified type.
        /// </summary>
        /// <returns>Returns an array of the specified type.</returns>
        public T[] ToArray()
        {
            T[] tempArray = new T[Length];

            for (int i = 0; i < Length; i++)
            {
                tempArray[i] = _array[i];
            }

            return tempArray;
        }

        /// <summary>
        /// Counting the length of the enumerated collection.
        /// </summary>
        /// <param name="collection">passed collection</param>
        /// <returns>Returns the number of items in the collection.</returns>
        private int GetLength(IEnumerable<T> collection)
        {
            int count = 0;

            foreach (T item in collection)
            {
                count++;
            }

            return count;
        }

        /// <summary>
        /// Check if the index is outside the range of the array.
        /// </summary>
        /// <param name="index">verifiable index</param>
        /// <returns>Returns true if the index is out of range; false otherwise.</returns>
        private bool IsOutOfRange(int index)
        {
            if (index >= Length || index < 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Performs a comparison of capacity with the transmitted value. 
        /// Increases if capacity is less than value.
        /// </summary>
        /// <param name="value">compared value</param>
        /// <returns>Returns true if increments; false otherwise</returns>
        private bool UpCapacity(int value)
        {
            if (Capacity < value)
            {
                int num = Length == 0 ? _defaultCapacity : Length * 2;

                if (num > int.MaxValue)
                {
                    num = int.MaxValue;
                }
                else if (num < value)
                {
                    num = value;
                }

                Capacity = num;

                return true;
            }

            return false;
        }

        #endregion
    }
}