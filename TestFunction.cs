using System;

namespace TestFunctionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // 測試 AddNumbers 函式
            //int result = AddNumbers(5, 10);

            //計算1加到3
            int result1to3 = AddNumbers(1, 2);
            
            result1to3 = AddNumbers(result1to3, 3);
            
            Console.WriteLine("1+2+4 = " + result1to3);
        }

        // 定義一個簡單的函式來相加兩個數字
        static int AddNumbers(int a, int b)
        {
            return a + b;
        }
    }
}
