// --------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="IrPunch.cs" company="">
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
        public static void IrPunchMain()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("test");

            var collection = database.GetCollection<InnerDocument>("irpunch");

            var aggregationDocument =
                collection.Aggregate()
                    .Match(x => x.LastUpdate > DateTime.Now.AddDays(-40))
                    .SortByDescending(x => x.LastUpdate)
                    .Group(BsonDocument.Parse("{ _id:'$Emp_ID', documents:{ '$push':'$$ROOT'}}"))
                    .Project<AggregationResult>(BsonDocument.Parse("{ _id:1, documents:{ $slice:['$documents', 3]}}"))
                    .ToList();

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
 http://stackoverflow.com/questions/37877377/select-last-n-documents-of-all-users-in-mongodb-collection-using-c-sharp/37885591#37885591
 I'm having a MongoDB Collection, I need to Select Last 3 documents (Order By Descending LastUpdate) of Each Employee (Emp_ID) using C# MongoDB Query.

The Sample Collection:

    {
        "_id" : ObjectId("575f4e2efd14481598fc0ebf"),
        "Emp_ID" : "100",
        "LastUpdate" : ISODate("2016-06-13T18:30:00.000Z")
    },
    {
        "_id" : ObjectId("575f4e2efd14481598fc0ec0"),
        "Emp_ID" : "101",
        "LastUpdate" : ISODate("2016-06-14T06:33:12.000Z")
    }
    ,
    {
        "_id" : ObjectId("575f4e2efd14481598fc0ec1"),
        "Emp_ID" : "101",
        "LastUpdate" : ISODate("2016-06-14T06:33:16.000Z")
    }
    ,
    {
        "_id" : ObjectId("575f4e2efd14481598fc0ec2"),
        "Emp_ID" : "102",
        "LastUpdate" : ISODate("2016-06-14T06:33:18.000Z")
    }
    ,
    {
        "_id" : ObjectId("575f4e2efd14481598fc0ec3"),
        "Emp_ID" : "100",
        "LastUpdate" : ISODate("2016-06-14T06:33:26.000Z")
    }
    ,
    {
        "_id" : ObjectId("575f4e2efd14481598fc0ec3"),
        "Emp_ID" : "102",
        "LastUpdate" : ISODate("2016-06-14T06:33:29.000Z")
    }
    ,
    {
        "_id" : ObjectId("575f4e2efd14481598fc0ec4"),
        "Emp_ID" : "101",
        "LastUpdate" : ISODate("2016-06-14T06:34:18.000Z")
    }
    ,
    {
        "_id" : ObjectId("575f4e2efd14481598fc0ec5"),
        "Emp_ID" : "102",
        "LastUpdate" : ISODate("2016-06-14T06:34:20.000Z")
    }
    ,
    {
        "_id" : ObjectId("575f4e2efd14481598fc0ec6"),
        "Emp_ID" : "100",
        "LastUpdate" : ISODate("2016-06-14T06:34:31.000Z")
    }
    ,
    {
        "_id" : ObjectId("575f4e2efd14481598fc0ec7"),
        "Emp_ID" : "102",
        "LastUpdate" : ISODate("2016-06-14T06:34:35.000Z")
    }
    ,
    {
        "_id" : ObjectId("575f4e2efd14481598fc0ec8"),
        "Emp_ID" : "101",
        "LastUpdate" : ISODate("2016-06-14T06:34:38.000Z")
    }

I know to select a single Employee info

The Experimental Query:

    var collection = _database.GetCollection<Employee>("EmpInfo");
    var filterBuilder = Builders<Employee>.Filter;
    var filter = filterBuilder.Eq("Emp_ID", "100");
    var Item = collection.Find(filter)
                         .Sort(Builders<Employee>.Sort.Descending("LastUpdate"))
                         .Limit(3).ToList();

Kindly assist me, how to Select last 3 records of every employee from the above collection using C# MongoDB Query (I'm preferring single query execution).
 */
