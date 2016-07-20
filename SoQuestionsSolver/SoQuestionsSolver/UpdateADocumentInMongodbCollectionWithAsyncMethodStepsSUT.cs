// --------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateADocumentInMongodbCollectionWithAsyncMethodStepsSUT.cs" company="">
//   
// </copyright>
// <summary>
//   TODO The update sut.
// </summary>
// --------------------------------------------------------------------------------------------------------------------------------------------
namespace SoQuestionsSolver
{
    using System.Threading.Tasks;

    using MongoDB.Driver;

    /// <summary>TODO The update sut.</summary>
    public static class AsyncSave
    {
        /// <summary>TODO The save async.</summary>
        /// <param name="collection">TODO The collection.</param>
        /// <param name="entity">TODO The entity.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>The <see cref="Task"/>.</returns>
        public static async Task<ReplaceOneResult> SaveAsync1<T>(this IMongoCollection<T> collection, T entity) where T : StronglyTypedFilterUsageSteps.Widget
        {
            return await collection.ReplaceOneAsync(i => i.X == entity.X, entity, new UpdateOptions { IsUpsert = false });
        }
    }
}
