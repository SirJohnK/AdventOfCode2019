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

            //Day 4
            Console.WriteLine($"Result Day 4 (Part 1): {Day4.ExecutePart1(145852, 616942)}");
            Console.WriteLine($"Result Day 4 (Part 2): {Day4.ExecutePart2(145852, 616942, 1)}");

            //Day 5
            Console.WriteLine($"Result Day 5 (Part 1): {Day5.ExecutePart1(1)}");
            Console.WriteLine($"Result Day 5 (Part 2): {Day5.ExecutePart2(5)}");

            //Day 6
            Console.WriteLine($"Result Day 6 (Part 1): {Day6.ExecutePart1()}");
            Console.WriteLine($"Result Day 6 (Part 2): {Day6.ExecutePart2()}");

            //Day 7
            Console.WriteLine($"Result Day 7 (Part 1): {Day7.ExecutePart1()}");
            Console.WriteLine($"Result Day 7 (Part 2): {Day7.ExecutePart2()}");

            //Day 8
            Console.WriteLine($"Result Day 8 (Part 1): {Day8.ExecutePart1()}");
            Console.WriteLine($"Result Day 8 (Part 2): {Day8.ExecutePart2()}");

            //Day 9
            Console.WriteLine($"Result Day 9 (Part 1): {Day9.ExecutePart1()}");
            Console.WriteLine($"Result Day 9 (Part 2): {Day9.ExecutePart2()}");
        }
    }
}