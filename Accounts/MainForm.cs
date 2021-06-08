using Accounts.Commands;
using Accounts.Data;
using Accounts.Data.Commands;
using Accounts.Patterns.Factory;
using Accounts.UI.Commands;
using Accounts.Data.Observables;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accounts.Patterns.Observer;

namespace Accounts
{
    public partial class MainForm : Form
    {
        private IFactory Factory { get; } = new ObservableFactory();

        public MainForm()
        {
            InitializeComponent();

            undoToolStripButton.Click += OnUndo;
            undoToolStripMenuItem.Click += OnUndo;

            redoToolStripButton.Click += OnRedo;
            redoToolStripMenuItem.Click += OnRedo;

            CommandManager.Instance.Notify += ( s, a ) => undoToolStripButton.Enabled = CommandManager.Instance.HasUndo();
            CommandManager.Instance.Notify += ( s, a ) => undoToolStripMenuItem.Enabled = CommandManager.Instance.HasUndo();
            CommandManager.Instance.Notify += ( s, a ) => redoToolStripButton.Enabled = CommandManager.Instance.HasRedo();
            CommandManager.Instance.Notify += ( s, a ) => redoToolStripMenuItem.Enabled = CommandManager.Instance.HasRedo();

            userToolStripButton.Click += OnCreateUser;
            organizationToolStripButton.Click += OnCreateOrganization;
        }

 
        private void OnUndo(object sender, EventArgs e)
        {
            if (!CommandManager.Instance.HasUndo()) return;
            CommandManager.Instance.Undo();
        }

        private void OnRedo(object sender, EventArgs e)
        {
            if ( ! CommandManager.Instance.HasRedo() ) return;
            CommandManager.Instance.Redo();
        }

        private void OnCreateUser(object sender, EventArgs e)
        {
            var dialog = new UserForm();
            dialog.Username = "New User";
            if ( dialog.ShowDialog() == DialogResult.OK )
            {
                var macro = new MacroCommand();

                var user = Factory.Create<IUser>();
                user.Username = dialog.Username;
                user.Password = dialog.Password;

                macro.Add( new CreateUser( user ) );

                var node = new TreeNode( user.Username );
                node.ImageIndex = node.SelectedImageIndex = 0;
                node.Tag = user;
                macro.Add( new AddNode( mainTreeView.Nodes, node ) );

                CommandManager.Instance.Execute(macro);

                if ( user is IObservable )
                {
                    var observable = user as IObservable;
                    observable.Notify += (sender, args) => node.Text = user.Username;
                }
            }
        }
        private void OnCreateOrganization(object sender, EventArgs e)
        {
            var dialog = new OrganizationForm();
            dialog.Name = "New Organization";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var macro = new MacroCommand();

                var organization = Factory.Create<IOrganization>();
                organization.Name = dialog.OrganizationName;

                macro.Add(new CreateOrganization(organization));

                var node = new TreeNode(organization.Name);
                node.ImageIndex = node.SelectedImageIndex = 1;
                node.Tag = organization;
                macro.Add(new AddNode(mainTreeView.Nodes, node));

                CommandManager.Instance.Execute(macro);

                if (organization is IObservable)
                {
                    var observable = organization as IObservable;
                    observable.Notify += (sender, args) => node.Text = organization.Name;
                }
            }
        }

        private void OnDoubleClick(object sender, EventArgs e)
        {
            var item = mainTreeView.SelectedNode?.Tag as IItem;
            if (item == null) return;

            if (item is IUser) 
            { 
                Edit( item as IUser ); 
            }
            else if ( item is IOrganization )
            {
                Edit( item as IOrganization );
            }
        }

        private void Edit( IUser user )
        {
            var dialog = new UserForm();
            dialog.Username = user.Username;
            dialog.Password = user.Password;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var macro = new MacroCommand();
                if (user.Username != dialog.Username)
                {
                    macro.Add( new ChangeUsername( user, dialog.Username ) );
                }
                if (user.Password != dialog.Password)
                {
                    macro.Add(new ChangePassword(user, dialog.Username));
                }

                if (macro.IsEmpty) return;
                CommandManager.Instance.Execute(macro);
            }
        }

        private void Edit( IOrganization organization )
        {
            var dialog = new OrganizationForm();
            dialog.OrganizationName = organization.Name;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (organization.Name != dialog.OrganizationName)
                {
                    CommandManager.Instance.Execute( new ChangeName( organization, dialog.OrganizationName) );
                }
            }
        }
    }
}
