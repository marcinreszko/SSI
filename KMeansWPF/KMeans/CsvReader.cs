using System;
using System.Collections.Generic;
using System.IO;

namespace KMeans
{
    public static class CsvReader
    {
        public static List<Value> GetData()
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
}
