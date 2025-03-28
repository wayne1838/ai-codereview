using System;

namespace TestFunctionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // 測試 AddNumbers 函式
            int result = AddNumbers(5, 10);

            
            Console.WriteLine("5+10=" + result);
        }

        // 定義一個簡單的函式來相加兩個數字
        static int AddNumbers(int a, int b)
        {
            return a + b;
        }
    }
}
