using System;
using System.Collections.Generic;
using System.Linq;

namespace MuPlusLambda
{
    public class MuPlusLambdaPopulationGenerator
    {
        private const int MinValue = 0;
        private const int MaxValue = 100;

        public List<Individual> GeneratePopulation(int count)
        {
            float X1, X2;

            var rnd = new Random();
            var population = new HashSet<Individual>();

            while (population.Count <= count)
            {
                X1 = rnd.Next(MinValue, MaxValue);
                X2 = rnd.Next(MinValue, MaxValue);

                var individual = new Individual(X1, X2);

                population.Add(individual);
            }

            return population.ToList();
        }
    }
}
