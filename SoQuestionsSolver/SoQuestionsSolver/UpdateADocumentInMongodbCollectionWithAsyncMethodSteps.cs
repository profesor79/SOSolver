// --------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateADocumentInMongodbCollectionWithAsyncMethodSteps.cs" company="">
//   
// </copyright>
// <summary>
//   TODO The update a document in mongodb collection with async method steps.
// </summary>
// --------------------------------------------------------------------------------------------------------------------------------------------
namespace SoQuestionsSolver
{
    using MongoDB.Driver;

    using TechTalk.SpecFlow;

    /// <summary>TODO The update a document in mongodb collection with async method steps.</summary>
    [Binding]
    public class UpdateADocumentInMongodbCollectionWithAsyncMethodSteps
    {
        /// <summary>TODO The _collection.</summary>
        private IMongoCollection<StronglyTypedFilterUsageSteps.Widget> _collection;

        /// <summary>TODO The when first document is updated.</summary>
        [When(@"first document is updated")]
        public async void WhenFirstDocumentIsUpdated()
        {
            _collection = ConnectionHelper.Connection<StronglyTypedFilterUsageSteps.Widget>("lapsus");

            var document = _collection.Find(x => x.X < 100).First();
            document.X = 101;
            await _collection.SaveAsync1(document);
        }
    }
}
