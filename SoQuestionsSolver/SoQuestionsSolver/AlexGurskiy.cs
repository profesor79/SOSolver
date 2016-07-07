// --------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="AlexGurskiy.cs" company="">
//   
// </copyright>
// <summary>
//   TODO The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------------------------------
namespace ConsoleApplication2
{
    using System;
    using System.Collections.Generic;

    using MongoDB.Bson;
    using MongoDB.Driver;

    /// <summary>TODO The program.</summary>
    class AlexGurskiy
    {
        /// <summary>TODO The main.</summary>
        public static void Main()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("test");

            var collection = database.GetCollection<Product>("AlexGurskiy");

            var aggregate =
                collection.Aggregate()
                    .Match(x => x._id == "some id here")
                    .Unwind("Items")
                    .Skip(3)
                    .Limit(3)
                    .Group(BsonDocument.Parse("{_id:{id:'$_id', ItemsCount:'$ItemsCount' },Items:{$push:'$Items'} }"))
                    .Project<Product>(BsonDocument.Parse("{_id:'$_id.id', ItemsCount:'$_id.ItemsCount', Items:1  }"))
                    .ToList();

            Console.ReadLine();
        }

        /// <summary>TODO The item.</summary>
        public class Item
        {
            /// <summary>Gets or sets the name.</summary>
            public string Name { get; set; }
        }

        /// <summary>TODO The product.</summary>
        public class Product
        {
            /// <summary>Gets or sets the _id.</summary>
            public string _id { get; set; }

            /// <summary>Gets or sets the items.</summary>
            public IEnumerable<Item> Items { get; set; }

            /// <summary>Gets or sets the items count.</summary>
            public int ItemsCount { get; set; }
        }
    }
}
