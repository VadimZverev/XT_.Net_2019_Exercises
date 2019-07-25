using System.Collections.Generic;

namespace _34_Dynamic_Array_Hardcore_Mode
{
    class CycledDynamicArray<T> : DynamicArray<T>
    {
        /// <summary>
        /// Initializes an instance with a capacity of 8.
        /// </summary>
        public CycledDynamicArray() : base() { }

        /// <summary>
        /// Initializes an instance with installed capacity.
        /// </summary>
        /// <param name="capacity">dynamic array capacity</param>
        public CycledDynamicArray(int capacity) :base(capacity) { }

        /// <summary>
        /// Initializes an instance with the passed collection.
        /// </summary>
        /// <param name="collection">passed collection</param>
        public CycledDynamicArray(IEnumerable<T> collection)
            : base(collection) { }

        /// <summary>
        ///  Returns the contents of the collection.
        /// </summary>
        /// <remarks>Infinitely iterates over the collection</remarks>
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
