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
            var muPlusLambda = new MuPlusLambdaImpl(plot);

            await muPlusLambda.Process(UpdateIteration, UpdateBestInIteration);
        }

        private void UpdateIteration(int iteration)
        {
            Iteration.Text = $"Iteracja: {iteration}";
        }

        private void UpdateBestInIteration(Individual individual)
        {
            BestInIteration.Text = $"Najlepszy wynik: {individual.F}";
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await Process();
        }
    }
}
