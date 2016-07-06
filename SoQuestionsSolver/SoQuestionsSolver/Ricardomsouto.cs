// --------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Hammer.cs" company="">
//   
// </copyright>
// <summary>
//   TODO The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace ConsoleApplication2
{
    using System;

    using MongoDB.Bson;
    using MongoDB.Driver;

    /// <summary>TODO The program.</summary>
    class Program
    {
        /// <summary>TODO The main.</summary>
        public static void MainRichard()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("test");

            var collection = database.GetCollection<Test1>("Ricardomsouto");


            // InsertDocument(collection);
            var aggregate = collection.Aggregate()
                .Match(x=>x.key== "k10")
                .SortByDescending(x=>x.timestamp)
               .Group( BsonDocument.Parse("{ '_id':'$group',  'latestvalue':{$first:'$value'} }")).ToList();


            Console.ReadLine();
        }

        private static void InsertDocument(IMongoCollection<Test1> collection)
        {
            for (int i = 0; i <50; i++)
            {
                var doc = new Test1
                {
                    group = "g" + (i / 10),
                    key = "k1" + (i / 5),
                    value = "v1" + (i / 2),
                    timestamp = DateTime.Now.AddDays(-(i/3))
                };
                collection.InsertOne(doc);
            }

        }
    }

    
    class Test1
    {
        public ObjectId Id { get; set; }
        public string group { get; set; }
        public string key { get; set; }
        public string value { get; set; }
        public DateTime timestamp { get; set; }
    }

    
}
