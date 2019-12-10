using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    public enum ProgramState
    {
        Initialized = 0,
        Running = 1,
        Paused = 2,
        Finished = 3,
    }

    public class IntcodeComputer
    {
        private int index = 0;
        private int output = 0;
        private List<int> backup;
        private List<int> program;

        public IntcodeComputer(List<int> program)
        {
            //Init
            backup = program;
            this.program = backup.ToList();
            State = ProgramState.Initialized;
        }

        public ProgramState State { get; private set; }

        public int Run(Queue<int> input, bool reset = true)
        {
            //Reset
            if (reset)
            {
                index = 0;
                program = backup.ToList();
            }

            //Init
            output = 0;
            State = (program[index] == 99 ? ProgramState.Finished : ProgramState.Running);

            //Run program
            while (State == ProgramState.Running)
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
                            var data = input.Dequeue();
                            Console.WriteLine($"Input: {data}");
                            program[program[index + 1]] = data;
                            index += 2;
                            break;
                        }
                    case '4': //Output
                        {
                            output = GetValue(program, instruction[2], index + 1);
                            Console.WriteLine($"Output: {output}");
                            index += 2;
                            State = ProgramState.Paused;
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

                //Set State
                State = (program[index] == 99 ? ProgramState.Finished : State);
            }

            //Return output
            return output == 0 && input.Count > 0 ? input.Dequeue() : output;

            int GetValue(List<int> program, char mode, int pos) => mode == '0' ? program[program[pos]] : program[pos];
        }
    }
}