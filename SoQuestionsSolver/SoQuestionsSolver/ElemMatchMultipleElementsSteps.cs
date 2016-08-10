// --------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="ElemMatchMultipleElementsSteps.cs" company="">
//   
// </copyright>
// <summary>
//   TODO The elem match multiple elements steps.
// </summary>
// --------------------------------------------------------------------------------------------------------------------------------------------
namespace SoQuestionsSolver
{
    using MongoDB.Bson;
    using MongoDB.Driver;

    using TechTalk.SpecFlow;

    /// <summary>TODO The elem match multiple elements steps.</summary>
    [Binding]
    public class ElemMatchMultipleElementsSteps
    {
        /// <summary>TODO The _collection.</summary>
        private IMongoCollection<BsonDocument> _collection;

        /// <summary>TODO The query.</summary>
        private BsonDocument query;

        /// <summary>TODO The given collection.</summary>
        /// <param name="p0">TODO The p 0.</param>
        [Given(@"collection ""(.*)""")]
        public void GivenCollection(string p0)
        {
            _collection = ConnectionHelper.Connection<BsonDocument>(p0);
        }

        /// <summary>TODO The then resut need be not null.</summary>
        [Then(@"resut need be not null")]
        public void ThenResutNeedBeNotNull()
        {
            var result = _collection.FindSync(query).ToList();
        }

        /// <summary>TODO The when try to parse query.</summary>
        [When(@"try to parse query")]
        public void WhenTryToParseQuery()
        {
            query =
                BsonDocument.Parse(
                    "{$and: [{WordsData:{$elemMatch:{UserId: ObjectId('57a87f5cc48933119cb96f93'),UserId: ObjectId('57a87f5cc48933119cb96f94')}}}, {WordsData:{$not:{$elemMatch:{MatchType: 2}}}}]}");
        }
    }
}
