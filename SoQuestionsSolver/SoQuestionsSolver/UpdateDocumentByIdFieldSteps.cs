// --------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateDocumentByIdFieldSteps.cs" company="">
//   
// </copyright>
// <summary>
//   TODO The update document by id field steps.
// </summary>
// --------------------------------------------------------------------------------------------------------------------------------------------
namespace SoQuestionsSolver
{
    using System;

    using FluentAssertions;

    using MongoDB.Driver;

    using TechTalk.SpecFlow;

    /// <summary>TODO The update document by id field steps.</summary>
    [Binding]
    public class UpdateDocumentByIdFieldSteps
    {
        /// <summary>TODO The _collection.</summary>
        private IMongoCollection<ThingsModel> _collection;

        /// <summary>TODO The _id.</summary>
        private Guid _id;

        /// <summary>TODO The given colloction with sample document.</summary>
        [Given(@"colloction with sample document")]
        public void GivenColloctionWithSampleDocument()
        {
            _collection = ConnectionHelper.Connection<ThingsModel>("ThingsModel");
            _id = Guid.NewGuid();
            var t = new ThingsModel { _id = _id, IsActive = true };
            _collection.InsertOne(t);
        }

        /// <summary>TODO The then status flag need to be false.</summary>
        [Then(@"status  flag need to be false")]
        public void ThenStatusFlagNeedToBeFalse()
        {
            var document = _collection.Find(u => u._id == _id).First();
            document.IsActive.Should().BeFalse();
        }

        /// <summary>TODO The when call dellete method.</summary>
        [When(@"call dellete method")]
        public void WhenCallDelleteMethod()
        {
            var update = Builders<ThingsModel>.Update.Set(a => a.IsActive, false);

            var result = _collection.UpdateOne(model => model._id == _id, update);
        }

        /// <summary>TODO The things model.</summary>
        public class ThingsModel
        {
            /// <summary>Gets or sets the _id.</summary>
            public Guid _id { get; set; }

            /// <summary>Gets or sets the guid.</summary>
            public Guid Guid { get; set; }

            /// <summary>Gets or sets a value indicating whether is active.</summary>
            public bool IsActive { get; set; }
        }
    }
}
