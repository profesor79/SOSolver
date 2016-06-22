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
        public static void Main()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("test");

            var collection = database.GetCollection<TestX>("hammer");

            
            //InsertDocument(collection);

            //var project =
            //    BsonDocument.Parse(
            //        "{_id: 1,address: 1,borough: 1,cuisine: 1,grades: 1,name: 1,restaurant_id: 1,year: {$year: '$grades.date'}}");

            var aggregationDocument =
                collection.Aggregate<TestX>()
                    .Unwind<TestX>(x=>x.grades)
                    .Match(BsonDocument.Parse("{$and:[{'grades.date':{$gte: ISODate('2012-01-01')}},{'grades.date':{$lt: ISODate('2013-01-01')}}]}"))
                    .ToList();


            foreach (var result in aggregationDocument)
            {
                 
                Console.WriteLine(result.ToString());
            }

            
            Console.ReadLine();
        }

        private static void InsertDocument(IMongoCollection<BsonDocument> collection)
        {
            var document = new BsonDocument
            {
                {
                    "address", new BsonDocument
                    {
                        {"street", "2 Avenue"},
                        {"zipcode", "10075"},
                        {"building", "1480"},
                        {"coord", new BsonArray {73.9557413, 40.7720266}}
                    }
                },
                {"borough", "Manhattan"},
                {"cuisine", "Italian"},
                {
                    "grades", new BsonArray
                    {
                        new BsonDocument
                        {
                            {"date", new DateTime(2015, 10, 1, 0, 0, 0, DateTimeKind.Utc)},
                            {"grade", "A"},
                            {"score", 11}
                        },
                        new BsonDocument
                        {
                            {"date", new DateTime(2012, 1, 6, 0, 0, 0, DateTimeKind.Utc)},
                            {"grade", "B"},
                            {"score", 17}
                        }
                    }
                },
                {"name", "Vella"},
                {"restaurant_id", "41704620"}
            };

            collection.InsertOne(document);
        }
    }

    class Address
    {
        public string street { get; set; }
        public string zipcode { get; set; }
        public string building { get; set; }
        public IEnumerable<double> coord { get; set; }
    }

    class Grade
    {
        public DateTime date { get; set; }
        public string grade { get; set; }
        public int score { get; set; }
    }

    class TestX
    {
        public ObjectId _id { get; set; }
        public Address address { get; set; }
        public string borough { get; set; }
        public string cuisine { get; set; }
        public IEnumerable<Grade> grades { get; set; }
        public string name { get; set; }
        public string restaurant_id { get; set; }
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
