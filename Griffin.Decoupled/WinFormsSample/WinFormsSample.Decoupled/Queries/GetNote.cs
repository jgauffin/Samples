using System;
using Griffin.Decoupled.Queries;
using WinFormsSample.Domain;

namespace WinFormsSample.Decoupled.Queries
{
    /// <summary>
    /// Gets a specific note (may or may not have been marked as completed)
    /// </summary>
    public class GetNote : IQuery<Note>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetNote" /> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <exception cref="System.ArgumentNullException">id</exception>
        public GetNote(string id)
        {
            if (id == null) throw new ArgumentNullException("id");
            Id = id;
        }

        /// <summary>
        /// Gets note id
        /// </summary>
        public string Id { get; set; }
    }
}