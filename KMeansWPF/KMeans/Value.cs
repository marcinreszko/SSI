using OxyPlot;

namespace KMeans
{
    public class Value
    {
        public double X { get; set; }

        public double Y { get; set; }

        public DataPoint ToDataPoint()
            => new(X, Y);
    }
}
