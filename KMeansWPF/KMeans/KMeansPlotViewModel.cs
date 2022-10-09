using OxyPlot;
using OxyPlot.Series;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

            var fileData = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerFill = OxyColor.FromRgb(0, 255, 0),
                LineStyle = LineStyle.None,
                ItemsSource = FileData,
            };

            var centroids = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                LineStyle = LineStyle.None,
                MarkerFill = OxyColor.FromRgb(255, 0, 0),
                ItemsSource = Centroids
            };

            Plot.Series.Add(fileData);
            Plot.Series.Add(centroids);

            Centroids.CollectionChanged += (_, _) => Plot.InvalidatePlot(true);
        }

        public ObservableCollection<DataPoint> FileData { get; set; } = new ObservableCollection<DataPoint>();

        public ObservableCollection<DataPoint> Centroids { get; set; } = new ObservableCollection<DataPoint>();

        public void UpdateCentroid(IEnumerable<Value> values)
        {
            Centroids.Clear();

            foreach (var value in values)
            {
                Centroids.Add(value.ToDataPoint());
            }
        }

        public void UpdateFileData(IEnumerable<Value> values)
        {
            foreach (var value in values)
            {
                FileData.Add(value.ToDataPoint());
            }

            Plot.InvalidatePlot(true);
        }
    }
}
