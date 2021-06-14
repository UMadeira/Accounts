using System.Windows.Forms;

namespace Accounts
{
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
            ActiveControl = usernameTextBox;
        }

        public string Username
        {
            get => usernameTextBox.Text;
            set => usernameTextBox.Text = value;
        }

        public string Password
        {
            get => passwordTextBox.Text;
            set => passwordTextBox.Text = value;
        }
    }
}
