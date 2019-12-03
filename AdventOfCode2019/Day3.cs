using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    public static class Day3
    {
        private class Line
        {
            public Point Start, End;

            public Line(Point start, Point end)
            {
                Start = start;
                End = end;
            }

            public IEnumerable<Point> PlotLine
            {
                get
                {
                    if (Start.X == End.X)
                    {
                        var plotline = Enumerable.Range(Start.Y > End.Y ? End.Y : Start.Y, Math.Abs(Start.Y - End.Y) + 1).Select(Y => new Point(Start.X, Y));
                        return Start.Y > End.Y ? plotline.Reverse() : plotline;
                    }
                    else
                    {
                        var plotline = Enumerable.Range(Start.X > End.X ? End.X : Start.X, Math.Abs(Start.X - End.X) + 1).Select(X => new Point(X, Start.Y)); ;
                        return Start.X > End.X ? plotline.Reverse() : plotline;
                    }
                }
            }
        }

        private static List<List<Line>> GetLines()
        {
            //Init
            var port = new Point(0, 0);
            var wires = File.ReadAllLines(@"..\..\..\Day3Input.txt").Select(line => line.Split(","));
            return wires.Select(wire =>
            {
                var pos = port;
                return wire.Select(path =>
                {
                    var line = GetLine(path, pos);
                    pos = line.End;
                    return line;
                }).ToList();
            }).ToList();

            Line GetLine(string path, Point start)
            {
                var direction = path.Substring(0, 1);
                var length = int.Parse(path.Substring(1));
                return direction switch
                {
                    "U" => new Line(start, new Point(start.X, start.Y - length)),

                    "D" => new Line(start, new Point(start.X, start.Y + length)),

                    "R" => new Line(start, new Point(start.X + length, start.Y)),

                    "L" => new Line(start, new Point(start.X - length, start.Y)),

                    _ => default,
                };
            }
        }

        private static List<Point> GetIntersections(List<List<Line>> lines)
        {
            //Init
            var intersections = new List<Point>();
            lines[0].ForEach(firstline =>
            {
                var firstplotline = firstline.PlotLine;
                lines[1].ForEach(secondline =>
                {
                    var intersection = firstplotline.Intersect(secondline.PlotLine).FirstOrDefault();
                    if (!intersection.IsEmpty) intersections.Add(intersection);
                });
            });
            return intersections;
        }

        public static int ExecutePart1()
        {
            //Init
            var intersections = GetIntersections(GetLines());
            return intersections.Select(intersection => Math.Abs(intersection.X) + Math.Abs(intersection.Y)).Min();
        }

        public static int ExecutePart2()
        {
            //Init
            var lines = GetLines();
            var intersections = GetIntersections(lines);
            var steps = new List<int>();

            intersections.ForEach(intersection =>
            {
                steps.Add(CountSteps(lines[0], intersection) + CountSteps(lines[1], intersection));

                int CountSteps(List<Line> lines, Point intersection)
                {
                    var line = 0;
                    var counter = 0;
                    var plotline = lines[line++].PlotLine;
                    while (!plotline.Contains(intersection))
                    {
                        counter += (plotline.Count() - 1);
                        plotline = lines[line++].PlotLine;
                    }
                    counter += plotline.ToList().IndexOf(intersection);
                    return counter;
                }
            });

            return steps.Min();
        }
    }
}