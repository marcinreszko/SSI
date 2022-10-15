using System;
using System.Collections.Generic;
using System.Linq;

namespace MuPlusLambda
{
    public static class Tournament
    {
        public static Individual Get(List<Individual> parentPopulation, int tournamentSize, int mutationLevel)
        {
            var tournamentPopulation = new HashSet<Individual>();

            var random = new Random();
            while (tournamentPopulation.Count < tournamentSize)
            {
                var index = random.Next(parentPopulation.Count);

                var item = parentPopulation.ElementAtOrDefault(index);

                if (item is not null)
                {
                    tournamentPopulation.Add(item);
                }
            }

            var best = tournamentPopulation.Best();

            var x1Mutate = random.Next(-mutationLevel, mutationLevel);
            var x2Mutate = random.Next(-mutationLevel, mutationLevel);
            
            return best.Mutate(x1Mutate, x2Mutate);
        }
    }
}
