using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Collections.Generic;
using System.Linq;

namespace KMeans
{
    public class KMeansPlotViewModel
    {
        public PlotModel Plot { get; }

        public KMeansPlotViewModel()
        {
            Plot = new PlotModel
            {
                Title = "KMeans"
            };

            var customAxis = GetColorAxis();
            customAxis.AddRange(0, 0.1, OxyColors.Red);
            customAxis.AddRange(0.1, 0.2, OxyColors.Black);
            customAxis.AddRange(0.2, 0.3, OxyColors.Green);
            customAxis.AddRange(0.3, 1, OxyColors.Orange);
            customAxis.AddRange(1, 1.1, OxyColors.Blue);
            Plot.Axes.Add(customAxis);

            var scatter = new ScatterSeries 
            { 
                ColorAxisKey = customAxis.Key 
            };
            Plot.Series.Add(scatter);

            Plot.InvalidatePlot(true);
        }

        public IEnumerable<Value> FileData { get; set; }

        public void UpdateCentroid(IEnumerable<Value> centroids)
        {
            Plot.Series.Clear();

            var colorAxis = GetColorAxis();

            var points = new ScatterSeries 
            { 
                ColorAxisKey = colorAxis.Key,
                MarkerSize = 16,
                MarkerType = MarkerType.Circle
            };

            int colorIdx = 0;
            foreach (var centroid in centroids.OrderBy(x => x.X))
            {
                var color = _colorRanges.ElementAt(colorIdx);

                points.Points.Add(new ScatterPoint(centroid.X, centroid.Y, 3, color));

                foreach (var filePoint in FileData.Where(x => x.X >= centroid.X).ToList())
                {
                    points.Points.Add(new ScatterPoint(filePoint.X, filePoint.Y, 2, color));
                }

                colorIdx++;
            }

            Plot.Axes.Add(colorAxis);
            Plot.Series.Add(points);

            Plot.InvalidatePlot(true);
        }

        public void UpdateFileData(IEnumerable<Value> values)
        {
            FileData = values;
        }

        private readonly double[] _colorRanges =
        {
            0,
            0.1,
            0.2,
            0.3,
            0.4
        };

        private RangeColorAxis GetColorAxis()
        {
            return new RangeColorAxis
            {
                Key = "colors"
            };
        }
    }
}
