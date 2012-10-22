using System;
using Griffin.Decoupled.Commands;

namespace WinFormsSample.Decoupled.Commands
{
    /// <summary>
    /// Create a new note
    /// </summary>
    public class CreateNote : CommandBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNote" /> class.
        /// </summary>
        /// <param name="title">Note title </param>
        /// <param name="body">The body.</param>
        /// <exception cref="System.ArgumentNullException">body</exception>
        public CreateNote(string title, string body)
        {
            if (title == null) throw new ArgumentNullException("title");
            if (body == null) throw new ArgumentNullException("body");
            Title = title;
            Body = body;
        }

        /// <summary>
        /// Gets note title.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets body of the note
        /// </summary>
        public string Body { get; private set; }
    }
}