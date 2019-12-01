using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    public static class Day1
    {
        public static int ExecutePart1()
        {
            return File.ReadAllLines(@"..\..\..\Day1Input.txt").ToList().Sum(mass => ((int)Math.Floor((double)int.Parse(mass) / 3)) - 2);
        }

        public static int ExecutePart2()
        {
            return File.ReadAllLines(@"..\..\..\Day1Input.txt").ToList().Sum(mass => Fuel(int.Parse(mass)));

            int Fuel(int value, int sum = 0)
            {
                var fuel = ((int)Math.Floor((double)value / 3)) - 2;
                return fuel > 0 ? Fuel(fuel, sum + fuel) : sum;
            }
        }
    }
}