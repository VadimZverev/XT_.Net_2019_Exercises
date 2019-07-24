using System;

namespace _34_Dynamic_Array_Hardcore_Mode
{
    class Program
    {
        static void Main()
        {
            DynamicArray<int> vs = new DynamicArray<int> {  1, 2, 3  };
            //Console.WriteLine(vs[-1]);
            //vs.Capacity = 2;
            DynamicArray<int> vs2 = vs.Clone() as DynamicArray<int>;
            vs2.Add(11);
            int[] arr = vs2.ToArray();
            CycledDynamicArray<int> vs3 = new CycledDynamicArray<int>(arr);

            foreach (int item in vs2)
            {
                Console.WriteLine(item);
            }
        }
    }
}
