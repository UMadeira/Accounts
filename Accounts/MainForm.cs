using Accounts.Data;
using Accounts.Data.Commands;
using Accounts.Data.Observables;
using Accounts.Patterns.Commands;
using Accounts.Patterns.Factory;
using Accounts.Patterns.Observer;
using Accounts.Patterns.Repository;
using Accounts.UI.Commands;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Accounts.UI.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Accounts
{
    public partial class MainForm : Form
    {
        private IFactory ObservableFactory { get; } = new ObservableFactory();
        private IUnitOfWork UnitOfWork { get; }

        public MainForm( IUnitOfWork unitOfWork )
        {
            UnitOfWork = unitOfWork;

            InitializeComponent();

            exitToolStripMenuItem.Click += ( sender, args ) => Application.Exit();

            editToolStripButton.Click += OnEdit;
            mainTreeView.AfterSelect +=
                ( sender, args ) => editToolStripButton.Enabled = mainTreeView.SelectedNode != null;

            deleteToolStripButton.Click += OnDelete;
            mainTreeView.AfterSelect +=
                ( sender, args ) => deleteToolStripButton.Enabled = mainTreeView.SelectedNode != null;

            undoToolStripButton.Click += OnUndo;
            undoToolStripMenuItem.Click += OnUndo;

            redoToolStripButton.Click += OnRedo;
            redoToolStripMenuItem.Click += OnRedo;

            userToolStripButton.Click += OnCreateUser;
            organizationToolStripButton.Click += OnCreateOrganization;

            CommandManager.Instance.Notify += ( s, a ) => undoToolStripButton.Enabled = CommandManager.Instance.HasUndo();
            CommandManager.Instance.Notify += ( s, a ) => redoToolStripButton.Enabled = CommandManager.Instance.HasRedo();

            CommandManager.Instance.Notify += ( s, a ) => undoToolStripMenuItem.Enabled = CommandManager.Instance.HasUndo();
            CommandManager.Instance.Notify += ( s, a ) => redoToolStripMenuItem.Enabled = CommandManager.Instance.HasRedo();

            LoadOrganizations( UnitOfWork.GetRepository<IOrganization>().Entities.Include( o => o.Users) );
            LoadUsers( UnitOfWork.GetRepository<IUser>().Entities );
        }

        private void LoadUsers( IQueryable<IUser> users )
        {
            foreach ( var user in users )
            {
                if ( user.Zombie ) continue;
                var node = CreateNode( user );
                mainTreeView.Nodes.Add( node );
            }
        }

        private void LoadOrganizations( IQueryable<IOrganization> organizations )
        {
            foreach ( var organization in organizations )
            {
                if ( organization.Zombie ) continue;

                var node = CreateNode( organization );
                mainTreeView.Nodes.Add( node );

                foreach ( var user in organization.Users )
                {
                    var child = CreateNode( user  );
                    node.Nodes.Add( child );
                }
            }
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
            if ( dialog.ShowDialog( this ) == DialogResult.OK )
            {
                var macro = new MacroCommand();

                var user = UnitOfWork.Factory.Create<IUser>();
                user.Username = dialog.Username;
                user.Password = dialog.Password;

                try
                {
                    UnitOfWork.Begin();
                    UnitOfWork.GetRepository<IUser>().Insert( user );
                    UnitOfWork.Commit();
                }
                catch ( Exception exception )
                {
                    UnitOfWork.Rollback();
                    MessageBox.Show( exception.Message, "Error", MessageBoxButtons.OK );
                    return;
                }

                macro.Add( new CreateUser( user ) );

                var node = CreateNode( user );
                macro.Add( new AddNode( mainTreeView.Nodes, node ) );

                CommandManager.Instance.Execute(macro);
            }
        }
        private void OnCreateOrganization(object sender, EventArgs e)
        {
            var dialog = new OrganizationForm( UnitOfWork.GetRepository<IUser>() ) {
                OrganizationName = "New Organization"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var macro = new MacroCommand();

                var organization = UnitOfWork.Factory.Create<IOrganization>();
                organization.Name = dialog.OrganizationName;
                foreach ( var user in dialog.Users)
                {
                    organization.Users.Add(user);
                }

                try
                {
                    UnitOfWork.Begin();
                    UnitOfWork.GetRepository<IOrganization>().Insert( organization );
                    UnitOfWork.Commit();
                }
                catch ( Exception exception )
                {
                    UnitOfWork.Rollback();
                    MessageBox.Show( exception.Message, "Error", MessageBoxButtons.OK );
                    return;
                }

                organization = ObservableFactory.Create<IOrganization>( organization );
                macro.Add( new CreateOrganization( organization ) );

                var node = CreateNode( organization );
                macro.Add(new AddNode(mainTreeView.Nodes, node));

                foreach ( var user in organization.Users)
                {
                    var child = CreateNode( user );
                    macro.Add( new AddNode( node.Nodes, child ) );
                }

                CommandManager.Instance.Execute(macro);
            }
        }

        private void OnEdit(object sender, EventArgs e)
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

        private void OnDelete( object sender, EventArgs e )
        {
            var item = mainTreeView.SelectedNode?.Tag as IItem;
            if ( item == null ) return;

            if ( item is IUser )
            {
                Delete( item as IUser );
            }
            else if ( item is IOrganization )
            {
                Delete( item as IOrganization );
            }
        }

        private TreeNode CreateNode( IUser user )
        {
            var node = new TreeNode( user.Username );
            node.ImageIndex = node.SelectedImageIndex = 0;

            user = ObservableFactory.Create<IUser>( user );
            user.Subscribe<IUser>( ( sender, args ) => node.Text = user.Username );
            node.Tag = user;

            return node;
        }

        private TreeNode CreateNode( IOrganization organization )
        {
            var node = new TreeNode( organization.Name );
            node.ImageIndex = node.SelectedImageIndex = 1;

            organization = ObservableFactory.Create<IOrganization>( organization );
            organization.Subscribe<IOrganization>( ( sender, args ) => node.Text = organization.Name );
            node.Tag = organization;

            return node;
        }

        private void Edit( IUser user )
        {
            var dialog = new UserForm { Username = user.Username, Password = user.Password };

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
            var dialog = new OrganizationForm( UnitOfWork.GetRepository<IUser>() );
            dialog.OrganizationName = organization.Name;
            dialog.Users = organization.Users;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var node  = mainTreeView.SelectedNode;
                var macro = new MacroCommand();

                if (organization.Name != dialog.OrganizationName)
                {
                    CommandManager.Instance.Execute( new ChangeName( organization, dialog.OrganizationName) );
                }

                foreach ( var user in organization.Users )
                {
                    if ( dialog.Users.Contains( user ) ) continue;

                    macro.Add(new RemoveOrganizationUser( organization, user ) );
                    RemoveChildNode( node, user, macro );
                }

                foreach (var user in dialog.Users)
                {
                    if (organization.Users.Contains(user)) continue;

                    macro.Add(new AddOrganizationUser(organization, user));
                    AddChildNode(node, user, macro);
                }

                if ( ! macro.IsEmpty ) CommandManager.Instance.Execute(macro);
            }
        }

        private void AddChildren( TreeNode parent, IEnumerable<IUser> users, MacroCommand macro )
        {
            foreach ( var user in users )
            {
                AddChildNode( parent, user, macro );
            }
        }

        private void AddChildNode( TreeNode parent, IUser user, MacroCommand macro)
        {
            var node = CreateNode( user );
            macro.Add( new AddNode( parent.Nodes, node ));
        }

        private void RemoveChildNode( TreeNode parent, IUser user, MacroCommand macro)
        {
            var child = parent.Nodes.Cast<TreeNode>().FirstOrDefault( node => node.Tag.Equals( user ) );
            if (child == null) return;

            macro.Add(new RemoveNode(parent.Nodes, child));
        }

        private void Delete( IUser user )
        {
            var macro = new MacroCommand();

            foreach ( var node in mainTreeView.GetAllNodes() )
            {
                if ( node.Tag == null ) continue;
                if ( ! node.Tag.Equals( user ) ) continue;

                var nodes = node.Parent?.Nodes ?? mainTreeView.Nodes;
                macro.Add( new RemoveNode( nodes, node ) );
            }

            macro.Add( new DeleteItem( user ) );
            UnitOfWork.GetRepository<IUser>().Update( user.GetSubject<IUser>() );

            CommandManager.Instance.Execute( macro );
        }

        private void Delete( IOrganization organization )
        {
            var macro = new MacroCommand();

            foreach ( var node in mainTreeView.GetAllNodes() )
            {
                if ( node.Tag == null ) continue;
                if ( ! node.Tag.Equals( organization ) ) continue;

                var nodes = node.Parent?.Nodes ?? mainTreeView.Nodes;
                macro.Add( new RemoveNode( nodes, node ) );
            }

            macro.Add( new DeleteItem( organization ) );
            UnitOfWork.GetRepository<IOrganization>().Update( organization.GetSubject<IOrganization>() );

            CommandManager.Instance.Execute( macro );
        }
    }
}
