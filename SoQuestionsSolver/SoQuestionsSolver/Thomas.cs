// --------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Thomas.cs" company="">
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
    using MongoDB.Bson.Serialization;
    using MongoDB.Driver;

    /// <summary>TODO The program.</summary>
    class Thomas
    {
        /// <summary>TODO The main.</summary>
        public static void Main()
        {
            BsonClassMap.RegisterClassMap<Child>(
                cm =>
                    {
                        cm.AutoMap();
                        cm.SetIgnoreExtraElements(true);
                    });
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("test");

            var collection = database.GetCollection<Family>("thomas");

            var f = new Family
                        {
                            Id = ObjectId.GenerateNewId(), 
                            name = "MyFamily " + DateTime.Now, 
                            children =
                                new[]
                                    {
                                        new Child
                                            {
                                                Id = ObjectId.GenerateNewId(), 
                                                givenName = "Child 1" + DateTime.Now.AddDays(-800), 
                                                dateOfBirth = DateTime.Now.AddDays(-800), 
                                                IsAlive = true
                                            }, 
                                        new Child
                                            {
                                                Id = ObjectId.GenerateNewId(), 
                                                givenName = "Child 2" + DateTime.Now.AddDays(-1800), 
                                                dateOfBirth = DateTime.Now.AddDays(-1800), 
                                                IsAlive = true
                                            }, 
                                        new Child
                                            {
                                                Id = ObjectId.GenerateNewId(), 
                                                givenName = "Child 3" + DateTime.Now.AddDays(-900), 
                                                dateOfBirth = DateTime.Now.AddDays(-900), 
                                                IsAlive = false
                                            }, 
                                        new Child
                                            {
                                                Id = ObjectId.GenerateNewId(), 
                                                givenName = "Child 4" + DateTime.Now.AddDays(-2800), 
                                                dateOfBirth = DateTime.Now.AddDays(-2800), 
                                                IsAlive = true
                                            }, 
                                        new Child
                                            {
                                                Id = ObjectId.GenerateNewId(), 
                                                givenName = "Child 5" + DateTime.Now.AddDays(-822), 
                                                dateOfBirth = DateTime.Now.AddDays(-822), 
                                                IsAlive = false
                                            }
                                    }
                        };
            collection.InsertOne(f);

            var sort = BsonDocument.Parse("{\"kids.dateOfBirth\": -1}"); // get the youngest 
            var project =
                BsonDocument.Parse(
                    "{_id:'$children._id', dateOfBirth:'$children.dateOfBirth', givenName:'$children.givenName', IsAlive:'$children.IsAlive'}");
            var aggregate = collection.Aggregate().Match(x => x.Id == f.Id)

                // .Project(x => new { kids = x.children })
                .Unwind("children").Sort(sort).Limit(1).Project<Child>(project);

            Console.WriteLine(aggregate.ToString());

            var result = aggregate.FirstOrDefault();

            var filterDef = new FilterDefinitionBuilder<Family>();
            var filter = filterDef.Eq(x => x.Id, f.Id);
            var projectDef = new ProjectionDefinitionBuilder<Family>();
            var projection = projectDef.ElemMatch<Child>("Children", "{IsAlive:true}");

            var kids = collection.Find(filter).Project<Family>(projection).First();

            var child2 = kids.children[0];
        }

        /// <summary>TODO The child.</summary>
        public class Child
        {
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
