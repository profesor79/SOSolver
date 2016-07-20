// --------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Flo.cs" company="">
//   
// </copyright>
// <summary>
//   TODO The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------------------------------

using MongoDB.Bson.Serialization.Attributes;

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
    public static class Inquisitive_one
    {
        

        /// <summary>TODO The main.</summary>
        public static void Main()
        {
        
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("test");

            var collection = database.GetCollection<Datapull>("Inquisitive_one");
           // InsertData(collection);

            // nice problem exploring mongo query weakness

            var projectionDef = new ProjectionDefinitionBuilder<Datapull>();
            var p = projectionDef.Include(x => x.Sedol)
                .Include(x => x.FSTicker)
                .Include(x => x.Company);
                
                

            var data = collection.Aggregate();
            var a1 =
                data.Project(
                    x =>
                        new 
                        {
                            FSTicker = x.FSTicker,
                            Sedol = x.Sedol,
                            Company = x.Company,
                            Exchange = x.Exchange,
                            LocalTicker = x.LocalTicker,
                            IsTrue = (x.Sedol == x.FSTicker)
                        });
            var a2 = a1.Match(x => x.IsTrue);

            var result = a2.ToList();


            //var data = collection.AsQueryable().Where(x => x.Sedol == x.FSTicker).ToList();
            
            // PushDocumentsToArray(size, f, collection);
            Console.WriteLine("ready");
            Console.ReadLine();
        }

        private static void InsertData(IMongoCollection<Datapull> collection)
        {
            var x = new Datapull
            {
                Company = "Microsoft",
                FSTicker="25881xx",
                Sedol = "25881xx",

            };

            var z = new Datapull
            {
                Company = "BlackBerry",
                FSTicker = "1",
                Sedol = "sd"
            };


            collection.InsertOne(x);
            collection.InsertOne(z);
        }


        [BsonIgnoreExtraElements]
        public class Datapull
        {
            [BsonElement("fstick")]
            public string FSTicker { get; set; }
            [BsonElement("sedol")]
            public string Sedol { get; set; }
            [BsonElement("exchange")]
            public string Exchange { get; set; }
            [BsonElement("localtick")]
            public string LocalTicker { get; set; }
            [BsonElement("compname")]
            public string Company { get; set; }

            public bool IsTrue { get; set; }
        }
    }
}

//http://stackoverflow.com/questions/38053557/mongo-find-method-does-not-work-for-datetime-minvalue
