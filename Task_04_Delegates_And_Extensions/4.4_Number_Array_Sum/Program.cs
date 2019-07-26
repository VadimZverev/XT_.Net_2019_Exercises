using System;
using System.Linq.Expressions;

namespace _44_Number_Array_Sum
{
    class Program
    {
        static void Main()
        {
            int sumInt = new int[] { 1, 2, 3 }.Sum();
            sumInt = new int[] { 1, 2, 3 }.Sum((a, b) => a + b);

            double sum = new double[] { 1.21d, 2.6d, 3.5d }.Sum();
            sum = new double[] { 1.21d, 2.6d, 3.5d }.Sum((a, b) => a + b);
        }
    }

    public static class MyExtensions
    {
        /// <summary>
        /// Returns the sum of numbers in an array of arbitrary type.
        /// </summary>
        /// <typeparam name="T">array type</typeparam>
        /// <param name="array">transmitted array</param>
        /// <returns>Returns the sum of numbers in an array of arbitrary type.</returns>
        public static T Sum<T>(this T[] array)
        {
            T sum = default;

            foreach (T item in array)
            {
                sum = Add(sum, item);
            }

            return sum;
        }

        /// <summary>
        /// Returns the sum of numbers in an array of arbitrary type.
        /// </summary>
        /// <typeparam name="T">array type</typeparam>
        /// <param name="array">transmitted array</param>
        /// <param name="funcSum">function of sum</param>
        /// <returns>Returns the sum of numbers in an array of arbitrary type.</returns>
        public static T Sum<T>(this T[] array, Func<T, T, T> funcSum)
        {
            T sum = default;

            for (int i = 0; i < array.Length; i++)
            {
                sum = funcSum(sum, array[i]);
            }

            return sum;
        }

        /// <summary>
        /// Performs addition with two arbitrary types.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <typeparam name="T">transmitted type</typeparam>
        /// <param name="a">first parameter</param>
        /// <param name="b">second parameter</param>
        /// <returns>Returns the sum of two parameters of arbitrary type.</returns>
        private static T Add<T>(T a, T b)
        {
            var paramA = Expression.Parameter(typeof(T), "a");
            var paramB = Expression.Parameter(typeof(T), "b");

            BinaryExpression body = Expression.Add(paramA, paramB);
            Func<T, T, T> add =
                Expression.Lambda<Func<T, T, T>>(body, paramA, paramB).Compile();

            return add(a, b);
        }
    }
}
