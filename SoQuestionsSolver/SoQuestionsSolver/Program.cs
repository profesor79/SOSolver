// --------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="">
//   
// </copyright>
// <summary>
//   TODO The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------------------------------
namespace ConsoleApplication2
{
    using System;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;
    using MongoDB.Driver;

    /// <summary>TODO The program.</summary>
    class Program
    {
        /// <summary>TODO The main.</summary>
        public static void Main()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("test");

            var collection = database.GetCollection<InnerDocument>("irpunch");
            

            var aggregationDocument = collection.Aggregate()
                .Match(x=>x.LastUpdate> DateTime.Now.AddDays(-40))
                .Sort(BsonDocument.Parse("{ LastUpdate:-1}"))
                .Group(BsonDocument.Parse("{ _id:'$Emp_ID', documents:{ '$push':'$$ROOT'}}"))
                .Project<AggregationResult>(BsonDocument.Parse("{ _id:1, documents:{ $slice:['$documents', 3]}}")).ToList()
                ;

            foreach (var aggregationResult in aggregationDocument)
            {
                foreach (var innerDocument in aggregationResult.documents)
                {
                    Console.WriteLine($"empID: {aggregationResult._id}, doc date: {innerDocument.LastUpdate}");
                }
                Console.WriteLine();
            }

            Console.ReadLine();
        }


        public class AggregationResult
        {
            public int _id { get; set; }
            public InnerDocument[] documents { get; set; }
        }

        public class InnerDocument
        {
            public ObjectId Id { get; set; }
            public string Emp_ID { get; set; }
            public DateTime LastUpdate { get; set; }
        }

    }
}
