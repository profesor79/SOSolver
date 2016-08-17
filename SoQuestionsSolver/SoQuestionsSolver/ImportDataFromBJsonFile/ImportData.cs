using System;
using System.IO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace SoQuestionsSolver.ImportDataFromBJsonFile
{
    public static class ImportData
    {
        /// <summary>TODO The main.</summary>
        public static void Main()
        {
            var worker = new Processor();
            worker.ProcessData();
            Console.ReadLine();
        }

        /// <summary>TODO The processor.</summary>
        public class Processor
        {
            /// <summary>TODO The process data.</summary>
            public void ProcessData()
            {
                string line;
                using (TextReader file = File.OpenText("ImportDataFromBJsonFile\\a.json"))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        var bsonDocument = BsonDocument.Parse(line);
                        var myObj = BsonSerializer.Deserialize<Zxed>(bsonDocument);
                    }
                }
            }
        }
    }

    public class Zxed
    {
        public ObjectId _id;
        public string group { get; set; }
        public string key { get; set; }
        public string value { get; set; }
        public DateTime timestamp { get; set; }
    }
}