using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    public static class Day5
    {
        public static int ExecutePart1(int systemId)
        {
            //Init
            var index = 0;
            var output = 0;
            var program = File.ReadAllText(@"..\..\..\Day5Input.txt").Split(",").Select(value => int.Parse(value)).ToList();

            while (program[index] != 99)
            {
                //Init
                var instruction = $"000{program[index]}".Reverse().Take(4).ToList();

                switch (instruction[0])
                {
                    case '1': //Add
                    case '2': //Multiply
                        {
                            var firstValue = GetValue(program, instruction[2], index + 1);
                            var secondValue = GetValue(program, instruction[3], index + 2);
                            var resultindex = program[index + 3];
                            program[resultindex] = instruction[0] == '1' ? firstValue + secondValue : firstValue * secondValue;
                            index += 4;
                            break;
                        }
                    case '3': //Input
                        {
                            Console.WriteLine($"Input System ID: {systemId}");
                            program[program[index + 1]] = systemId;
                            index += 2;
                            break;
                        }
                    case '4': //Output
                        {
                            output = GetValue(program, instruction[2], index + 1);
                            Console.WriteLine($"Output: {output}");
                            index += 2;
                            break;
                        }
                    case '5': //Jump-If-True
                    case '6': //Jump-If-False
                        {
                            var value = GetValue(program, instruction[2], index + 1);
                            var jump = (instruction[0] == '5' && value > 0) || (instruction[0] == '6' && value == 0);
                            index = jump ? GetValue(program, instruction[3], index + 2) : index += 3;
                            break;
                        }
                    case '7': //Less Than
                    case '8': //Equals
                        {
                            var firstValue = GetValue(program, instruction[2], index + 1);
                            var secondValue = GetValue(program, instruction[3], index + 2);
                            var SetBool = (instruction[0] == '7' && firstValue < secondValue) || (instruction[0] == '8' && firstValue == secondValue);
                            var resultindex = program[index + 3];
                            program[resultindex] = SetBool ? 1 : 0;
                            index += 4;
                            break;
                        }
                }
            }

            return output;

            int GetValue(List<int> program, char mode, int pos) => mode == '0' ? program[program[pos]] : program[pos];
        }

        public static int ExecutePart2(int systemId)
        {
            return ExecutePart1(systemId);
        }
    }
}