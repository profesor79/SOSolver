// --------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Andrei.cs" company="">
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
    class Andrei
    {
        /// <summary>TODO The main.</summary>
        public static void Main234()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("test");

            var collection = database.GetCollection<Data>("Andrei");

            var minValue = new Data { TimeUtce = DateTime.MinValue };
            collection.InsertOne(minValue);

            Console.ReadLine();

            // problemSolved:  and new in mongo query
        }

        /// <summary>TODO The data.</summary>
        class Data
        {
            /// <summary>Gets or sets the id.</summary>
            public ObjectId Id { get; set; }

            /// <summary>Gets or sets the time utce.</summary>
            public DateTime TimeUtce { get; set; }
        }
    }

    // <summary>TODO The kalaimani data.</summary>
}

//http://stackoverflow.com/questions/38053557/mongo-find-method-does-not-work-for-datetime-minvalue
