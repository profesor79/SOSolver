// --------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Lapsus.cs" company="">
//   
// </copyright>
// <summary>
//   TODO The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------------------------------
namespace ConsoleApplication2
{
    using System;

    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Driver;

    /// <summary>TODO The program.</summary>
    class Lapsus
    {
        /// <summary>TODO The main.</summary>
        public static async void Main()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("test");

            var collection = database.GetCollection<MongoDbRepositoryItem>("Lapsus");

            var builder = Builders<MongoDbRepositoryItem>.Filter;

            // I didn't read about sorting yet. I also need the "oldest" document.
            var filter = builder.Exists(item => item.ProcessingStatus, false);

            var result = await collection.FindAsync<MongoDbRepositoryItem>(filter);

            var data = result.ToList();
            Console.ReadLine();

            // problemSolved:  and new in mongo query
        }

        /// <summary>TODO The mongo db repository item.</summary>
        internal class MongoDbRepositoryItem
        {
            /// <summary>Gets or sets the id.</summary>
            [BsonId]
            public string Id { get; set; }

            /// <summary>Gets or sets the payload.</summary>
            [BsonElement("rawdata")]
            public byte[] Payload { get; set; }

            /// <summary>Gets or sets the processing attempts.</summary>
            public int ProcessingAttempts { get; set; }

            /// <summary>Gets or sets the processing started.</summary>
            public DateTime ProcessingStarted { get; set; }

            /// <summary>Gets or sets the processing status.</summary>
            public string ProcessingStatus { get; set; }
        }
    }

    // <summary>TODO The kalaimani data.</summary>
}

//http://stackoverflow.com/questions/38053557/mongo-find-method-does-not-work-for-datetime-minvalue
