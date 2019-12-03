using System;

namespace AdventOfCode2019
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Day 1
            Console.WriteLine($"Result Day 1 (Part 1): {Day1.ExecutePart1()}");
            Console.WriteLine($"Result Day 1 (Part 2): {Day1.ExecutePart2()}");

            //Day 2
            Console.WriteLine($"Result Day 2 (Part 1): {Day2.ExecutePart1()}");
            Console.WriteLine($"Result Day 2 (Part 2): {Day2.ExecutePart2(19690720)}");

            //Day 3
            Console.WriteLine($"Result Day 3 (Part 1): {Day3.ExecutePart1()}");
            Console.WriteLine($"Result Day 3 (Part 2): {Day3.ExecutePart2()}");
        }
    }
}