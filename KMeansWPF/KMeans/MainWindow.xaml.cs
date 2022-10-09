using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace KMeans
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Value> Items { get; }

        public MainWindow()
        {
            InitializeComponent();
            Items = CsvReader.GetData();

            var dataContext = (KMeansPlotViewModel)DataContext;

            dataContext.UpdateFileData(Items);
        }

        public async Task Process()
        {
            var dataContext = (KMeansPlotViewModel)DataContext;

            var kmeans = new KMeansModel();
            await kmeans.Invoke(Items, dataContext, UpdateIteration);
        }

        private void UpdateIteration(string value)
        {
            IterationText.Text = value;
        }
    }
}
