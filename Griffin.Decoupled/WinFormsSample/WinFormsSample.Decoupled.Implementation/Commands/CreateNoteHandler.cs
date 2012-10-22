using Griffin.Container;
using Griffin.Decoupled.Commands;
using Griffin.Decoupled.DomainEvents;
using WinFormsSample.Decoupled.Commands;
using WinFormsSample.Domain;

namespace WinFormsSample.Decoupled.Implementation.Commands
{
    [Component]
    public class CreateNoteHandler : IHandleCommand<CreateNote>
    {
        private readonly INoteStorage _storage;

        public CreateNoteHandler(INoteStorage storage)
        {
            _storage = storage;
        }

        /// <summary>
        /// Invoke the command
        /// </summary>
        /// <param name="command">Command to run</param>
        public void Invoke(CreateNote command)
        {
            var note = new Note(command.Title, command.Body);
            _storage.Save(note);
            DomainEvent.Publish(new NoteCreated(note.Id));
        }
    }
}