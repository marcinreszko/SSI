using System;
using System.Collections.Generic;
using System.Linq;

namespace MuPlusLambda
{
    public static class Extensions
    {
        public static float F(this Individual individual)
        {
            var result = Math.Sin(individual.X1 * 0.05f)
                + Math.Sin(individual.X2 * 0.05f)
                + 0.4f * Math.Sin(individual.X1 * 0.15f) * Math.Sin(individual.X2 * 0.15f);

            return (float)result;
        }

        public static Individual Best(this IEnumerable<Individual> population)
        {
            return population.OrderByDescending(x => x.F).FirstOrDefault();
        }

        public static List<Individual> Best(this IEnumerable<Individual> population, int mu)
        {
            return population.OrderByDescending(x => x.F).Take(mu).ToList();
        }

        public static bool ContainsIndividual(this IEnumerable<Individual> population, Individual individual)
        {
            return population.Any(x => x.X1 == individual.X1 && x.X2 == individual.X2);
        }
    }
}
