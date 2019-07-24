using System.Collections.Generic;

namespace _34_Dynamic_Array_Hardcore_Mode
{
    class CycledDynamicArray<T> : DynamicArray<T>
    {
        /// <summary>
        /// Инициализирует экземпляр с ёмкостью равно 8.
        /// </summary>
        public CycledDynamicArray() : base() { }

        /// <summary>
        /// Инициализирует экземпляр с устанавливаемой ёмкостью.
        /// </summary>
        /// <param name="capacity">ёмкость динамического массива</param>
        public CycledDynamicArray(int capacity) :base(capacity) { }

        public CycledDynamicArray(IEnumerable<T> collection)
            : base(collection) { }

        public override IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i <= Length; i++)
            {
                if (i == Length) i = 0;

                yield return _array[i];
            }
        }
    }
}
