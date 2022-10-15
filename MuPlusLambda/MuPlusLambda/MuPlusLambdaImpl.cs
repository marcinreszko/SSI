using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MuPlusLambda
{
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

        public Action<int> IterationUpdated { get; set; }

        public Action<Individual> BestIndividualFound { get; set; }

        public async Task Process(Dispatcher dispatcher)
        {
            var parentPopulation = _populationGenerator.GeneratePopulation(_mu);
            var descendantPopulation = new List<Individual>();
            DrawParentPopulation(parentPopulation);

            await Task.Delay(_delay);

            await Task.Factory.StartNew(async () =>
            {
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
                    dispatcher.Invoke(() =>
                    {
                        IterationUpdated.Invoke(i + 1);
                        BestIndividualFound.Invoke(bestInPopulation);
                    });
                    
                    await Task.Delay(_delay);

                    descendantPopulation.Clear();
                }
            });
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
