// --------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Flo - Copy.cs" company="">
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
    class Woozar
    {
        /// <summary>TODO The data.</summary>
        private static string data;

        /// <summary>TODO The main.</summary>
        public static void MainWoozar()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("test");

            var collection = database.GetCollection<Family>("wozar");

            // var f = GenerateFaimly();
            // collection.InsertOne(f);
            var filter = new FilterDefinitionBuilder<Family>();
            filter.Where(x => x.Id == new ObjectId("577ba98534780d45d0c80ec3")).ToBsonDocument();

            var document = collection.Find(x => x.Id == new ObjectId("577ba98534780d45d0c80ec3")).First();

            Console.WriteLine("ready");
            Console.ReadLine();
        }

        /// <summary>TODO The generate faimly.</summary>
        /// <returns>The <see cref="Family" />.</returns>
        /// <summary>TODO The child.</summary>
        [Serializable]
        public class Child
        {
            /// <summary>Gets or sets the d.</summary>
            public string d { get; set; }

            /// <summary>Gets or sets the date of birth.</summary>
            public DateTime dateOfBirth { get; set; }

            /// <summary>Gets or sets the given name.</summary>
            public string givenName { get; set; }

            /// <summary>Gets or sets the id.</summary>
            public ObjectId Id { get; set; }

            /// <summary>Gets or sets a value indicating whether is alive.</summary>
            public bool IsAlive { get; set; }
        }

        /// <summary>TODO The family.</summary>
        [Serializable]
        class Family
        {
            /// <summary>Gets or sets the children.</summary>
            public Child[] children { get; set; }

            /// <summary>Gets or sets the id.</summary>
            public ObjectId Id { get; set; }

            /// <summary>Gets or sets the name.</summary>
            public string name { get; set; }
        }
    }
}

//http://stackoverflow.com/questions/38053557/mongo-find-method-does-not-work-for-datetime-minvalue
