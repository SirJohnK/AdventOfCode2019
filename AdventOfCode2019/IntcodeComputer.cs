using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    public enum ProgramState
    {
        None = 0,
        Initialized = 1,
        Running = 2,
        Paused = 3,
        Finished = 4,
        Error = 5,
    }

    public class IntcodeComputer
    {
        private int index = 0;
        private long? output = default;
        private int relativebase = 0;
        private List<long> memory;

        public ProgramState State { get; private set; }

        private long Peek(int address)
        {
            //Outside allocated memory?
            if (address >= memory.Count) return default;
            return memory[address];
        }

        private void Poke(int address, long value)
        {
            //Allocate memory?
            if (address >= memory.Count) AllocateMemory((address - memory.Count) + 1);
            memory[address] = value;
        }

        private void AllocateMemory(int size) => memory.AddRange(Enumerable.Repeat<long>(default, size));

        private void FreeMemory() => memory = new List<long>();

        public void Load(List<long> program)
        {
            var pointer = 0;
            FreeMemory();
            AllocateMemory(program.Count);
            program.ForEach(code => memory[pointer++] = code);

            index = 0;
            output = default;
            relativebase = 0;
            State = ProgramState.Initialized;
        }

        public long? Run(Queue<long> input = default)
        {
            //Verify Program and Input
            if (State == ProgramState.None) throw new ApplicationException("No Intcode program loaded!");

            //Init
            output = input?.Peek();
            State = (Peek(index) == 99 ? ProgramState.Finished : ProgramState.Running);

            //Run program
            while (State == ProgramState.Running)
            {
                //Init
                var instruction = $"0000{Peek(index)}".Reverse().Take(5).ToList();

                switch (instruction[0])
                {
                    case '1': //Add
                    case '2': //Multiply
                        {
                            var firstValue = GetValue(instruction[2], index + 1);
                            var secondValue = GetValue(instruction[3], index + 2);
                            var resultindex = GetIndex(instruction[4], index + 3);
                            Poke(resultindex, instruction[0] == '1' ? firstValue + secondValue : firstValue * secondValue);
                            index += 4;
                            break;
                        }
                    case '3': //Input
                        {
                            var data = input?.Dequeue();
                            if (data != null)
                            {
                                var resultindex = GetIndex(instruction[2], index + 1);
                                Console.WriteLine($"Input: {data}");
                                Poke(resultindex, data.Value);
                            }
                            else
                            {
                                Console.WriteLine($"Error: Input missing, program stopped!");
                                State = ProgramState.Error;
                            }
                            index += 2;
                            break;
                        }
                    case '4': //Output
                        {
                            output = GetValue(instruction[2], index + 1);
                            Console.WriteLine($"Output: {output}");
                            State = ProgramState.Paused;
                            index += 2;
                            break;
                        }
                    case '5': //Jump-If-True
                    case '6': //Jump-If-False
                        {
                            var value = GetValue(instruction[2], index + 1);
                            var jump = (instruction[0] == '5' && value > 0) || (instruction[0] == '6' && value == 0);
                            index = jump ? (int)GetValue(instruction[3], index + 2) : index += 3;
                            break;
                        }
                    case '7': //Less Than
                    case '8': //Equals
                        {
                            var firstValue = GetValue(instruction[2], index + 1);
                            var secondValue = GetValue(instruction[3], index + 2);
                            var SetBool = (instruction[0] == '7' && firstValue < secondValue) || (instruction[0] == '8' && firstValue == secondValue);
                            var resultindex = GetIndex(instruction[4], index + 3);
                            Poke(resultindex, SetBool ? 1 : 0);
                            index += 4;
                            break;
                        }
                    case '9': //Adjusts the relative base
                        {
                            var value = GetValue(instruction[2], index + 1);
                            relativebase += (int)value;
                            index += 2;
                            break;
                        }
                }

                //Set State
                State = Peek(index) == 99 ? ProgramState.Finished : State;
            }

            //Return output
            return output;

            long GetValue(char mode, int pos)
            {
                switch (mode)
                {
                    case '0': //Position mode
                        return Peek((int)Peek(pos));

                    case '1': //Immediate mode
                        return Peek(pos);

                    case '2': //Relative mode
                        return Peek(relativebase + (int)Peek(pos));

                    default:
                        return default;
                }
            }

            int GetIndex(char mode, int pos)
            {
                switch (mode)
                {
                    case '0': //Position mode
                    case '1': //Immediate mode
                        return (int)Peek(pos);

                    case '2': //Relative Immediate mode
                        return (int)(relativebase + Peek(pos));

                    default:
                        return default;
                }
            }
        }
    }
}