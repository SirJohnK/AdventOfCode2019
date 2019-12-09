using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    public static class Day6
    {
        private static Dictionary<string, List<string>> GetOrbits()
        {
            //Init
            var orbits = new Dictionary<string, List<string>>();
            var input = File.ReadAllLines(@"..\..\..\Day6Input.txt").Select(value => value.Split(")")).ToList();

            input.ForEach(orbit =>
            {
                if (orbits.ContainsKey(orbit[0]))
                    orbits[orbit[0]].Add(orbit[1]);
                else
                    orbits.Add(orbit[0], new List<string>() { orbit[1] });
            });

            return orbits;
        }

        public static int ExecutePart1()
        {
            //Init
            var orbitcount = 0;
            var orbits = GetOrbits();

            //Count direct and indirect orbits
            foreach (var planets in orbits.Values)
                orbitcount += CountOrbits(planets);

            return orbitcount;

            int CountOrbits(List<string> planets)
            {
                var count = planets.Count;
                planets.ForEach(value => count += orbits.ContainsKey(value) ? CountOrbits(orbits[value]) : 0);
                return count;
            }
        }

        public static int ExecutePart2()
        {
            //Init
            var sourcecount = 0;
            var destinationcount = 0;
            var orbiters = File.ReadAllLines(@"..\..\..\Day6Input.txt").Select(value => value.Split(")")).ToDictionary(value => value[1], value => value[0]);

            var source = orbiters["YOU"];
            var destination = orbiters["SAN"];
            while (source != destination)
            {
                sourcecount++;
                source = orbiters[source];
                destinationcount = 0;
                destination = orbiters["SAN"];
                while (source != destination && destination != "COM")
                {
                    destinationcount++;
                    destination = orbiters[destination];
                }
            }

            return sourcecount + destinationcount;
        }
    }
}