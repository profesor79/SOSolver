// --------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="CompareOnSameDocumentFieldsSteps.cs" company="">
//   
// </copyright>
// <summary>
//   TODO The compare on same document fields steps.
// </summary>
// --------------------------------------------------------------------------------------------------------------------------------------------
namespace SoQuestionsSolver
{
    using System.Diagnostics;

    using FluentAssertions;

    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Driver;
    using MongoDB.Driver.Linq;

    using TechTalk.SpecFlow;

    /// <summary>TODO The compare on same document fields steps.</summary>
    [Binding]
    public class CompareOnSameDocumentFieldsSteps
    {
        /// <summary>TODO The _collection.</summary>
        private IMongoCollection<Datapull> _collection;

        /// <summary>TODO The given comparision sample data.</summary>
        [Given(@"ComparisionSampleData")]
        public void GivenComparisionSampleData()
        {
            _collection = ConnectionHelper.Connection<Datapull>("datapull");

            var x = new Datapull { Company = "msft", Sedol = "111", FSTicker = "111" };

            var y = new Datapull { Company = "msft2", Sedol = "1112", FSTicker = "111" };

            // _collection.InsertOne(x);
            // _collection.InsertOne(y);
        }

        /// <summary>TODO The then try to use where clause.</summary>
        [Then(@"Try to use where clause")]
        public void ThenTryToUseWhereClause()
        {
            // var result = _collection.AsQueryable().Where(x => x.FSTicker == x.Sedol).ToList();
            var result = _collection.AsQueryable().Where(c => c.FSTicker.Equals(c.Sedol)).ToList();
            foreach (var r in result)
            {
                Debug.Write($"{r.FSTicker}{r.Sedol}");
                r.FSTicker.Should().Equals(r.Sedol);
            }
        }

        /// <summary>TODO The then will get only result with same fields value.</summary>
        [Then(@"Will get only result with same fields value")]
        public void ThenWillGetOnlyResultWithSameFieldsValue()
        {
            var data = _collection.Aggregate();
            var a1 =
                data.Project(
                    x =>
                    new { x.FSTicker, x.Sedol, x.Company, x.Exchange, x.LocalTicker, IsTrue = x.Sedol == x.FSTicker });
            var a2 = a1.Match(x => x.IsTrue);

            var result = a2.ToList();

            foreach (var r in result)
            {
                Debug.Write($"{r.FSTicker}{r.Sedol}");
                r.FSTicker.Should().Equals(r.Sedol);
            }
        }

        /// <summary>TODO The datapull.</summary>
        [BsonIgnoreExtraElements]
        public class Datapull
        {
            /// <summary>Gets or sets the company.</summary>
            [BsonElement("compname")]
            public string Company { get; set; }

            /// <summary>Gets or sets the exchange.</summary>
            [BsonElement("exchange")]
            public string Exchange { get; set; }

            /// <summary>Gets or sets the fs ticker.</summary>
            [BsonElement("fstick")]
            public string FSTicker { get; set; }

            /// <summary>Gets or sets a value indicating whether is true.</summary>
            [BsonIgnore]
            public bool IsTrue { get; set; }

            /// <summary>Gets or sets the local ticker.</summary>
            [BsonElement("localtick")]
            public string LocalTicker { get; set; }

            /// <summary>Gets or sets the sedol.</summary>
            [BsonElement("sedol")]
            public string Sedol { get; set; }
        }
    }
}
