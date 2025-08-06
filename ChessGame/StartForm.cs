using System;
using System.Windows.Forms;

namespace ChessGame
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string serverIp = txtIp.Text;
            if (serverIp.Length > 0 )
            {
                var chessForm = new ChessForm(false, serverIp);
                chessForm.Show();
                this.Hide();
            }
        }

        private void btnHost_Click(object sender, EventArgs e)
        {
            var chessForm = new ChessForm(true, null);
            chessForm.Show();
            this.Hide();
        }
    }
}
