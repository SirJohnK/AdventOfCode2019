using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    public static class Day2
    {
        public static int ExecutePart1(int noun = 12, int verb = 2)
        {
            //Init
            var index = 0;
            var program = File.ReadAllText(@"..\..\..\Day2Input.txt").Split(",").Select(value => int.Parse(value)).ToList();
            program[1] = noun;
            program[2] = verb;

            while (program[index] == 1 || program[index] == 2)
            {
                var firstindex = program[index + 1];
                var secondindex = program[index + 2];
                var resultindex = program[index + 3];
                program[resultindex] = program[index] == 1 ? program[firstindex] + program[secondindex] : program[firstindex] * program[secondindex];
                index += 4;
            }

            return program[0];
        }

        public static int ExecutePart2(int target)
        {
            //Init
            var noun = -1;
            var verb = -1;
            var output = 0;

            while (output != target && noun++ < 99)
            {
                verb = -1;
                while (output != target && verb++ < 99)
                {
                    output = ExecutePart1(noun, verb);
                }
            }

            return 100 * noun + verb;
        }
    }
}