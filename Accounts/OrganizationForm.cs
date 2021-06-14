using Accounts.Data;
using Accounts.Patterns.Repository;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace Accounts
{
    internal partial class OrganizationForm : Form
    {
        public OrganizationForm( IRepository<IUser> repository )
        {
            InitializeComponent();
            ActiveControl = nameTextBox;

            existingUsersListView.ItemSelectionChanged += 
                (sender, args) => addUserButton.Enabled = existingUsersListView.SelectedItems.Count > 0;
            selectedUsersListView.ItemSelectionChanged +=
                (sender, args) => removeUserButton.Enabled = selectedUsersListView.SelectedItems.Count > 0;

            addUserButton.Click += (sender, args) => AddUser();
            removeUserButton.Click += (sender, args) => RemoveUser();

            existingUsersListView.DoubleClick += (sender, args) => AddUser();
            selectedUsersListView.DoubleClick += (sender, args) => RemoveUser();

            foreach ( var user in repository.Entities )
            {
                AppendItem( existingUsersListView, user );
            }
        }

        public string OrganizationName
        {
            get => nameTextBox.Text;
            set => nameTextBox.Text = value;
        }

        public IEnumerable<IUser> Users 
        { 
            get
            {
                return selectedUsersListView.Items
                    .Cast<ListViewItem>()
                    .Select( item => item.Tag as IUser );
            }
            set 
            {
                foreach ( var user in value.Where( u => u.Zombie == false) )
                {
                    AppendItem( selectedUsersListView, user );
                    RemoveItem( existingUsersListView, user );
                }
            }
        }

        private void AddUser()
        {
            if (existingUsersListView.SelectedItems.Count == 0) return;
            var selected = existingUsersListView.SelectedItems[0];

            var user = selected.Tag as IUser;
            RemoveItem(existingUsersListView, user);
            AppendItem(selectedUsersListView, user);
        }

        private void RemoveUser()
        {
            if (selectedUsersListView.SelectedItems.Count == 0) return;
            var selected = selectedUsersListView.SelectedItems[0];

            var user = selected.Tag as IUser;
            RemoveItem(selectedUsersListView, user);
            AppendItem(existingUsersListView, user);
        }

        private void AppendItem(ListView view, IUser user)
        {
            var item = view.Items.Add(user.Username);
            item.Tag = user;
            item.ImageIndex = 0;

            view.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void RemoveItem( ListView view, IUser user)
        {
            var item = view.Items.Cast<ListViewItem>().FirstOrDefault( e => e.Tag == user );
            if ( item == null) return;
            view.Items.Remove(item);

            view.AutoResizeColumn( 0, ColumnHeaderAutoResizeStyle.ColumnContent );
        }
    }
}
