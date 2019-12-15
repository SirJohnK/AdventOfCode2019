using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    public static class Day9
    {
        private static string RunBoostProgram(long input)
        {
            //Init
            var computer = new IntcodeComputer();
            var program = File.ReadAllText(@"..\..\..\Day9Input.txt").Split(",").Select(value => long.Parse(value)).ToList();
            var result = new List<long>();

            //Load program
            computer.Load(program);

            //Run program
            while (computer.State != ProgramState.Finished)
            {
                var output = computer.Run(new Queue<long>(new long[] { input }));
                if (output.HasValue) result.Add(output.Value);
            }

            return string.Join(",", result.Select(value => value.ToString()));
        }

        public static string ExecutePart1()
        {
            return RunBoostProgram(1);
        }

        public static string ExecutePart2()
        {
            return RunBoostProgram(2);
        }
    }
}