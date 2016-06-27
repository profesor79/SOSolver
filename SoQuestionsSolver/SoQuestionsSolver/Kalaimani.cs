// --------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Ricardomsouto - Copy.cs" company="">
//   
// </copyright>
// <summary>
//   TODO The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------------------------------
namespace ConsoleApplication2
{
    using System;
    using System.Linq;

    using MongoDB.Bson;
    using MongoDB.Driver;

    /// <summary>TODO The program.</summary>
    class Kalaimani
    {
        /// <summary>TODO The main.</summary>
        public static void Main56()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("test");

            var collection = database.GetCollection<KalaimaniData>("kalaimani");

            // create array to inser
            var arrayToInsert = new[] { 1, 4, 5, 6 };
            var arrayToMerge = new[] { 2, 3, 4, 5 };
            var arrayExpected = new[] { 1, 4, 5, 6, 2, 3 };

            var doc = new KalaimaniData { Numbers = arrayToInsert };

            collection.InsertOne(doc);

            var filter = Builders<KalaimaniData>.Filter.Eq(x => x.Id, doc.Id);

            var updateDef = new UpdateDefinitionBuilder<KalaimaniData>().AddToSetEach(x => x.Numbers, arrayToMerge);

            collection.UpdateOne(filter, updateDef);

            // retrive and compare
            var changed = collection.Find(filter).First();

            var matched = 0;
            foreach (var element in arrayExpected)
            {
                if (changed.Numbers.Contains(element))
                {
                    matched++;
                }
            }

            if (changed.Numbers.Length == matched)
            {
                Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine("NOK");
            }

            Console.ReadLine();
        }

        /// <summary>TODO The kalaimani data.</summary>
        class KalaimaniData
        {
            /// <summary>Gets or sets the id.</summary>
            public ObjectId Id { get; set; }

            /// <summary>Gets or sets the numbers.</summary>
            public int[] Numbers { get; set; }
        }
    }

    // <summary>TODO The kalaimani data.</summary>
}
//http://stackoverflow.com/questions/38050078/equivalent-for-setunion-in-c-sharp-driver
