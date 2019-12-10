using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    public static class Day7
    {
        private static IEnumerable<IEnumerable<T>> Permutate<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return Permutate(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        public static int ExecutePart1()
        {
            //Init
            var result = new List<int>();
            var phasesettings = Permutate(new List<int>() { 0, 1, 2, 3, 4 }, 5);
            var program = File.ReadAllText(@"..\..\..\Day7Input.txt").Split(",").Select(value => int.Parse(value)).ToList();
            var computer = new IntcodeComputer(program);

            foreach (var phasesetting in phasesettings)
            {
                var signal = 0;
                foreach (var ps in phasesetting)
                {
                    signal = computer.Run(new Queue<int>(new int[] { ps, signal }));
                }
                result.Add(signal);
            }

            return result.Max();
        }

        public static int ExecutePart2()
        {
            //Init
            var result = new List<int>();
            var phasesettings = Permutate(new List<int>() { 5, 6, 7, 8, 9 }, 5);
            var program = File.ReadAllText(@"..\..\..\Day7Input.txt").Split(",").Select(value => int.Parse(value)).ToList();

            foreach (var phasesetting in phasesettings)
            {
                //Init
                var signal = 0;
                var computers = new List<IntcodeComputer>()
                {
                    new IntcodeComputer(program), //A
                    new IntcodeComputer(program), //B
                    new IntcodeComputer(program), //C
                    new IntcodeComputer(program), //D
                    new IntcodeComputer(program), //E
                };

                //While E is not Finished
                while (computers[4].State != ProgramState.Finished)
                {
                    for (int index = 0; index < 5; index++)
                    {
                        var input = computers[index].State == ProgramState.Initialized ? new Queue<int>(new int[] { phasesetting.ToList()[index], signal }) : new Queue<int>(new int[] { signal });
                        signal = computers[index].Run(input, false);
                    }
                }
                result.Add(signal);
            }

            return result.Max();
        }
    }
}