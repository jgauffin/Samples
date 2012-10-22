using System;
using Griffin.Decoupled.DomainEvents;

namespace WinFormsSample.Domain
{
    /// <summary>
    /// A new note has been created
    /// </summary>
    public class NoteCreated : DomainEventBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteCreated" /> class.
        /// </summary>
        /// <param name="id">Note id.</param>
        public NoteCreated(string id)
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