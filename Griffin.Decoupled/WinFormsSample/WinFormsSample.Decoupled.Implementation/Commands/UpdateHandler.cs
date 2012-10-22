using Griffin.Container;
using Griffin.Decoupled.Commands;
using WinFormsSample.Decoupled.Commands;

namespace WinFormsSample.Decoupled.Implementation.Commands
{
    [Component]
    internal class UpdateHandler : IHandleCommand<UpdateNote>
    {
        private readonly INoteStorage _storage;

        public UpdateHandler(INoteStorage storage)
        {
            _storage = storage;
        }

        /// <summary>
        /// Invoke the command
        /// </summary>
        /// <param name="command">Command to run</param>
        public void Invoke(UpdateNote command)
        {
            var note = _storage.Load(command.Id);
            note.Update(command.NewBody);
            _storage.Save(note);
        }
    }
}