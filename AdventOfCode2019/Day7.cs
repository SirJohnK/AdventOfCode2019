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

        public static long ExecutePart1()
        {
            //Init
            var result = new List<long>();
            var computer = new IntcodeComputer();
            var phasesettings = Permutate(new List<long>() { 0, 1, 2, 3, 4 }, 5);
            var program = File.ReadAllText(@"..\..\..\Day7Input.txt").Split(",").Select(value => long.Parse(value)).ToList();

            //Test all phase settings permutations
            foreach (var phasesetting in phasesettings)
            {
                long signal = 0;
                foreach (var ps in phasesetting)
                {
                    //Load program
                    computer.Load(program);

                    //Run program
                    var output = computer.Run(new Queue<long>(new long[] { ps, signal }));
                    if (output.HasValue) signal = output.Value;
                }
                result.Add(signal);
            }

            return result.Max();
        }

        public static long ExecutePart2()
        {
            //Init
            var result = new List<long>();
            var phasesettings = Permutate(new List<long>() { 5, 6, 7, 8, 9 }, 5);
            var program = File.ReadAllText(@"..\..\..\Day7Input.txt").Split(",").Select(value => long.Parse(value)).ToList();

            foreach (var phasesetting in phasesettings)
            {
                //Init
                long signal = 0;
                var computers = new List<IntcodeComputer>()
                    {
                        new IntcodeComputer(), //A
                        new IntcodeComputer(), //B
                        new IntcodeComputer(), //C
                        new IntcodeComputer(), //D
                        new IntcodeComputer(), //E
                    };

                //Load programs
                computers.ForEach(computer => computer.Load(program));

                //While E is not Finished
                while (computers[4].State != ProgramState.Finished)
                {
                    for (int index = 0; index < 5; index++)
                    {
                        var input = computers[index].State == ProgramState.Initialized ? new Queue<long>(new long[] { phasesetting.ToList()[index], signal }) : new Queue<long>(new long[] { signal });
                        var output = computers[index].Run(input);
                        if (output.HasValue) signal = output.Value;
                    }
                }
                result.Add(signal);
            }

            return result.Max();
        }
    }
}