using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Collections.ObjectModel;

namespace MuPlusLambda
{
    public class PlotViewModel
    {
        public PlotModel Plot { get; }

        public ObservableCollection<DataPoint> ParentPopulation { get; set; } = new();

        public ObservableCollection<DataPoint> DescendantPopulation { get; set; } = new();

        public PlotViewModel()
        {
            Plot = new PlotModel
            {
                Title = "MuPlusLambda"
            };
            
            var bottomAxis = new LinearAxis
            {
                Minimum = 0,
                Maximum = 100,
                MajorGridlineStyle = LineStyle.Solid,
                MajorGridlineThickness = 1,
                MajorGridlineColor = OxyColors.Gray,
                Position = AxisPosition.Bottom
            };

            var leftAxis = new LinearAxis
            {
                Minimum = 0,
                Maximum = 100,
                MajorGridlineStyle = LineStyle.Solid,
                MajorGridlineThickness = 1,
                MajorGridlineColor = OxyColors.Gray,
                Position = AxisPosition.Left
            };

            Plot.Axes.Add(bottomAxis);
            Plot.Axes.Add(leftAxis);

            var parentPopulation = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerFill = OxyColors.Red,
                LineStyle = LineStyle.None,
                ItemsSource = ParentPopulation
            };

            var descendantPopulation = new LineSeries
            {
                MarkerType = MarkerType.Star,
                LineStyle = LineStyle.None,
                ItemsSource = DescendantPopulation,
                MarkerStroke = OxyColors.Black
            };

            Plot.Series.Add(parentPopulation);
            Plot.Series.Add(descendantPopulation);
        }
    }
}
