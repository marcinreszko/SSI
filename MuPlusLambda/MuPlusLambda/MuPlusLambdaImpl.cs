using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

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
                MarkerFill = OxyColors.DeepSkyBlue,
                LineStyle = LineStyle.None,
                ItemsSource = ParentPopulation
            };

            var descendantPopulation = new LineSeries
            {
                MarkerType = MarkerType.Star,
                LineStyle = LineStyle.None,
                ItemsSource = DescendantPopulation,
                MarkerStroke = OxyColors.Yellow
            };

            Plot.Series.Add(parentPopulation);
            Plot.Series.Add(descendantPopulation);
        }
    }

    public class MuPlusLambdaImpl
    {
        private readonly MuPlusLambdaPopulationGenerator _populationGenerator;
        const int InterationsCount = 20;
        const int _mu = 4;
        const int _lambda = 10;
        const int _tournamentSize = 2;
        const int _mutationLevel = 10;
        const int _delay = 2000;

        public PlotViewModel PlotVm { get; }

        public MuPlusLambdaImpl(PlotViewModel plot)
        {
            _populationGenerator = new MuPlusLambdaPopulationGenerator();
            PlotVm = plot;
        }

        public async Task Process(Action<int> updateIteration, Action<Individual> updateBestInIteration)
        {
            var parentPopulation = _populationGenerator.GeneratePopulation(_mu);
            var descendantPopulation = new List<Individual>();
            DrawParentPopulation(parentPopulation);

            await Task.Delay(_delay);
            for (var i = 0; i < InterationsCount; i++)
            {
                var wholePopulation = parentPopulation.ToList();

                while (descendantPopulation.Count < _lambda)
                {
                    var individual = Tournament.Get(parentPopulation, _tournamentSize, _mutationLevel);

                    if (!wholePopulation.ContainsIndividual(individual)
                        || !descendantPopulation.ContainsIndividual(individual))
                    {
                        wholePopulation.Add(individual);
                        descendantPopulation.Add(individual);
                    }
                }

                parentPopulation = wholePopulation.Best(_mu);

                var bestInPopulation = parentPopulation.Best();

                DrawParentPopulation(parentPopulation);
                DrawDescendantPopulation(descendantPopulation);
                updateIteration.Invoke(i + 1);
                updateBestInIteration.Invoke(bestInPopulation);

                await Task.Delay(_delay);

                descendantPopulation.Clear();
            }
        }

        private void DrawParentPopulation(List<Individual> population)
        {
            PlotVm.ParentPopulation.Clear();

            foreach (var individual in population)
            {
                PlotVm.ParentPopulation.Add(individual.ToDataPoint());
            }

            PlotVm.Plot.InvalidatePlot(true);
        }

        private void DrawDescendantPopulation(List<Individual> population)
        {
            PlotVm.DescendantPopulation.Clear();

            foreach (var individual in population)
            {
                PlotVm.DescendantPopulation.Add(individual.ToDataPoint());
            }

            PlotVm.Plot.InvalidatePlot(true);
        }
    }
}
