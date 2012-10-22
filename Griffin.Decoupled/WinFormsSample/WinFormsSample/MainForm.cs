using System;
using System.Windows.Forms;
using Griffin.Container;
using Griffin.Decoupled.Commands;
using Griffin.Decoupled.DomainEvents;
using Griffin.Decoupled.Queries;
using WinFormsSample.Decoupled.Commands;
using WinFormsSample.Decoupled.Queries;
using WinFormsSample.Domain;

namespace WinFormsSample
{
    // Only one main form.
    [Component(Lifetime = Lifetime.Singleton)]
    public partial class MainForm : Form, ISubscribeOn<NoteCompleted>, ISubscribeOn<NoteCreated>
    {
        public MainForm()
        {
            InitializeComponent();
            Note.Enabled = false;
            btnComplete.Enabled = false;
            btnSave.Enabled = false;
        }

        /// <summary>
        /// Will be invoked when the domain event is triggered.
        /// </summary>
        /// <param name="domainEvent">Domin event to handle</param>
        public void Handle(NoteCompleted domainEvent)
        {
            Invoke(new MethodInvoker(() => RemoveItem(domainEvent.Id)));
        }

        /// <summary>
        /// Will be invoked when the domain event is triggered.
        /// </summary>
        /// <param name="domainEvent">Domin event to handle</param>
        public void Handle(NoteCreated domainEvent)
        {
            Invoke(new MethodInvoker(() => AddItem(domainEvent.Id)));
        }

        private void Save_Click(object sender, EventArgs e)
        {
            var cmd = new UpdateNote((string) Note.Tag, Note.Text);
            CommandDispatcher.Dispatch(cmd);
        }

        private void CboNote_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CboNote.SelectedIndex == -1)
                return;

            var item = CboNote.Items[CboNote.SelectedIndex] as IdTitle;
            btnComplete.Enabled = false;
            btnSave.Enabled = false;
            Note.Enabled = false;
            Note.Text = "";

            if (item == null)
                return;

            if (item.Id == "-1")
            {
                Program.Build<CreateNoteForm>().Show();
                CboNote.SelectedIndex = -1;
                return;
            }

            LoadItem(item.Id);
        }

        private void LoadItem(string noteId)
        {
            btnComplete.Enabled = btnSave.Enabled = true;
            Note.Enabled = true;

            using (var scope = Program.CreateScope())
            {
                var query = new GetNote(noteId);
                var result = scope.Resolve<IQueryDispatcher>().Execute(query);
                Note.Text = result.Body;
                Note.Tag = result.Id;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            PopulateNotes();
        }

        private void PopulateNotes()
        {
            CboNote.Items.Add("");
            CboNote.Items.Add(new IdTitle {Id = "-1", Title = "(Create new note)"});

            var query = new GetMyActiveNotes();
            using (var scope = Program.CreateScope())
            {
                var result = scope.Resolve<IQueryDispatcher>().Execute(query);
                foreach (var note in result)
                {
                    CboNote.Items.Add(note);
                }
            }
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            var cmd = new CompleteNote((string) Note.Tag);
            CommandDispatcher.Dispatch(cmd);
        }

        private void RemoveItem(string id)
        {
            foreach (var item in CboNote.Items)
            {
                var obj = item as IdTitle;
                if (obj != null && obj.Id == id)
                {
                    if (item == CboNote.SelectedItem)
                        CboNote.SelectedIndex = 0;
                    CboNote.Items.Remove(item);
                    return;
                }
            }
        }

        private void AddItem(string id)
        {
            var query = new GetNote(id);
            Note note;
            using (var scope = Program.CreateScope())
            {
                note = scope.Resolve<IQueryDispatcher>().Execute(query);
            }

            CboNote.Items.Add(new IdTitle
                {
                    Id = note.Id,
                    Title = note.Title
                });
        }
    }
}