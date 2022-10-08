using KMeans;

var csvReader = new CsvReader();

var data = csvReader.GetData();

const int GroupCount = 4;
const int ItersCount = 4;

double CalculateDistance(Value sample, Value center)
{
    return Math.Sqrt(Math.Pow(sample.X - center.X, 2) + Math.Pow(sample.Y - center.Y, 2));
}

List<Value> GetRandomCenterPoints(List<Value> values, int m)
{
    var result = new List<Value>();

    var random = new Random();

    while (result.Count < m)
    {
        var index = random.Next();

        var element = values.ElementAtOrDefault(index);

        if (element != null)
        {
            result.Add(element);
        }
    }

    return result;
}


var centerPoints = GetRandomCenterPoints(data, GroupCount);

for (var iteration = 0; iteration < ItersCount; iteration++)
{
    break;
    Console.WriteLine($"ITERATION {iteration}");
    Console.WriteLine("Center points");
    centerPoints.Print();

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

    groups.Print();
    centerPoints.Clear();

    foreach (var group in groups)
    {
        centerPoints.Add(new Value
        {
            X = group.Value.Average(x => x.X),
            Y = group.Value.Average(x => x.Y)
        });
    }
    Console.WriteLine("");
}