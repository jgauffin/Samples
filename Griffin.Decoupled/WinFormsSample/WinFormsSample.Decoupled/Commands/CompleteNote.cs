using System;
using Griffin.Decoupled.Commands;

namespace WinFormsSample.Decoupled.Commands
{
    /// <summary>
    /// Mark a note as completed
    /// </summary>
    public class CompleteNote : CommandBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompleteNote" /> class.
        /// </summary>
        /// <param name="id">note id.</param>
        public CompleteNote(string id)
        {
            if (id == null) throw new ArgumentNullException("id");
            Id = id;
        }

        /// <summary>
        /// Gets note id
        /// </summary>
        public string Id { get; private set; }
    }
}