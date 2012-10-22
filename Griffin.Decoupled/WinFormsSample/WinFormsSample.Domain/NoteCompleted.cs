using System;
using Griffin.Decoupled.DomainEvents;

namespace WinFormsSample.Domain
{
    /// <summary>
    /// A note has been successfully completed.
    /// </summary>
    public class NoteCompleted : DomainEventBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteCompleted" /> class.
        /// </summary>
        /// <param name="id">The id.</param>
        public NoteCompleted(string id)
        {
            if (id == null) throw new ArgumentNullException("id");
            Id = id;
        }

        /// <summary>
        /// Gets id of item.
        /// </summary>
        public string Id { get; private set; }
    }
}