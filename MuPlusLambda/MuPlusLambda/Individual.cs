using OxyPlot;

namespace MuPlusLambda
{
    public class Individual
    {
        public Individual(float x1, float x2)
        {
            X1 = x1;
            X2 = x2;
            F = this.F();
        }

        public float X1 { get; set; }

        public float X2 { get; set; }

        public float F { get; set; }

        public Individual Mutate(int x1, int x2)
        {
            var newX1 = CalculateNewParameterValue(X1, x1);
            var newX2 = CalculateNewParameterValue(X2, x2);
            return new(newX1, newX2);
        }

        private static float CalculateNewParameterValue(float oldParameterValue, int  mutationChange)
        {
            if (oldParameterValue + mutationChange < 0 || oldParameterValue + mutationChange > 100)
            {
                return oldParameterValue - mutationChange;
            }

            return oldParameterValue + mutationChange;
        }

        public DataPoint ToDataPoint()
        {
            return new DataPoint(X1, X2);
        }
    }
}
