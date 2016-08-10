// --------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="user5510044.cs" company="">
//   
// </copyright>
// <summary>
//   TODO The user 5510044.
// </summary>
// --------------------------------------------------------------------------------------------------------------------------------------------
namespace SoQuestionsSolver.DirtyOnes
{
    using System.Collections.Generic;
    using System.Linq;

    using MongoDB.Bson;
    using MongoDB.Driver;

    /// <summary>TODO The user 5510044.</summary>
    public class User5510044
    {
        /// <summary>TODO The aa.</summary>
        public void Aa()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<Interactions>("Interactions");

            var newItem = new Interactions
                              {
                                  SiteName = "Example", 
                                  Pages =
                                      new List<Pages>
                                          {
                                              new Pages
                                                  {
                                                      Url = @"http://stackoverflow.com/documentation/mongodb-csharp", 
                                                      VisitPageIndex = 4
                                                  }, 
                                              new Pages { Url = @"https://github.com/", VisitPageIndex = 2 }
                                          }
                              };
            collection.InsertOne(newItem);

            var result = IMongoCollectionExtensions.AsQueryable(collection).FirstOrDefault(s => s.SiteName == "Example");

            var update = Builders<Interactions>.Update.Set(s => s.SiteName, "New Example");

            IMongoCollectionExtensions.FindOneAndUpdate(collection, s => s.SiteName == "Example", update);
            IMongoCollectionExtensions.DeleteOne(collection, s => s.SiteName == "New Example");

            /*
             * from mongo driver
             var result = Group(x => x.A, g => new RootView { Property = g.Key, Field = g.First().B });
            result.Projection.Should().Be("{ _id: \"$A\", Field: { \"$first\" : \"$B\" } }"
             
            var result2 = IAggregateFluentExtensions.Unwind(IMongoCollectionExtensions.Aggregate(collection), e => e.Pages)
                .Group<Pages>(p => p.Pages.url, e => new Pages { Url = e.Key, VisitPageIndex = e.Count() });

            // var resurt = collection.Aggregate<BsonDocument>().Unwind<BsonDocument>(unwind)
            */
        }

        /// <summary>TODO The interactions.</summary>
        public class Interactions
        {
            /// <summary>Gets or sets the channel id.</summary>
            public string ChannelId { get; set; }

            /// <summary>Gets or sets the contact id.</summary>
            public string ContactId { get; set; }

            /// <summary>Gets or sets the id.</summary>
            public ObjectId Id { get; set; }

            /// <summary>Gets or sets the language.</summary>
            public string Language { get; set; }

            /// <summary>Gets or sets the pages.</summary>
            public List<Pages> Pages { get; set; }

            /// <summary>Gets or sets the site name.</summary>
            public string SiteName { get; set; }

            /// <summary>Gets or sets the value.</summary>
            public int Value { get; set; }

            /// <summary>Gets or sets the visit page count.</summary>
            public int VisitPageCount { get; set; }
        }

        /// <summary>TODO The pages.</summary>
        public class Pages
        {
            /// <summary>Gets or sets the url.</summary>
            public string Url { get; set; }

            /// <summary>Gets or sets the visit page index.</summary>
            public int VisitPageIndex { get; set; }
        }
    }
}
