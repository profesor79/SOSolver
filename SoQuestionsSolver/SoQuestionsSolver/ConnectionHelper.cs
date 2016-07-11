// --------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="ConnectionHelper.cs" company="">
//   
// </copyright>
// <summary>
//   TODO The connection helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------------------------------
namespace SoQuestionsSolver
{
    using MongoDB.Driver;

    /// <summary>TODO The connection helper.</summary>
    public static class ConnectionHelper
    {
        /// <summary>TODO The connection.</summary>
        /// <param name="collectionNAme">TODO The collection n ame.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>The <see cref="IMongoCollection"/>.</returns>
        public static IMongoCollection<T> Connection<T>(string collectionNAme)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("test");
            return database.GetCollection<T>(collectionNAme);
        }
    }
}
