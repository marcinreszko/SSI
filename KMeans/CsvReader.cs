namespace KMeans
{
    public class CsvReader
    {
        public List<Value> GetData()
        {
            var values = new List<Value>();

            using (var reader = new StreamReader(@"spirala.txt"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    var valuesString = line.Split(";");

                    values.Add(new Value
                    {
                        X = Convert.ToDouble(valuesString[0].Replace(".", ",")),
                        Y = Convert.ToDouble(valuesString[1].Replace(".", ","))
                    });
                }
            }

            return values;
        }
    }

    public class Value
    {
        public double X { get; set; }

        public double Y { get; set; }

        public override string ToString()
        {
            return $"x = {X}, y = {Y}";
        }
    }

    public static class Extensions
    {
        public static void Print(this List<Value> values)
        {
            foreach (var value in values)
            {
                Console.WriteLine(value.ToString());
            }
        }

        public static void Print(this Dictionary<int, List<Value>> dict)
        {
            foreach (var value in dict)
            {
                var valuesString = string.Join("\n", value.Value
                    .OrderByDescending(x => x.X)
                    .Select(x => x.ToString())
                    .ToArray());

                Console.WriteLine($"Key = {value.Key}, Values = [\n{valuesString}\n]");
            }
        }
    }
}
