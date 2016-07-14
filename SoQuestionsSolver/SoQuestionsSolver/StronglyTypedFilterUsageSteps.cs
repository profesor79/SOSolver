// --------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="StronglyTypedFilterUsageSteps.cs" company="">
//   
// </copyright>
// <summary>
//   TODO The strongly typed filter usage steps.
// </summary>
// --------------------------------------------------------------------------------------------------------------------------------------------
namespace SoQuestionsSolver
{
    using System.Linq;

    using FluentAssertions;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Driver;

    using TechTalk.SpecFlow;

    /// <summary>TODO The strongly typed filter usage steps.</summary>
    [Binding]
    public class StronglyTypedFilterUsageSteps
    {
        /// <summary>TODO The _collection.</summary>
        private IMongoCollection<Widget> _collection;

        /// <summary>TODO The _result.</summary>
        private Widget _result;

        /// <summary>TODO The given sample data from lapsus.</summary>
        [Given(@"Sample data from lapsus")]
        public void GivenSampleDataFromLapsus()
        {
            _collection = ConnectionHelper.Connection<Widget>("lapsus");

            var x = new Widget { X = 10, Y = 20 };
            var y = new Widget { X = 10, Y = 30 };
            var z = new Widget { X = 10, Y = 15 };

            _collection.InsertOne(x);
            _collection.InsertOne(y);
            _collection.InsertOne(z);
        }

        /// <summary>TODO The then result should be xy.</summary>
        /// <param name="x">TODO The x.</param>
        /// <param name="y">TODO The y.</param>
        [Then(@"result should be x= (.*), y  = (.*)")]
        public void ThenResultShouldBeXY(int x, int y)
        {
            _result.X.Should().Be(x);
            _result.Y.Should().Be(y);
        }

        /// <summary>TODO The when asking as queryable for x and y.</summary>
        /// <param name="x">TODO The x.</param>
        /// <param name="y">TODO The y.</param>
        [When(@"Asking as queryable for x = (.*) and y < (.*)")]
        public void WhenAskingAsQueryableForXAndY(int x, int y)
        {
            _result = _collection.AsQueryable().First(w => w.X == x && w.Y < y);
        }

        /// <summary>TODO The when asking for x and y.</summary>
        /// <param name="x">TODO The x.</param>
        /// <param name="y">TODO The y.</param>
        [When(@"Asking for x = (.*) and y < (.*)")]
        public void WhenAskingForXAndY(int x, int y)
        {
            _result =
                _collection.Find(new ExpressionFilterDefinition<Widget>(widget => widget.X == 10 && widget.Y < y))
                    .First();
        }

        /// <summary>TODO The widget.</summary>
        public class Widget
        {
            /// <summary>Gets or sets the _id.</summary>
            public ObjectId _id { get; set; }

            /// <summary>Gets or sets the x.</summary>
            [BsonElement("X")]
            public int X { get; set; }

            /// <summary>Gets or sets the y.</summary>
            [BsonElement("Y")]
            public int Y { get; set; }
        }
    }
}
