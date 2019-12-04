namespace AdventOfCode2019
{
    public static class Day4
    {
        public static int ExecutePart1(int start, int end, int maxAdjacent = 5)
        {
            //Init
            var count = 0;

            for (int value = start; value <= end; value++)
            {
                count += Match(value, maxAdjacent) ? 1 : 0;
            }

            return count;

            bool Match(int value, int maxAdjacent)
            {
                //Init
                var index = -1;
                var match = true;
                var adjacent = false;
                var adjacentCount = 0;
                var number = value.ToString();

                while (match && ++index < 5)
                {
                    match = number[index] <= number[index + 1];
                    if (!adjacent)
                    {
                        if (number[index] == number[index + 1])
                            adjacentCount++;
                        else if (adjacentCount > 0)
                        {
                            adjacent = adjacentCount <= maxAdjacent;
                            adjacentCount = 0;
                        }
                    }
                }

                return match && (adjacent || (adjacentCount > 0 && adjacentCount <= maxAdjacent));
            }
        }

        public static int ExecutePart2(int start, int end, int maxAdjacent)
        {
            return ExecutePart1(start, end, maxAdjacent);
        }
    }
}