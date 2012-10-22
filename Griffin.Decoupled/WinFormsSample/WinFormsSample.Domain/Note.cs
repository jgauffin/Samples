using System;
using Griffin.Decoupled.DomainEvents;

namespace WinFormsSample.Domain
{
    /// <summary>
    /// A note in the system
    /// </summary>
    public class Note
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Note" /> class.
        /// </summary>
        /// <param name="title">note title</param>
        /// <param name="body">The body.</param>
        /// <exception cref="System.ArgumentNullException">body</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">body;Body may max me 500000 bytes.</exception>
        public Note(string title, string body)
        {
            if (title == null) throw new ArgumentNullException("title");
            if (body == null) throw new ArgumentNullException("body");
            if (body.Length > 500000)
                throw new ArgumentOutOfRangeException("body", body, "Body may max me 500000 bytes.");

            Title = title;
            Body = body;
            CreatedAt = DateTime.Now;
        }

        /// <summary>
        /// For the persistance layer.
        /// </summary>
        protected Note(string title)
        {
            Title = title;
        }

        /// <summary>
        /// Gets db id
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets when the item was created.
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Gets current body
        /// </summary>
        public string Body { get; private set; }

        /// <summary>
        /// Gets when the item was upated
        /// </summary>
        public DateTime UpdatedAt { get; private set; }

        /// <summary>
        /// Gets if the item has been completed
        /// </summary>
        public bool IsCompleted { get; private set; }

        /// <summary>
        /// Gets when the item was completed.
        /// </summary>
        public DateTime CompletedAt { get; set; }

        /// <summary>
        /// Gets title
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Updates the text.
        /// </summary>
        /// <param name="newText">The new text.</param>
        /// <exception cref="System.ArgumentNullException">newText</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">newText;Body may max be 500000 bytes.</exception>
        public void Update(string newText)
        {
            if (newText == null) throw new ArgumentNullException("newText");
            if (newText.Length > 500000)
                throw new ArgumentOutOfRangeException("newText", newText, "Body may max be 500000 bytes.");

            if (IsCompleted)
                throw new InvalidOperationException("Items may not be changed once completed.");

            var oldText = Body;
            Body = newText;
            UpdatedAt = DateTime.Now;

            DomainEvent.Publish(new NoteUpdated(Id, oldText, newText));
        }

        /// <summary>
        /// Mark item as completed
        /// </summary>
        public void Complete()
        {
            // Might look trivial, but it's in fact invalid logic which may
            // produce other side effects. So throw that exception...
            if (IsCompleted)
                throw new InvalidOperationException("Item has already been marked as completed.");

            IsCompleted = true;
            CompletedAt = DateTime.Now;

            DomainEvent.Publish(new NoteCompleted(Id));
        }
    }
}