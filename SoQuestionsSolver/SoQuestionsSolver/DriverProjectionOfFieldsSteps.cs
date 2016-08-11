// --------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="DriverProjectionOfFieldsSteps.cs" company="">
//   
// </copyright>
// <summary>
//   TODO The driver projection of fields steps.
// </summary>
// --------------------------------------------------------------------------------------------------------------------------------------------
namespace SoQuestionsSolver
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using MongoDB.Bson;
    using MongoDB.Driver;

    using TechTalk.SpecFlow;

    /// <summary>TODO The driver projection of fields steps.</summary>
    [Binding]
    public class DriverProjectionOfFieldsSteps
    {
        /// <summary>TODO The _collection.</summary>
        private IMongoCollection<FrameDocument> _collection;

        /// <summary>TODO The _id.</summary>
        private ObjectId _id;

        /// <summary>TODO The _result.</summary>
        private List<FrameDocumentNoFrameField> _result;

        /// <summary>TODO The given frame document collection.</summary>
        [Given(@"FrameDocument collection")]
        public void GivenFrameDocumentCollection()
        {
            _collection = ConnectionHelper.Connection<FrameDocument>("FrameDocument");
        }

        /// <summary>TODO The given one document inserted.</summary>
        [Given(@"one document inserted")]
        public void GivenOneDocumentInserted()
        {
            var doc = new FrameDocument { FrameTimeStamp = DateTime.Now, Frame = new byte[] { 0x2, 0x6 } };
            _collection.InsertOne(doc);
            _id = doc._id;
        }

        /// <summary>TODO The then result has to be without frame filed.</summary>
        [Then(@"result has to be without Frame filed")]
        public void ThenResultHasToBeWithoutFrameFiled()
        {
            (_result[0].GetType() == typeof(FrameDocumentNoFrameField)).Should().BeTrue();
        }

        /// <summary>TODO The when run projection.</summary>
        [When(@"run projection")]
        public void WhenRunProjection()
        {
            _result = _collection.Find(o => o._id == _id).Project<FrameDocumentNoFrameField>(Builders<FrameDocument>.Projection.Exclude(f => f.Frame)).ToList();
        }

        /// <summary>TODO The frame document.</summary>
        public class FrameDocument
        {
            /// <summary>Gets or sets the _id.</summary>
            public ObjectId _id { get; set; }

            /// <summary>Gets or sets the active pick.</summary>
            public int? ActivePick { get; set; }

            /// <summary>Gets or sets the event code id.</summary>
            public int? EventCodeId { get; set; }

            /// <summary>Gets or sets the frame.</summary>
            public byte[] Frame { get; set; }

            /// <summary>Gets or sets the frame time stamp.</summary>
            public DateTime? FrameTimeStamp { get; set; }

            /// <summary>Gets or sets the server user id.</summary>
            public int ServerUserId { get; set; }

            /// <summary>Gets or sets the server user name.</summary>
            public string ServerUserName { get; set; }

            /// <summary>Gets or sets the sesion id.</summary>
            public int SesionId { get; set; }

            /// <summary>Gets or sets the trader id.</summary>
            public int? TraderId { get; set; }

            /// <summary>Gets or sets the trader name.</summary>
            public string TraderName { get; set; }
        }

        /// <summary>TODO The frame document no frame field.</summary>
        public class FrameDocumentNoFrameField
        {
            /// <summary>Gets or sets the _id.</summary>
            public ObjectId _id { get; set; }

            /// <summary>Gets or sets the active pick.</summary>
            public int? ActivePick { get; set; }

            /// <summary>Gets or sets the event code id.</summary>
            public int? EventCodeId { get; set; }

            /// <summary>Gets or sets the frame time stamp.</summary>
            public DateTime? FrameTimeStamp { get; set; }

            /// <summary>Gets or sets the server user id.</summary>
            public int ServerUserId { get; set; }

            /// <summary>Gets or sets the server user name.</summary>
            public string ServerUserName { get; set; }

            /// <summary>Gets or sets the sesion id.</summary>
            public int SesionId { get; set; }

            /// <summary>Gets or sets the trader id.</summary>
            public int? TraderId { get; set; }

            /// <summary>Gets or sets the trader name.</summary>
            public string TraderName { get; set; }
        }
    }
}
