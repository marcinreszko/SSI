using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KMeans
{
    public class KMeansModel
    {
        private const int GroupCount = 4;
        private const int ItersCount = 11;

        private double CalculateDistance(Value sample, Value center)
        {
            return Math.Sqrt(Math.Pow(sample.X - center.X, 2) + Math.Pow(sample.Y - center.Y, 2));
        }

        private List<Value> GetRandomCenterPoints(List<Value> values, int m)
        {
            var result = new List<Value>();

            var random = new Random();

            while (result.Count < m)
            {
                var index = random.Next(0, values.Count);

                var element = values.ElementAtOrDefault(index);

                if (element != null)
                {
                    result.Add(element);
                }
            }

            return result;
        }

        public async Task Invoke(List<Value> data, KMeansPlotViewModel plotViewModel, Action<string> onIterationUpdated)
        {
            var centerPoints = GetRandomCenterPoints(data, GroupCount);

            for (var iteration = 0; iteration < ItersCount; iteration++)
            {
                var groups = new Dictionary<int, List<Value>>();

                foreach (var sample in data)
                {
                    var distances = new List<double>();

                    foreach (var cp in centerPoints)
                    {
                        var distance = CalculateDistance(sample, cp);
                        distances.Add(distance);
                    }

                    var minDistance = distances.Min();
                    var minDistanceIndex = distances.IndexOf(minDistance);

                    if (groups.ContainsKey(minDistanceIndex))
                    {
                        groups[minDistanceIndex].Add(sample);
                    }
                    else
                    {
                        groups.Add(minDistanceIndex, new List<Value>() { sample });
                    }
                }

                centerPoints.Clear();

                foreach (var group in groups)
                {
                    centerPoints.Add(new Value
                    {
                        X = group.Value.Average(x => x.X),
                        Y = group.Value.Average(x => x.Y)
                    });
                }

                plotViewModel.UpdateCentroid(centerPoints);
                onIterationUpdated.Invoke($"Iteracja: {iteration + 1}");
                await Task.Delay(100);
            }
        }
    }
}
