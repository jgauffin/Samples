using System.Windows.Forms;

namespace ChatClient
{
    public static class InvokeRequiredExtension
    {
        public static void InvokeIfRequired(this Control control, MethodInvoker action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }
        
    }
}