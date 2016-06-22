// --------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Hammer.cs" company="">
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
    class Program
    {
        /// <summary>TODO The main.</summary>
        public static void Main()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("test");

            var collection = database.GetCollection<BsonDocument>("hammer");

            var project =
                BsonDocument.Parse(
                    "{_id: 1,address: 1,borough: 1,cuisine: 1,grades: 1,name: 1,restaurant_id: 1,year: {$year: '$grades.date'}}");

            var aggregationDocument =
                collection.Aggregate()
                    .Unwind("grades")
                    .Project(project)
                    .Match(BsonDocument.Parse("{'year' : {$in : [2013, 2015]}}"))
                    .ToList();

            foreach (var result in aggregationDocument)
            {
                Console.WriteLine(result.ToString());
            }

            // using $projection
            Console.ReadLine();
        }

        /// <summary>TODO The aggregation result.</summary>
        public class AggregationResult
        {
            /// <summary>Gets or sets the _id.</summary>
            public int _id { get; set; }

            /// <summary>Gets or sets the documents.</summary>
            public InnerDocument[] documents { get; set; }
        }

        /// <summary>TODO The inner document.</summary>
        public class InnerDocument
        {
            /// <summary>Gets or sets the emp_ id.</summary>
            public string Emp_ID { get; set; }

            /// <summary>Gets or sets the id.</summary>
            public ObjectId Id { get; set; }

            /// <summary>Gets or sets the last update.</summary>
            public DateTime LastUpdate { get; set; }
        }
    }
}

/*
 * 
 http://stackoverflow.com/questions/37956678/mongodb-query-date-by-year-c-sharp/37967736#37967736
 Having such a document:

    var document = new BsonDocument
    {
        { "address" , new BsonDocument
            {
                { "street", "2 Avenue" },
                { "zipcode", "10075" },
                { "building", "1480" },
                { "coord", new BsonArray { 73.9557413, 40.7720266 } }
            }
        },
        { "borough", "Manhattan" },
        { "cuisine", "Italian" },
        { "grades", new BsonArray
            {
                new BsonDocument
                {
                    { "date", new DateTime(2014, 10, 1, 0, 0, 0, DateTimeKind.Utc) },
                    { "grade", "A" },
                    { "score", 11 }
                },
                new BsonDocument
                {
                    { "date", new DateTime(2014, 1, 6, 0, 0, 0, DateTimeKind.Utc) },
                    { "grade", "B" },
                    { "score", 17 }
                }
            }
        },
        { "name", "Vella" },
        { "restaurant_id", "41704620" }
    };

How would I query for grades.date.year = 2016?

Was trying:

    var filter = Builders<BsonDocument>.Filter.Eq("grades.date.year", 2016);
    var result = await collection.Find(filter).ToListAsync();

But I guess dot notation only works on the json doc, not the objects? Scoured the internet, but couldn't find a clean example.
 */
