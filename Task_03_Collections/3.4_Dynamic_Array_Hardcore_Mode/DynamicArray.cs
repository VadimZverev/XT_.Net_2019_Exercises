using System;
using System.Collections;
using System.Collections.Generic;

namespace _34_Dynamic_Array_Hardcore_Mode
{
    class DynamicArray<T> : IEnumerable<T>, IEnumerable, ICloneable
    {
        #region Поля

        protected const int _defaultCapacity = 8;
        protected T[] _array;
        private int capacity;

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

        #region Свойства

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

        public object Clone()
        {
            return new DynamicArray<T>(capacity)
            {
                _array = _array.Clone() as T[],
                Length = Length
            };
        }

        /// <summary>
        /// Возвращает содержимое коллекции.
        /// </summary>
        public virtual IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Length; i++)
            {
                yield return _array[i];
            }
        }

        /// <summary>
        /// Возвращает содержимое коллекции.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
        /// Проверка, выходит ли индекс за диапазон массива.
        /// </summary>
        /// <param name="index">проверяемый индекс</param>
        /// <returns>Возвращает true, если индекс выходит за пределы, иначе false.</returns>
        private bool IsOutOfRange(int index)
        {
            if (index >= Length || index < 0)
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}