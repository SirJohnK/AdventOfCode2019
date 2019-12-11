using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    public static class Day8
    {
        private static List<List<T>> splitList<T>(this List<T> list, int nSize)
        {
            var splitlist = new List<List<T>>();

            for (int i = 0; i < list.Count; i += nSize)
                splitlist.Add(list.GetRange(i, Math.Min(nSize, list.Count - i)));

            return splitlist;
        }

        private static List<List<int>> GetLayers(int width, int height)
        {
            return File.ReadAllText(@"..\..\..\Day8Input.txt").Select(value => int.Parse(value.ToString())).ToList().splitList(width * height);
        }

        public static int ExecutePart1()
        {
            var layers = GetLayers(25, 6);
            var result = layers.OrderBy(layer => layer.Count(digit => digit == 0)).First();
            return result.Count(digit => digit == 1) * result.Count(digit => digit == 2);
        }

        public static string ExecutePart2()
        {
            var layers = GetLayers(25, 6);
            var colors = new List<int>();

            for (int pixel = 0; pixel < 25 * 6; pixel++)
            {
                var layer = 0;
                while (layers[layer][pixel] == 2) { layer++; }
                colors.Add(layers[layer][pixel]);
            }

            var image = colors.splitList(25);
            var password = image.Select(pixels => string.Join("", pixels));
            var output = Environment.NewLine + string.Join(Environment.NewLine, password);

            return output;
        }
    }
}