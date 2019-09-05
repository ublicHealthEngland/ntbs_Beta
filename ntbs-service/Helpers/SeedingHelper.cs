using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;

namespace ntbs_service.Helpers
{
    public static class SeedingHelper
    {
        public static List<T> GetRecordsFromCSV<T>(string pathToFile)
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, pathToFile);

            List<T> records = new List<T>();
            using (TextReader reader = File.OpenText(filePath)) {
                CsvReader csv = new CsvReader(reader);
                csv.Configuration.Delimiter = ",";
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.HeaderValidated = null;
                csv.Configuration.MissingFieldFound = null;
                while (csv.Read())
                {
                    T record = csv.GetRecord<T>();
                    records.Add(record);
                }
            }           
            return records;
        }
    }
}