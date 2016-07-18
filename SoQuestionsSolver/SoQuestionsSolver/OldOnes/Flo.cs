// --------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Flo.cs" company="">
//   
// </copyright>
// <summary>
//   TODO The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------------------------------
namespace ConsoleApplication2
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;

    using MongoDB.Bson;
    using MongoDB.Driver;

    /// <summary>TODO The program.</summary>
    class Flo
    {
        /// <summary>TODO The data.</summary>
        private static string data;

        /// <summary>TODO The main.</summary>
        public static void Main23()
        {
            GenerateStringData();
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("test");

            var collection = database.GetCollection<Family>("thomas");

            // var f = GenerateFaimly();
            // collection.InsertOne(f);
            var filter = new FilterDefinitionBuilder<Family>();
            filter.Where(x => x.Id == new ObjectId("577ba98534780d45d0c80ec3")).ToBsonDocument();

            var document = collection.Find(x => x.Id == new ObjectId("577ba98534780d45d0c80ec3")).First();

            // PushDocumentsToArray(size, f, collection);
            Console.WriteLine("ready");
            Console.ReadLine();
        }

        /// <summary>TODO The generate faimly.</summary>
        /// <returns>The <see cref="Family" />.</returns>
        private static Family GenerateFaimly()
        {
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
            return f;
        }

        /// <summary>TODO The generate string data.</summary>
        private static void GenerateStringData()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < 500; i++)
            {
                sb.AppendLine(i.ToString());
            }

            data = sb.ToString();
        }

        /// <summary>TODO The push documents to array.</summary>
        /// <param name="size">TODO The size.</param>
        /// <param name="f">TODO The f.</param>
        /// <param name="collection">TODO The collection.</param>
        private static void PushDocumentsToArray(long size, Family f, IMongoCollection<Family> collection)
        {
            while (size <= 16777216)
            {
                var kid = new Child
                              {
                                  Id = ObjectId.GenerateNewId(), 
                                  givenName =
                                      "Child 1skdjfilahfuiaehfa feh fh fu hfuh fhsadhldiufhkjl ah hc hafha fwebffhsrhddhv  hfhfhwehfahfkjhewkhj32xjrjciojeacehf 4 cq43 rb4jhxrjq4hbr h43x rhq43xrjhq43vfq3vrqv3 r43vxfxqv43kxvkq3vkhv4fkqv43hvrkxqhwd8493uidicsurehjrxhhwrxf f  qhfuhfiuqh3ixhiuqhdihqhdiqhz2i3rhi3qx4hrxihioh3rxsidixma4wutc984yu98tu9483c984798xjfojdsfjoiasdfoa", 
                                  dateOfBirth = DateTime.Now.AddDays(-800), 
                                  d = data, 
                                  IsAlive = true
                              };

                var kids = f.children.ToList();
                kids.Add(kid);
                f.children = kids.ToArray();

                collection.FindOneAndReplace(family => family.Id == f.Id, f);

                using (Stream s = new MemoryStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(s, f);
                    size = s.Length;
                    Console.WriteLine($"size of object is: {size / 1024} kbytes");
                }
            }
        }

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
