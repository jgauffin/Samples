using System;
using Griffin.Decoupled.Commands;

namespace WinFormsSample.Decoupled.Commands
{
    /// <summary>
    /// Update a previously created note
    /// </summary>
    public class UpdateNote : CommandBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateNote" /> class.
        /// </summary>
        /// <param name="id">The note id.</param>
        /// <param name="newBody">The new body.</param>
        public UpdateNote(string id, string newBody)
        {
            if (id == null) throw new ArgumentNullException("id");
            if (newBody == null) throw new ArgumentNullException("newBody");
            Id = id;
            NewBody = newBody;
        }

        /// <summary>
        /// Gets the note id
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets new contents
        /// </summary>
        public string NewBody { get; private set; }
    }
}