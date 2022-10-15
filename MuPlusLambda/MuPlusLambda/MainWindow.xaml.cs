using System.Threading.Tasks;
using System.Windows;

namespace MuPlusLambda
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public async Task Process()
        {
            var plot = (PlotViewModel)DataContext;
            var muPlusLambda = new MuPlusLambdaImpl(plot)
            {
                BestIndividualFound = UpdateBestIndividual,
                IterationUpdated = UpdateIteration,
            };
            
            await muPlusLambda.Process(Dispatcher);
        }

        private void UpdateIteration(int iteration)
        {
            Iteration.Text = $"Iteracja: {iteration}";
        }

        private void UpdateBestIndividual(Individual individual)
        {
            BestInIteration.Text = $"Najlepszy wynik: {individual.F}";
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await Process();
        }
    }
}
