// --------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Boas.cs" company="">
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
    using MongoDB.Driver;

    /// <summary>TODO The program.</summary>
    class Boas
    {
        /// <summary>TODO The main.</summary>
        public static void Main3()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("test");

            var collection = database.GetCollection<Boas1>("Boas");

            var count = collection.Find(new BsonDocument()).Count();
            var stepSize = 1000;

            for (var i = 0; i < Math.Ceiling((double)count / stepSize); i++)
            {
                var list = collection.Find(new BsonDocument()).Skip(i * stepSize).Limit(stepSize).ToList();

                // process the 1000 ...
            }

            var project =
                BsonDocument.Parse(
                    "{Key:1, Timestamp:1, year:{$year:'$Timestamp'}, dayOfYear:{$dayOfYear:'$Timestamp'}}");
            var group = BsonDocument.Parse("{_id:{year:'$year', dayOfYear:'$dayOfYear'}, count:{$sum:1}}");
            var result = collection.Aggregate().Project(x => new { x }).Group(group).ToList();

            // .Project(i => new { i.Key, dayOfYear = i.Timestamp.GetDayOfYear,  year = i.Timestamp.GetYear })
            Console.ReadLine();
        }

        /// <summary>TODO The insert document.</summary>
        /// <param name="collection">TODO The collection.</param>
        private static void InsertDocument(IMongoCollection<Test1> collection)
        {
            for (var i = 0; i < 50; i++)
            {
                var doc = new Test1
                              {
                                  group = "g" + i / 10, 
                                  key = "k1" + i / 5, 
                                  value = "v1" + i / 2, 
                                  timestamp = DateTime.Now.AddDays(-(i / 3))
                              };
                collection.InsertOne(doc);
            }
        }
    }

    /// <summary>TODO The boas 1.</summary>
    class Boas1
    {
        /// <summary>Gets or sets the group.</summary>
        public string group { get; set; }

        /// <summary>Gets or sets the id.</summary>
        public ObjectId Id { get; set; }

        /// <summary>Gets or sets the key.</summary>
        public string Key { get; set; }

        /// <summary>Gets or sets the timestamp.</summary>
        public DateTime Timestamp { get; set; }

        /// <summary>Gets or sets the value.</summary>
        public string value { get; set; }
    }
}
