using System;
using System.Collections;
using System.Collections.Generic;

namespace _33_Dynamic_Array
{
    class DynamicArray<T> : IEnumerable<T>, IEnumerable
    {
        #region Поля

        private const int _defaultCapacity = 8;
        private int capacity;
        private T[] _array;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Инициализирует экземпляр с ёмкостью равно 8.
        /// </summary>
        public DynamicArray() : this(_defaultCapacity) { }

        /// <summary>
        /// Инициализирует экземпляр с устанавливаемой ёмкостью.
        /// </summary>
        /// <param name="capacity">ёмкость динамического массива</param>
        public DynamicArray(int capacity)
        {
            Capacity = capacity;
            _array = new T[capacity];
        }

        public DynamicArray(IEnumerable<T> collection)
            : this(_defaultCapacity)
        {
            AddRange(collection);
        }

        #endregion

        #region Индексатор

        public T this[int index]
        {
            get
            {
                if (index >= Length)
                {
                    throw new ArgumentOutOfRangeException();
                }

                return _array[index];
            }

            set
            {
                if (index >= Length)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _array[index] = value;
            }
        }

        #endregion

        #region Свойства

        public int Capacity
        {
            get => capacity;
            set
            {
                if (!(capacity >= value))
                {
                    capacity = value;
                }
            }
        }

        public int Length { get; private set; } = 0;

        #endregion

        #region Методы

        /// <summary>
        /// Добавление объекта в конец массива.
        /// </summary>
        /// <param name="item">передаваемый объект</param>
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
        /// добавляет коллекцию в конец массива.
        /// </summary>
        /// <param name="collection">передаваемая коллекция.</param>
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
        /// Вставляет объект в указанное место.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <param name="index">индекс вставки</param>
        /// <param name="item">передаваемый объект</param>
        /// <returns>Возвращает true, если объект вставлен, иначе false</returns>
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
        /// Удаляет указываемый объект.
        /// </summary>
        /// <param name="item">передаваемый объект</param>
        /// <returns>Возвращает true, если объект удалён, иначе false.</returns>
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
        /// Удаляет объект по указанному массиву.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <param name="index">передаваемый индекс</param>
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
        /// Возвращает содержимое коллекции.
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            int position = 0;
            foreach (var item in _array)
            {
                if (position == Length) break;

                position++;
                yield return item;
            }
        }

        /// <summary>
        /// Осуществляет сравнение ёмкости с передаваемым значением.
        /// Увеличивает, если ёмкость меньше значения.
        /// </summary>
        /// <param name="value">сравниваемое значение</param>
        /// <returns>Возвращает true, если увеличивает, иначе false</returns>
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

        /// <summary>
        /// Подсчёт длины перечислимой коллекции.
        /// </summary>
        /// <param name="collection">передаваемая коллекция.</param>
        /// <returns>Возвращает количество элементов в коллекции.</returns>
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
        /// Возвращает содержимое коллекции.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        } 

        #endregion
    }
}