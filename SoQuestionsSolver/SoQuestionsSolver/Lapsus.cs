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
        public static void M3ain()
        {
            Main2();
        }

        /// <summary>TODO The main 2.</summary>
        public static async void Main2()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("test");

            var collection = database.GetCollection<MongoDbRepositoryItem>("Lapsus");

            var builder = Builders<MongoDbRepositoryItem>.Filter;
            var b2 = new FilterDefinitionBuilder<MongoDbRepositoryItem>();
            var f2 = b2.Exists(x => x.ProcessingStatus, false);
            var result2 = await collection.FindAsync<MongoDbRepositoryItem>(f2);

            // I didn't read about sorting yet. I also need the "oldest" document.
            var filter = builder.Exists(item => item.ProcessingStatus, false);

            try
            {
                var result = collection.FindAsync<MongoDbRepositoryItem>(filter).Result;
                var data = result.ToList();
            }
            catch (Exception)
            {
                throw;
            }

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
