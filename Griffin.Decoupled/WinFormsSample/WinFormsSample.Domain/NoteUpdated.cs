using System;
using Griffin.Decoupled.DomainEvents;

namespace WinFormsSample.Domain
{
    /// <summary>
    /// A note has been updated.
    /// </summary>
    public class NoteUpdated : DomainEventBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteUpdated" /> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="oldText">Text before change</param>
        /// <param name="newText">The new text.</param>
        public NoteUpdated(string id, string oldText, string newText)
        {
            if (id == null) throw new ArgumentNullException("id");
            if (oldText == null) throw new ArgumentNullException("oldText");
            if (newText == null) throw new ArgumentNullException("newText");

            Id = id;
            OldText = oldText;
            NewText = newText;
        }

        /// <summary>
        /// Gets id of the entity
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets text before change
        /// </summary>
        public string OldText { get; set; }

        /// <summary>
        /// Gets text after change
        /// </summary>
        public string NewText { get; private set; }
    }
}