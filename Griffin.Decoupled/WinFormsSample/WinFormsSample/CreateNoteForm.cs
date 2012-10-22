using System;
using System.Windows.Forms;
using Griffin.Container;
using Griffin.Decoupled.Commands;
using WinFormsSample.Decoupled.Commands;

namespace WinFormsSample
{
    [Component]
    public partial class CreateNoteForm : Form
    {
        public CreateNoteForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // you should validate here too.
            var command = new CreateNote(Title.Text, Body.Text);
            CommandDispatcher.Dispatch(command);
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}