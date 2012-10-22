using Griffin.Container;
using Griffin.Decoupled.Commands;
using WinFormsSample.Decoupled.Commands;

namespace WinFormsSample.Decoupled.Implementation.Commands
{
    [Component]
    internal class CompleteHandler : IHandleCommand<CompleteNote>
    {
        private readonly INoteStorage _storage;

        public CompleteHandler(INoteStorage storage)
        {
            _storage = storage;
        }

        /// <summary>
        /// Invoke the command
        /// </summary>
        /// <param name="command">Command to run</param>
        public void Invoke(CompleteNote command)
        {
            var note = _storage.Load(command.Id);
            note.Complete();
            _storage.Save(note);
        }
    }
}