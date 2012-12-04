using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chat.Api;

namespace ChatClient
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void CommandBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        public void AddChatMessage(ChatMessage message)
        {
            ChatWindow.Text += string.Format("[{0}] {1} {2}\r\n", message.CreatedAt.ToShortTimeString(),
                                             message.UserName, message.Message);
        }

        private void CommandBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                Program.SendChatMessage(new ChatMessage
                    {
                        CreatedAt = DateTime.Now,
                        Message = CommandBox.Text,
                        UserName = Environment.UserName
                    });

                CommandBox.Text = "";
            }


        }
    }
}
